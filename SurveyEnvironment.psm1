function Initialise{
    param(
        [string] $resourceGroupName
    )

    try{
        $location = "West Europe"
    
        #Create a resource group
        Write-Host "Creating resource group $resourceGroupName" -ForegroundColor Green

        #This is to check if the resource exist and only create if not exist
        Get-AzureRmResourceGroup -Name $resourceGroupName -ErrorVariable notPresent -ErrorAction SilentlyContinue

        if ($notPresent) {
            New-AzureRmResourceGroup -Name $resourceGroupName -Location $location
        }        

        Write-Host "Resource group $resourceGroupName created" -ForegroundColor Green

        #create sql server
        Write-Host "Create sql server"  -ForegroundColor Green

        $sqlServerName = $resourceGroupName.ToLower() + "server"
        $databaseName = $resourceGroupName.ToLower() + "db"

        #Should hash or randomise this, but for this excersise this will do just fine
        $serverPassword = $databaseName + $sqlServerName + "!!54!!321"
        $userName = $databaseName + "admin"

        #check if the sql serrver exist. If not create it and the database. If you delete the database in any other way besides running the cleanup script 
        #this will then not create a new database for you. you will have to cleanup environment or manually create your db again
        Get-AzureRmSqlServer -ServerName $sqlServerName -ResourceGroupName $resourceGroupName -ErrorVariable noServer -ErrorAction SilentlyContinue

        if($noServer){
             
             Write-Host "Go have coffee, this might take some time"  -ForegroundColor Yellow

             $pWord = ConvertTo-SecureString $serverPassword -AsPlainText -Force
             $credential = New-Object -TypeName System.Management.Automation.PSCredential -ArgumentList $userName, $pWord

             $server = New-AzureRmSqlServer -ResourceGroupName $resourceGroupName `
             -Location $location `
             -ServerName $sqlServerName `
             -ServerVersion "12.0" `
             -SqlAdministratorCredentials $credential
             
             #get the ip for the firewall rules to allow your pc access to the sql server for development
             $ip = Invoke-WebRequest 'https://api.ipify.org' | Select-Object -ExpandProperty Content

             Write-Host "Creating Firewall rules for $sqlServerName" -ForegroundColor Green

             $serverFirewallRule = New-AzureRmSqlServerFirewallRule -ResourceGroupName $resourceGroupName `
             -ServerName $server.ServerName `
             -FirewallRuleName "AllowedIPs" `
             -StartIpAddress $ip `
             -EndIpAddress $ip

            Write-Host "Creating Database $databaseName" -ForegroundColor Green
            Write-Host "Go have another coffee, this might take some more time"  -ForegroundColor Yellow

            New-AzureRmSqlDatabase -ResourceGroupName $resourceGroupName `
            -ServerName $sqlServerName `
            -DatabaseName $databaseName `
            -RequestedServiceObjectiveName "S0" `
            
        }
        
        $database = Get-AzureRmSqlDatabase -ResourceGroupName $resourceGroupName `
            -ServerName $sqlServerName `
            -DatabaseName $databaseName
                
        Write-Host "Finish Creating sql server" -ForegroundColor Green

        $connectionString = "Server=tcp:" + $sqlServerName + ".database.windows.net,1433;Initial Catalog=" + $databaseName + ";Persist Security Info=False;User ID=" + $userName + ";Password=" + $serverPassword + ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
        
        Write-Host "Replace appsettings.json" -ForegroundColor Green
        $json = Get-Content -Raw -Path "InterviewPlay/appsettings.json" | ConvertFrom-Json 
        $json.ConnectionStrings.SurveyDataBase = $connectionString
        $json | ConvertTo-Json | Out-File -FilePath ".\InterviewPlay/appsettings.json"

        #Docker compose build
        Write-Host "Starting docker-compose build" -ForegroundColor Green
        Write-Host "This might show some red text complaining about docker-compose, give it a few seconds. It should start the docker build and there after the docker run. If it does not build and run, then check that the appsettings changed and run docker build and run seperate from this script" -ForegroundColor Yellow
        docker-compose build

        Write-Host "Starting docker run" -ForegroundColor Green
        Write-Host "once running go to http://localhost:5000/" -ForegroundColor Yellow

        #docker run
        docker run -p 5000:80 interviewrun
    }
    catch{
        write-host "Exception Message: $($_.Exception.Message)" -ForegroundColor Red
    }
    
}

function PushToAzureContainerRegistry{
    param(
        [string] $resourceGroupName
    )

    #create container registry
    Write-Host "Create container registry" -ForegroundColor Green
    $regName = $resourceGroupName + "container001"
    $registry = New-AzureRmContainerRegistry -ResourceGroupName $resourceGroupName -Name $regName -EnableAdminUser -Sku Basic
    $creds = Get-AzureRmContainerRegistryCredential -Registry $registry

    $creds.Password | docker login $registry.LoginServer -u $creds.Username --password-stdin

    $acrLoginServer = "$regName.azurecr.io"
    $tag = "$acrLoginServer/interviewrun:v1"

    Write-Host "tag docker image" -ForegroundColor Green
    docker tag interviewrun $tag

    Write-Host "Push to registry" -ForegroundColor Green
    docker push $tag

    #I do this so when docker run start it will say cannot find and will pull, this show some level that it was pushed
    docker rmi $tag

    Write-Host "Run http://localhost:3000/ " -ForegroundColor Green
    docker run -p 3000:80 $tag
}

function CleanUpSurveyResources{
    param(
        [string] $resourceGroupName
    )
    
    Write-Host "Clean Azure resources" -ForegroundColor Yellow
    Remove-AzureRmResourceGroup -Name $resourceGroupName

    Write-Host "Clean Docker" -ForegroundColor Yellow
    docker kill $(docker ps -q)
    docker rmi -f $(docker images -q)
}

Export-ModuleMember -Function Initialise
Export-ModuleMember -Function PushToAzureContainerRegistry
Export-ModuleMember -Function CleanUpSurveyResources

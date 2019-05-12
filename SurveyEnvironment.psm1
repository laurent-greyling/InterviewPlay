function Initialise{
    param(
        [string] $resourceGroupName
    )

    try{
        $location = "West Europe"
    
        #Create a resource group
        Write-Host "Creating resource group $resourceGroupName" -ForegroundColor Green

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

        Write-Host "Starting docker-compose build" -ForegroundColor Green
        Write-Host "This might show some red text complaining about docker-compose, give it a few seconds. It should start the docker build and there after the docker run. If it does not build and run, then check that the appsettings changed and run docker build and run seperate from this script" -ForegroundColor Yellow
        docker-compose build

        Write-Host "Starting docker run" -ForegroundColor Green
        Write-Host "once running go to http://localhost:3000/" -ForegroundColor Yellow

        docker run -p 3000:80 interviewplay_web 
    }
    catch{
        write-host "Exception Message: $($_.Exception.Message)" -ForegroundColor Red
    }
    
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
Export-ModuleMember -Function CleanUpSurveyResources
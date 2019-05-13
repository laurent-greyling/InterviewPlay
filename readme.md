# Survey Play
This project was done in `ASP.net core Angular` with [Docker Support](https://www.docker.com/). For this project my goal was to display an interview to a respondent and have their answers saved into an `Azure Sql DB`. 

(Docker support for this project is for linux and I set right click docker => settings->Deamon- experimental on (probably not necessary))

## Setting Up
For this project to run correctly a `powershell` was created which will setup the Azure Sql, substitute the `appsettings.json` connection string value and finally start a `docker-compose build` and `docker run`. 

1. In Powershell (Preferably in Admin mode) go to the folder directory `\InterviewPlay`
2. Run `Connect-AzureRmAccount` and sign in to your azure account
3. Run `Import-Module .\SurveyEnvironment.psm1 -force`
4. Run `Initialise -resourceGroupName "<your chosen resource group name>"`

This will:

1. Create the `resource group` in west europe
2. Create an Azure Sql server (`$resourceGroupName + "server"`)
3. Setup the firewall rules for the server to allow your IP address
4. Create a Database (this is database: `$resourceGroupName + "db"` the interview will use)
5. Replace the connectionstring value in the `appsettings.json` file
6. Start `docker-compose build` => this will show red text, give it a second or two, it will start the build.
7. Start a `docker run -p 5000:80 interviewrun` (interviewrun as per the docker-compose.yml)

After this script has run you can use `http://localhost:5000/`  to see the survey run. If the `docker-compose build` doesn't start up then try doing it seperate from this script.

__Note__ if you do an interview and want to see answers in the database, check your `appsettings.json` for the needed information.

Once you have finished testing or looking at the interview and checked all is saved into the DB. 

Run:
`CleanUpSurveyResources -resourceGroupName "<your resource group name>"`

This will:

1. Remove your resource group from Azure, this will include your Sql server and database
2. Kill docker runs and remove the containers

## Additional Information
In this project you will find a folder and solution called `SurveyDeserialise`. This is incorrectly named and should be `CreateSurveyLookupTables`. 

This is intended to create lookup tables for the subject, question and category text. The main project called `SurveyPlay` will run an interview and only save the RespondentId, SubjectId, QuestionId, CategoryId and OpenAnswer. 

The lookup tables can then be used later in analyses to see what text belong to what Id. In this manner we do not care in what langauge the respondent answer the question, we only care for what they answered.

Run this app with the option `-c <your db connection string>`, connection string can be found in appsettings.json after powershell was run to create resources.

### Solutions and important files

1. Main Solution - `InterviewPlay.sln`
2. Support Solution - `SurveyDeserialise.sln`
3. `DockerFile`
4. `docker-compose.yml`
5. `SurveyEnvironment.psm1`

## To Improve
There are 6 main things that I believe can still be done on this project to improve it

1. Even though it is supported for a respondent to continue on an interview, when the respondent does so, the answers selected previously is not displayed yet.
2. When switching languages from English to Dutch answers are not transfered.
3. In code, the way langauge is selected can be done smarter.
4. Connectionstring info would be better suited for `Azure Keyvault` and for local development `dotnet secrets`
5. Though user input fields for the entity framework raw Sql queries are parametarised, the code still complains about this as the table name is not parametarised.
6. Proper logging into `Application Insights`. This is currently substituted with `Debug.Writeline` and can be replaced by application insights.


# Conclusion

The overall experience of building an ASP.net Core Angular project with Docker support was fun and challenging. I think I will do more projects in this manner to increase this knowledge.

![interview](https://user-images.githubusercontent.com/17876815/57584732-eb415580-74de-11e9-94d2-0cf5fe454a70.gif)









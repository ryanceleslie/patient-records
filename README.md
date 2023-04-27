# patient-records
This is a sample application to upload a csv file with basic records and save them. Built with .NET 6 API backend and Angular front end

## Solution Architecture

This application is following the basic principles of CLEAN architecture with core, infrastructure, repo, and presentation layers while also following domain driven design and SOLID principles

The eShopOnWeb application from Microsoft recommendation architecture examples was used as a guide for this project and to supplement best practices, that code base can be found here, https://github.com/dotnet-architecture/eShopOnWeb

I am also using TailwindCSS for the CSS options and Materials for the table, filtering, sorting, paging.

## Running Locally

To get started, create a database in a sql server and construct a connection string. Add that connection string to the appsettings.json file or a local secrets file.

Configuration points to a database I created in Azure for testing purposes. Will move to a local SQL server running in docker, but will likely not be able to maintain state between docker builds.

Furthermore, the API App also allows CORS from any source as the initial application of this does not have security enabled. A bearer token, or OAuth, will likely be set for future use, but for now it's open.

## Generating Code Migrations

```powershell
# run these from patient-records/src folder, not project folder
dotnet ef migrations add Initial -c Infrastructure.Data.PatientRecordsContext -p Infrastructure -s API -o Data/Migrations
dotnet ef database update Initial -c Infrastructure.Data.PatientRecordsContext -p Infrastructure -s API
```

## Automation

This project also includes automated deployments via commits to the `main` branch in the repo. The `main` branch is protected by policy settings to force a pull request for any commit, and when the pull request is merged, there are two GitHub actions that automatically builds the solutions, publishes and deploys both projects to individual an Azure Services.
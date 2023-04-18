# patient-records
This is a sample application to upload a csv file with basic records and save them. Built with .NET 6 API backend and Angular front end

## Solution Architecture

This application is following the basic principles of CLEAN architecture with core, infrastructure, repo, and presentation layers while also following domain driven design and SOLID principles

The eShopOnWeb application from Microsoft recommendation architecture examples was used as a guide for this project and to supplement best practices, that code base can be found here, https://github.com/dotnet-architecture/eShopOnWeb

## Running Locally

## Generating Code Migrations

```powershell
# run these from patient-records/src folder, not project folder
dotnet ef migrations add Initial -c Infrastructure.Data.PatientRecordsContext -p Infrastructure -s API -o Data/Migrations
dotnet ef database update Initial -c Infrastructure.Data.PatientRecordsContext -p Infrastructure -s API
```
# mm-platform# mm-platform

mm-platform is back-end part of Marcher-Markholt project.

## Installation
- Install [Docker](https://docs.docker.com/get-docker/). 
- Create local network bridge with docker CLI "docker network create -d bridge mm-network"
- Clone solution and Set as Start-up project "docker-compose".

## Usage
##### Development environment
Services endpoints:
| Service name        | Endpoint                       |
| ------------------- | ----------------------         |
| backoffice-bff-api  | http://localhost:5101/swagger  |
| frontoffice-bff-api | http://localhost:5201/swagger  |
| candidates-api      | http://localhost:5011/swagger  |
| companies-api:      | http://localhost:5012/swagger  |
| jobs-api:           | http://localhost:5013/swagger  |
| tags-system-api:    | http://localhost:5014/swagger  |

SQL instance details: Server: 127.0.0.1,1433; User: "sa"; Password: "Admin123!".

## Azure functions debugging
- Right click on solution and open "Properties".
- Set "Multiple startup projects" on "Startup Project" tab.
- Change "Action" to "Start" for "Docker-compose" project and for Azure function(s) project(s).

## KeyVault
BackOffice.Users.CacheRefresher project uses KeyVault for secrect settings and other project should start using it soon as well.
In order to have working project, you need to do following things:
1. Make sure your account has access to KeyVault secrects
2. Open PowerShell and run command: az loggin -tenant tenat_id_goes_here


## Migrations

After changes in data layer models or entity configurations files you have to add new migration for changes to appear in database. 
##### Migration in Visual Studio:
-	Install nugget package Microsoft.EntityFrameworkCore.Tools in project that contains DBContext.
- 	Set entry point project as Start-up project.
-   In Package manager console in Deafault project dropdown set your project that contains DBContext and EntityConfigurations files.
- Example of command to add new migration via Package manager console:
Add-Migration "migration name" -Context CandidateContext -OutputDir "./Persistence/Migrations"
-   More examples in [MS documentation](https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli).

## Contributing
- Follow [Feature branch workflow](https://www.atlassian.com/git/tutorials/comparing-workflows/feature-branch-workflow).
- Branches name structure: feature/taskId_short_name.
- 
## Useful to know.
- Project was build based on [Microservices with DDD and CQRS Patterns](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/).
- Infrastructure persistence layer with [Entity Framework Core](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implementation-entity-framework-core)
# clean-architecture-template

A clean architecture template for C# projects through Nugget.

## Structure (Empty):

```
CleanArchitecture/
|-src/
| |-CleanArchitecture.API/
| | |-Controllers/
| | |-Logs/
| | |-Middleware/
| | |-Models/
| | |-Api.csproj
| | |-Program.cs
|
| |-Core/
| | |-CleanArchitecture.Application/
| | | |-Features/
| | | |-Interfaces/
| | | |-Models/
| | | |-Exeptions/
| | | |-MappingProfies/
| | | |-Aplication.csproj
| |
| | |-CleanArchitecture.Domain/
| | | |-Common/
| | | |-Domain.csproj
| |
| |-Infraestructure/
| | |-CleanArchitecture.Identity/
| | | |-Configurations/
| | | |-DbContext/
| | | |-Migrations/
| | | |-Models/
| | | |-Services/
| | | |-Identity.csproj
| |
| | |-CleanArchitecture.Infrastructure/
| | | |-Infrastructure.csproj
| |
| | |-CleanArchitecture.Persistence/
| | | |-Configurations/
| | | |-DatabaseContext/
| | | |-Migrations/
| | | |-Repositories/
| | | |-Persistence.csproj
|
| |-UI/
| | |-WebUI/
| | |-DesktopUI/
| | |-MobileUI/
|
|-test/
| |-CleanArchitecture.UnitTests/
| | |-Features/
| | |-Mocks/
| | |-CleanArchitecture.UnitTests.csproj
| |-CleanArchitecture.IntegrationTests/
| | |-CleanArchitecture.IntegrationTests.csproj
|
|-CleanArchitecture.sln
```

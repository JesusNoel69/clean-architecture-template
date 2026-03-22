# clean-architecture-template

A clean architecture template for C# projects through Nugget.

## Structure (Empty):

```
CleanArchitecture/
|-src/
| |-API/
| | |-Controllers/
| | |-Logs/
| | |-Middleware/
| | |-Models/
| | |-Api.csproj
| | |-Program.cs
| |-Core/
| | |-Application/
| | | |-Features/
| | | |-Interfaces/
| | | |-Models/
| | | |-Exeptions/
| | | |-MappingProfies/
| | | |-Aplication.csproj
| | |-Domain/
| | | |-Common/
| | | |-Domain.csproj
| |-Infraestructure/
| | |-Identity/
| | | |-Configurations/
| | | |-DbContext/
| | | |-Migrations/
| | | |-Models/
| | | |-Services/
| | | |-Identity.csproj
| | |-Infrastructure/
| | | |-Infrastructure.csproj
| | |-Persistence/
| | | |-Configurations/
| | | |-DatabaseContext/
| | | |-Migrations/
| | | |-Repositories/
| | | |-Persistence.csproj
| |-UI/
| | |-WebUI/
| | |-DesktopUI/
| | |-MobileUI/
|-test/
| |-UnitTests/
| | |-UnitTests.Features/
| | |-UnitTests.Mocks/
| | |-UnitTests.csproj
| |-IntegrationTests/
| | |-IntegrationTests.csproj
|-CleanArchitecture.sln
```

# clean-architecture-template

A clean architecture template for C# projects using NuGet templates.

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
| |
| |-Core/
| | |-CleanArchitecture.Application/
| | | |-Features/
| | | |-Interfaces/
| | | |-Models/
| | | |-Exceptions/
| | | |-MappingProfiles/
| | | |-ApplicationServiceRegistration.cs
| | | |-Application.csproj
| | |
| | |-CleanArchitecture.Domain/
| | | |-Common/
| | | | |-BaseEntity.cs
| | | |-Domain.csproj
| |
| |-Infrastructure/
| | |-CleanArchitecture.Identity/
| | | |-Configurations/
| | | |-Context/
| | | | |-IdentityDbContext.cs
| | | |-Migrations/
| | | |-Models/
| | | |-Services/
| | | |-IdentityServiceRegistration.cs
| | | |-Identity.csproj
| | |
| | |-CleanArchitecture.Infrastructure/
| | | |-InfrastructureServiceRegistration.cs
| | | |-Infrastructure.csproj
| | |
| | |-CleanArchitecture.Persistence/
| | | |-Configurations/
| | | |-Context/
| | | | |-ApplicationDbContext.cs
| | | |-Migrations/
| | | |-Repositories/
| | | |-PersistenceServiceRegistration.cs
| | | |-Persistence.csproj
| |
| |-UI/
| | |-WebUI/
| | |-DesktopUI/
| | |-MobileUI/
|
|-test/
| |-CleanArchitecture.UnitTests/
| | |-Behaviors/
| | |-Common
| | |-Features/
| | |-Mocks/
| | |-CleanArchitecture.UnitTests.csproj
| |-CleanArchitecture.IntegrationTests/
| | |-API/
| | |-Persistence/
| | |-CleanArchitecture.IntegrationTests.csproj
|
|-CleanArchitecture.sln
```

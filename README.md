# clean-architecture-template

A clean architecture template for C# projects using NuGet templates.

## Features

✔ CQRS
✔ MediatR
✔ JWT
✔ Refresh Tokens
✔ Identity
✔ EF Core
✔ Repository Pattern
✔ FluentValidation
✔ Serilog
✔ Unit Tests
✔ Integration Tests

## Installation

dotnet new install ...

## Usage

dotnet new cleanarchitecture -n MyProject

dotnet new cleanarchitecture-sample -n Demo

## Structure (Empty):

```
CleanArchitecture/
|-src/
| |-CleanArchitecture.API/
| | |-Controllers/
| | |-ApiLogs/
| | |-Middleware/
| | |-Models/
| | |-Api.csproj
| | |-Program.cs
| |
| |-Core/
| | |-CleanArchitecture.Application/
| | | |-Features/
| | | |-Interfaces/
| | | | |-Identity/
| | | | |-Persitence/
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

## Structure (Sample):

```
CleanArchitecture/
|-src/
| |-CleanArchitecture.API/
| | |-Controllers/
| | | |-AutController.cs
| | | |-UsersController.cs
| | | |-WorkItemController.cs
| | |-ApiLogs/
| | |-Middleware/
| | | |-ExceptionMiddleware.cs
| | |-Models/
| | | |-CustomProblemDetails.cs
| | |-Api.csproj
| | |-Program.cs
| |
| |-Core/
| | |-CleanArchitecture.Application/
| | | |-Features/
| | | | |-Auth/
| | | | | |-Commands/
| | | | | | |-LoginUser/
| | | | | | |-RefreshToken/
| | | | | | |-RegisterUser/
| | | | | | |-RevokeToken/
| | | | | |-Queries/
| | | | | | |-GetCurrentUser/
| | | | |-Users/
| | | | | |-Commands/
| | | | | | |-AssignRole/
| | | | | | |-DeleteUser/
| | | | | | |-UpdateUser/
| | | | | |-Queries/
| | | | | | |-GetUserById/
| | | | | | |-GetUsers/
| | | | |-WorkItems/
| | | | | |-Commands/
| | | | | | |-ChangePriority/
| | | | | | |-CompleteWorkItem/
| | | | | | |-CreateWorkItem/
| | | | | | |-DeleteWorkItem/
| | | | | | |-UpdateWorkItem/
| | | | | |-Queries/
| | | | | | |-GetMyWorkItems/
| | | | | | |-GetOverDueWorkItems/
| | | | | | |-GetPendingWorkItems/
| | | | | | |-GetWorkItemById/
| | | |-Interfaces/
| | | | |-Identity/
| | | | | |-IAuthService.cs
| | | | | |-IUserService.cs
| | | | |-Logging/
| | | | | |-IAppLogger.cs
| | | | |-Persitence/
| | | | | |-IGenericRepository.cs
| | | | | |-IWorkItemRespository.cs
| | | |-Models/
| | | | |-Identity/
| | | | |-WorkItem/
| | | |-Exceptions/
| | | | |-BadRequestException.cs
| | | | |-NotFoundException.cs
| | | |-MappingProfiles/
| | | | |-WorkItemProfile.cs
| | | |-ApplicationServiceRegistration.cs
| | | |-Application.csproj
| | |
| | |-CleanArchitecture.Domain/
| | | |-Common/
| | | | |-BaseEntity.cs
| | | |-Enums/
| | | |-WorkItem.cs
| | | |-Domain.csproj
| |
| |-Infrastructure/
| | |-CleanArchitecture.Identity/
| | | |-Configurations/
| | | | |-RoleConfiguraion.cs
| | | | |-UserConfiguration.cs
| | | | |-UserRoleConfiguration.cs
| | | |-Context/
| | | | |-IdentityDbContext.cs
| | | |-Migrations/
| | | |-Models/
| | | | |-ApplicationUser.cs
| | | | |-CustomClaimTypes.cs
| | | | |-RefreshToken.cs
| | | | |-Roles.cs
| | | |-Services/
| | | | |-AuthService.cs
| | | | |-UserService.cs
| | | |-IdentityDbInitializer.cs
| | | |-IdentityServiceRegistration.cs
| | | |-Identity.csproj
| | |
| | |-CleanArchitecture.Infrastructure/
| | | |-Logging/
| | | | |-LoggerAdapter.cs
| | | |-InfrastructureServiceRegistration.cs
| | | |-Infrastructure.csproj
| | |
| | |-CleanArchitecture.Persistence/
| | | |-Configurations/
| | | | |-WorkItemConfiguration.cs
| | | |-Context/
| | | | |-ApplicationDbContext.cs
| | | |-Migrations/
| | | |-Repositories/
| | | | |-GenericRepository.cs
| | | | |-WorkItemRepository.cs
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
| | |-Common/
| | | |-Mocks/
| | | | |-WorkItemMockData.cs
| | |-Features/
| | | |-WorkItems/
| | | | |-ChangePriorityCommandHandlerTests.cs
| | | | |-CompleteWorkItemCommandHandlerTests.cs
| | | | |-CreateWorkItemCommandHandlerTests.cs
| | | | |-DeleteWorkItemCommandHandlerTests.cs
| | | | |-UpdateWorkItemCommandHandlerTests.cs
| | |-CleanArchitecture.UnitTests.csproj
| |-CleanArchitecture.IntegrationTests/
| | |-API/
| | | |-AuthControllerTests.cs
| | | |-WorkItemControllerTests.cs
| | |-Persistence/
| | | |-ApplicationDbContextTests.cs
| | | |-WorkItemRepositoryTests.cs
| | |-CleanArchitecture.IntegrationTests.csproj
| | |-CustomWebApplicationFactory.cs
|
|-CleanArchitecture.sln
```

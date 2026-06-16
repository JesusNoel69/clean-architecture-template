using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces.Identity;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CleanArchitecture.IntegrationTests.Persistence
{
    public class WorkItemsRepositoryTests
    {
        [Fact]
        public async Task Should_Return_Pending_WorkItems()
        {
            var options =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            var userService =
                Mock.Of<IUserService>();

            await using var context =
                new ApplicationDbContext(
                    userService,
                    options);

            context.WorkItem.Add(
                new WorkItem(
                    "Task",
                    "Description",
                    "user1",
                    WorkItemPriority.Medium));

            await context.SaveChangesAsync();

            var repository =
                new WorkItemRepository(context);

            var result =
                await repository.GetPendingByUserId(
                    "user1");

            Assert.Single(result);
        }
    }
}
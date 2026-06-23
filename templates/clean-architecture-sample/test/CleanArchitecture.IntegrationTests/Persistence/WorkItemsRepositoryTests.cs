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
        private readonly ApplicationDbContext _context;
        private readonly WorkItemRepository _repository;

        public WorkItemsRepositoryTests()
        {
            var userServiceMock =
                new Mock<IUserService>();

            var options =
                new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

            _context = new ApplicationDbContext(
                userServiceMock.Object,
                options);

            _repository =
                new WorkItemRepository(_context);
        }
        
        [Fact]
        public async Task Should_Return_Pending_WorkItems()
        {
            // Arrange
            var pending = new WorkItem(
                "Task1",
                "Description",
                "user1",
                WorkItemPriority.Medium);

            var completed = new WorkItem(
                "Task2",
                "Description",
                "user1",
                WorkItemPriority.Medium);

            completed.Complete();

            await _context.WorkItem.AddRangeAsync(
                pending,
                completed);

            await _context.SaveChangesAsync();

            // Act
            var result =
                await _repository.GetPendingByUserId(
                    "user1");

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public async Task Should_Return_True_When_WorkItem_Exists()
        {
            // Arrange
            var workItem = new WorkItem(
                "Task",
                "Description",
                "user1",
                WorkItemPriority.Medium);

            await _context.WorkItem.AddAsync(workItem);

            await _context.SaveChangesAsync();

            // Act
            var result =
                await _repository.WorkItemExists(
                    "user1",
                    "Task");

            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public async Task Should_Return_WorkItems_By_User()
        {
            // Arrange
            await _context.WorkItem.AddRangeAsync(
            [
                new WorkItem(
                    "Task1",
                    "Description",
                    "user1",
                    WorkItemPriority.Medium),

                new WorkItem(
                    "Task2",
                    "Description",
                    "user1",
                    WorkItemPriority.Medium),

                new WorkItem(
                    "Task3",
                    "Description",
                    "user2",
                    WorkItemPriority.Medium)
            ]);

            await _context.SaveChangesAsync();

            // Act
            var result =
                await _repository.GetByUserId("user1");

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task Should_Return_All_WorkItems()
        {
            // Arrange
            await _context.WorkItem.AddRangeAsync(
                new WorkItem(
                    "Task1",
                    "Description",
                    "user1",
                    WorkItemPriority.Medium),

                new WorkItem(
                    "Task2",
                    "Description",
                    "user2",
                    WorkItemPriority.Medium));

            await _context.SaveChangesAsync();

            // Act
            var result =
                await _repository.GetWorkItemsWithDetails();

            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}
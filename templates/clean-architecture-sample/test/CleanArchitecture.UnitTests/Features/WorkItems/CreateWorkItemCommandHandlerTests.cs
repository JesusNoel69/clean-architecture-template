using AutoMapper;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Features.WorkItems.Commands.CreateWorkItem;
using CleanArchitecture.Application.Interfaces.Logging;
using CleanArchitecture.Application.Interfaces.Persistence;
using CleanArchitecture.Application.Models.WorkItem;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.UnitTests.Common.Mocks;
using Moq;

namespace CleanArchitecture.UnitTests.Features.WorkItems
{
    public class CreateWorkItemCommandHandlerTests
    {
        private readonly Mock<IWorkItemRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IAppLogger<CreateWorkItemCommandHandler>> _loggerMock;
        private readonly CreateWorkItemCommandHandler _handler;

        public CreateWorkItemCommandHandlerTests()
        {
            _repositoryMock = new Mock<IWorkItemRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<IAppLogger<CreateWorkItemCommandHandler>>();
            _handler = new CreateWorkItemCommandHandler(_mapperMock.Object, _repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Should_ThrowBadRequest_WhenTitleAlreadyExists()
        {
            // Arrange
            var command = new CreateWorkItemCommand
            {
                UserId = "user1",
                Title = "Task"
            };

            _repositoryMock
                .Setup(x => x.WorkItemExists(
                    command.UserId,
                    command.Title))
                .ReturnsAsync(true);

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(
                () => _handler.Handle(
                    command,
                    CancellationToken.None));
        }

        [Fact]
        public async Task Should_Create_WorkItem()
        {
            // Arrange
            var command = new CreateWorkItemCommand
            {
                UserId = "user1",
                Title = "Task"
            };

            var workItem = WorkItemMockData.GetWorkItem(null);
            
            var dto = new WorkItemDto();

            _repositoryMock
                .Setup(x => x.WorkItemExists(
                    command.UserId,
                    command.Title))
                .ReturnsAsync(false);

            _mapperMock
                .Setup(x => x.Map<WorkItem>(command))
                .Returns(workItem);

            _mapperMock
                .Setup(x => x.Map<WorkItemDto>(workItem))
                .Returns(dto);

            // Act
            var result = await _handler.Handle(
                command,
                CancellationToken.None);

            // Assert
            _repositoryMock.Verify(
                x => x.CreateAsync(workItem),
                Times.Once);

            Assert.Same(dto, result);
        }
    }
}
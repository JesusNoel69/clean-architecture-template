using AutoMapper;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Application.Features.WorkItems.Commands.UpdateWorkItem;
using CleanArchitecture.Application.Interfaces.Logging;
using CleanArchitecture.Application.Interfaces.Persistence;
using CleanArchitecture.Application.Models.WorkItem;
using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.UnitTests.Common.Mocks;
using Moq;

namespace CleanArchitecture.UnitTests.Features.WorkItems
{
    public class UpdateWorkItemCommandHandlerTests
    {
        private readonly Mock<IWorkItemRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IAppLogger<UpdateWorkItemCommandHandler>> _loggerMock;
        private readonly UpdateWorkItemCommandHandler _handler;
        public UpdateWorkItemCommandHandlerTests()
        {
            _repositoryMock = new Mock<IWorkItemRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<IAppLogger<UpdateWorkItemCommandHandler>>();
            _handler = new UpdateWorkItemCommandHandler(_mapperMock.Object, _repositoryMock.Object, _loggerMock.Object);
        }
        [Fact]
        public async Task Should_ThrowNotFound_WhenWorkItemDoesNotExist()
        {
            // Arrange
            _repositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync((WorkItem?)null);

            var command = new UpdateWorkItemCommand{ Id = 1 };
            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(
                () => _handler.Handle(
                    command,
                    CancellationToken.None));
        }

        [Fact]
        public async Task Should_Update_WorkItem()
        {
            // Arrange
            var workItem = WorkItemMockData.GetWorkItem(null);

            var command = new UpdateWorkItemCommand
            {
                Id = 1
            };

            var dto = new WorkItemDto();

            _repositoryMock
                .Setup(x => x.GetByIdAsync(command.Id))
                .ReturnsAsync(workItem);

            _mapperMock
                .Setup(x => x.Map(command, workItem));

            _mapperMock
                .Setup(x => x.Map<WorkItemDto>(workItem))
                .Returns(dto);

            // Act
            var result = await _handler.Handle(
                command,
                CancellationToken.None);

            // Assert
            _mapperMock.Verify(
                x => x.Map(command, workItem),
                Times.Once);

            _repositoryMock.Verify(
                x => x.UpdateAsync(workItem),
                Times.Once);

            Assert.Same(dto, result);
        }
    }
}
using System.Net;
using System.Net.Http.Json;
using CleanArchitecture.Application.Features.WorkItems.Commands.CreateWorkItem;
using CleanArchitecture.Application.Models.WorkItem;
using Xunit;

namespace CleanArchitecture.IntegrationTests.API{
    public class WorkItemsControllerTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public WorkItemsControllerTests(
            CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Create_Should_Return_Created()
        {
            var command = new CreateWorkItemCommand
            {
                Title = "My Task",
                Description = "Description",
                UserId = "user1"
            };

            var response =
                await _client.PostAsJsonAsync(
                    "/api/workitems",
                    command);

            Assert.Equal(
                HttpStatusCode.Created,
                response.StatusCode);
        }

        [Fact]
        public async Task Create_Then_GetById_Should_Return_WorkItem()
        {
            var createResponse =
                await _client.PostAsJsonAsync(
                    "/api/workitems",
                    new CreateWorkItemCommand
                    {
                        Title = "Task",
                        Description = "Description",
                        UserId = "user1"
                    });

            var created =
                await createResponse.Content
                    .ReadFromJsonAsync<WorkItemDto>();

            var response =
                await _client.GetAsync(
                    $"/api/workitems/by-id/{created!.Id}");

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);

            var workItem =
                await response.Content
                    .ReadFromJsonAsync<WorkItemDto>();

            Assert.NotNull(workItem);
            Assert.Equal(created.Id, workItem!.Id);
        }
        [Fact]
        public async Task Delete_Should_Return_NoContent()
        {
            var createResponse =
                await _client.PostAsJsonAsync(
                    "/api/workitems",
                    new CreateWorkItemCommand
                    {
                        Title = "Task",
                        Description = "Description",
                        UserId = "user1"
                    });

            var created =
                await createResponse.Content
                    .ReadFromJsonAsync<WorkItemDto>();

            var deleteResponse =
                await _client.DeleteAsync(
                    $"/api/workitems/{created!.Id}");

            Assert.Equal(
                HttpStatusCode.NoContent,
                deleteResponse.StatusCode);
        }
    }
}
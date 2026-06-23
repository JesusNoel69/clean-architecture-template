using System.Net;
using System.Net.Http.Json;
using CleanArchitecture.Application.Features.Auth.Commands.RegisterUser;
using CleanArchitecture.Application.Models.Identity;
using Xunit;

namespace CleanArchitecture.IntegrationTests.API
{
    public class AuthControllerTests
        : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public AuthControllerTests(
            CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Register_Should_Return_Ok()
        {
            var command = new RegisterUserCommand
            {
                Email = "test@test.com",
                Password = "Password123!",
                FirstName = "John",
                LastName = "Doe"
            };

            var response =
                await _client.PostAsJsonAsync(
                    "/api/auth/register",
                    command);

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);
        }
        
        [Fact]
        public async Task Login_Should_Return_Token()
        {
            await _client.PostAsJsonAsync(
                "/api/auth/register",
                new RegisterUserCommand
                {
                    Email = "login@test.com",
                    Password = "Password123!",
                    FirstName = "John",
                    LastName = "Doe"
                });

            var response =
                await _client.PostAsJsonAsync(
                    "/api/auth/login",
                    new
                    {
                        Email = "login@test.com",
                        Password = "Password123!"
                    });

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);

            var result =
                await response.Content.ReadFromJsonAsync<AuthResponse>();

            Assert.NotNull(result);
            Assert.False(string.IsNullOrWhiteSpace(result!.Token));
        }
    }    
}

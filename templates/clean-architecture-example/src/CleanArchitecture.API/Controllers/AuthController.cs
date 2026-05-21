using System.Security.Claims;
using CleanArchitecture.Application.Interfaces.Identity;
using CleanArchitecture.Application.Models.Identity;
using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authenticationService, IUserService userService) : ControllerBase
    {
        private readonly IAuthService _authenticationService = authenticationService;
        private readonly IUserService _userService = userService;

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody]AuthRequest request)
        {
            return Ok(await _authenticationService.Login(request));
        }
        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register([FromBody]RegistrationRequest request)
        {
            return Ok(await _authenticationService.Register(request));
        }
        //Endpoints for test
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<User>> Me()
        {
            var userId = User.FindFirstValue(CustomClaimTypes.Uid);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }
            return Ok(await _userService.GetUser(userId));
        }
        [Authorize(Roles = Roles.Administrator)]
        [HttpGet("admin")]
        public IActionResult AdminOnly()
        {
            return Ok("You are an administrator");
        }
        [Authorize(Roles = Roles.Employee)]
        [HttpGet("employee")]
        public IActionResult EmployeeOnly()
        {
            return Ok("You are an employee");
        }
    }
}
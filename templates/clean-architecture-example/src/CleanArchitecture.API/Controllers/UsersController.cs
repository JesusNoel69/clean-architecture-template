using CleanArchitecture.Application.Features.Users.Commands.AssignRole;
using CleanArchitecture.Application.Features.Users.Commands.DeleteUser;
using CleanArchitecture.Application.Features.Users.Commands.UpdateUser;
using CleanArchitecture.Application.Interfaces.Identity;
using CleanArchitecture.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IMediator mediator, IUserService userService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator; 
        private readonly IUserService _userService = userService;
        
        [Authorize(Roles = Roles.Administrator)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;

            return Ok(await _mediator.Send(command));
        }
        [Authorize(Roles = Roles.Administrator)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _mediator.Send(new DeleteUserCommand(){ Id = id });

            return NoContent();
        }
        [Authorize(Roles = Roles.Administrator)]
        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
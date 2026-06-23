using CleanArchitecture.Application.Features.WorkItems.Commands.ChangePriority;
using CleanArchitecture.Application.Features.WorkItems.Commands.CompleteWorkItem;
using CleanArchitecture.Application.Features.WorkItems.Commands.CreateWorkItem;
using CleanArchitecture.Application.Features.WorkItems.Commands.DeleteWorkItem;
using CleanArchitecture.Application.Features.WorkItems.Commands.UpdateWorkItem;
using CleanArchitecture.Application.Features.WorkItems.Queries.GetMyWorkItems;
using CleanArchitecture.Application.Features.WorkItems.Queries.GetOverdueWorkItems;
using CleanArchitecture.Application.Features.WorkItems.Queries.GetPendingWorkItems;
using CleanArchitecture.Application.Features.WorkItems.Queries.GetWorkItemById;
using CleanArchitecture.Application.Models.WorkItem;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class WorkItemsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("by-id/{id}")]
        public async Task<ActionResult<WorkItemDto>> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetWorkItemByIdQuery(id)));
        }

        [HttpGet("my-work-items")]
        public async Task<ActionResult<List<WorkItemDto>>> GetMyWorkItems()
        {
            return Ok(await _mediator.Send(new GetMyWorkItemsQuery()));
        }

        [HttpGet("pending")]
        public async Task<ActionResult<List<WorkItemDto>>> GetPending()
        {
            return Ok(await _mediator.Send(new GetPendingWorkItemsQuery()));
        }

        [HttpGet("overdue")]
        public async Task<ActionResult<List<WorkItemDto>>> GetOverdue()
        {
            return Ok(await _mediator.Send(new GetOverdueWorkItemsQuery()));
        }

        [HttpPost]
        public async Task<ActionResult<WorkItemDto>> Create(CreateWorkItemCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WorkItemDto>> Update(int id, UpdateWorkItemCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteWorkItemCommand(){Id = id});
            return NoContent();
        }

        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            await _mediator.Send(new CompleteWorkItemCommand(){Id = id});
            return NoContent();
        }

        [HttpPatch("{id}/priority")]
        public async Task<IActionResult> ChangePriority(int id, ChangePriorityCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            await _mediator.Send(command);
            return NoContent();
        }
    }
}
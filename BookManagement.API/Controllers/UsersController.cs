using BookManagement.Application.Commands.CreateUser;
using BookManagement.Application.Commands.DeleteUser;
using BookManagement.Application.Commands.UpdateUser;
using BookManagement.Application.Queries.GetAllUsers;
using BookManagement.Application.Queries.GetUserById;
using BookManagement.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
	public UsersController(IMediator mediator)
	{
        _mediator = mediator;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<UserViewModel>>> GetAll()
	{
        var query = new GetAllUsersQuery();

        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserViewModel>> GetById(int id)
    {
        var userId = new GetUserByIdQuery(id);

        var userResult = await _mediator.Send(userId);

        if (userResult is null) return NotFound();
        
        return Ok(userResult);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
    {
        if (command is null) return BadRequest();

        var id = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = id }, command);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateUserCommand command)
    {
        var user = new UpdateUserCommand(id, command.Name, command.Email);

        await _mediator.Send(user);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteUserCommand(id);

        await _mediator.Send(command);

        return NoContent();
    }
}

using BookManagement.Application.Commands.CreateUser;
using BookManagement.Application.Commands.DeleteUser;
using BookManagement.Application.Commands.UpdateUser;
using BookManagement.Application.InputModels;
using BookManagement.Application.ViewModels;
using BookManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly BooksManagementDbContext _dbContext;
	public UsersController(BooksManagementDbContext dbContext, IMediator mediator)
	{
        _mediator = mediator;
		_dbContext = dbContext;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<UserViewModel>>> GetAll()
	{
        var users = await _dbContext.Users.ToListAsync();

        if (users is null) return BadRequest();

        var userViewModel = users.Select(user => new UserViewModel
		{
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
        }).ToList();

        return userViewModel;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserViewModel>> GetById(int id)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);

        if (user is null) return NotFound();

        var userViewModel = new UserViewModel
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
        };
        
        return userViewModel;
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

        var result = await _mediator.Send(user);

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

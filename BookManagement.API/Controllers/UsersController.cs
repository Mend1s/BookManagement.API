using BookManagement.Application.InputModels;
using BookManagement.Application.ViewModels;
using BookManagement.Core.Entities;
using BookManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly BooksManagementDbContext _dbContext;
	public UsersController(BooksManagementDbContext dbContext)
	{
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
    public async Task<IActionResult> Post([FromBody] CreateUserInputModel createUserInputModel)
    {
        var user = new User(createUserInputModel.Name, createUserInputModel.Email);

        await _dbContext.Users.AddAsync(user);

        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, createUserInputModel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateUserInputModel updateUserInputModel)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);

        if (user is null) return NotFound();

        user.UpdateUser(updateUserInputModel.Name, updateUserInputModel.Email);

        _dbContext.Users.Update(user);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);

        if (user is null) return NotFound();

        _dbContext.Users.Remove(user);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}

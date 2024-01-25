using BookManagement.Application.Commands.CreateLoan;
using BookManagement.Application.Commands.DeleteLoan;
using BookManagement.Application.Commands.RenewalLoan;
using BookManagement.Application.Commands.UpdateLoan;
using BookManagement.Application.InputModels;
using BookManagement.Application.ViewModels;
using BookManagement.Core.Entities;
using BookManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly IMediator _mediator;
	private readonly BooksManagementDbContext _dbContext;
	public LoansController(BooksManagementDbContext dbContext, IMediator mediator)
	{
		_dbContext = dbContext;
        _mediator = mediator;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<LoanViewModel>>> GetAll()
	{
		var loans = await _dbContext.Loans.ToListAsync();

		if (loans is null) return BadRequest();

		var loanViewModel = loans.Select(loan => new LoanViewModel
		{
            Id = loan.Id,
            IdUser = loan.IdUser,
            IdBook = loan.IdBook,
            LoanDate = loan.LoanDate,
            Devolution = loan.Devolution
        }).ToList();

		return Ok(loanViewModel);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<LoanViewModel>> GetById(int id)
	{
        var loan = await _dbContext.Loans.SingleOrDefaultAsync(l => l.Id == id);

        if (loan is null) return NotFound();

        var loanViewModel = new LoanViewModel
		{
            Id = loan.Id,
            IdUser = loan.IdUser,
            IdBook = loan.IdBook,
            LoanDate = loan.LoanDate,
            Devolution = loan.Devolution
        };
        
        return Ok(loanViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateLoanCommand command)
    {
        var loan = new Loan(command.IdUser, command.IdBook);

        var result = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = loan.Id }, command);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateLoanCommand command)
    {
        var user = new UpdateLoanCommand(id, command.IdUser, command.IdBook);

        await _mediator.Send(user);

        return NoContent();
    }

    [HttpPut("renewal/{id}")]
    public async Task<IActionResult> Renewal(int id)
    {
        var command = new RenewalLoanCommand(id);

        if (command is null) return BadRequest();

        await _mediator.Send(command);

        return Ok();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteLoanCommand(id);

        await _mediator.Send(command);

        return NoContent();
    }
}

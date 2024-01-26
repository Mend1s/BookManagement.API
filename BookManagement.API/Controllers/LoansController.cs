using BookManagement.Application.Commands.CreateLoan;
using BookManagement.Application.Commands.DeleteLoan;
using BookManagement.Application.Commands.RenewalLoan;
using BookManagement.Application.Commands.UpdateLoan;
using BookManagement.Application.Queries.GetAllLoan;
using BookManagement.Application.Queries.GetLoanById;
using BookManagement.Application.ViewModels;
using BookManagement.Core.Entities;
using BookManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        var query = new GetAllLoanQuery();

        var result = await _mediator.Send(query);

        return Ok(result);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<LoanViewModel>> GetById(int id)
	{
        var loanId = new GetLoanByIdQuery(id);

        var loanResult = await _mediator.Send(loanId);

        if (loanResult is null) return NotFound();

        return Ok(loanResult);
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
        try
        {
            var command = new RenewalLoanCommand(id);
            
            if (command is null) return BadRequest();

            await _mediator.Send(command);
            
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteLoanCommand(id);

        await _mediator.Send(command);

        return NoContent();
    }
}

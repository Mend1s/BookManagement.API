using BookManagement.Application.InputModels;
using BookManagement.Application.ViewModels;
using BookManagement.Core.Entities;
using BookManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
public class LoansController : ControllerBase
{
	private readonly BooksManagementDbContext _dbContext;
	public LoansController(BooksManagementDbContext dbContext)
	{
		_dbContext = dbContext;
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
    public async Task<IActionResult> Post([FromBody] LoanInputModel createLoanInputModel)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == createLoanInputModel.IdUser);

        if (user is null) return NotFound();

        var loan = new Loan(createLoanInputModel.IdUser, createLoanInputModel.IdBook);

        await _dbContext.Loans.AddAsync(loan);

        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = loan.Id }, createLoanInputModel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] LoanInputModel LoanInputModel)
    {
        var loan = await _dbContext.Loans.SingleOrDefaultAsync(l => l.Id == id);

        if (loan is null) return BadRequest();

        loan.UpdateLoan(LoanInputModel.IdUser, LoanInputModel.IdBook);

        await _dbContext.SaveChangesAsync();

        return Ok(loan);
    }

    [HttpPut("renewal/{id}")]
    public async Task<IActionResult> Renewal(int id)
    {
        var loan = await _dbContext.Loans.SingleOrDefaultAsync(l => l.Id == id);

        if (loan is null) return BadRequest();

        if (DateTime.Now >= loan.Devolution) return BadRequest("Devolução em atraso! Devolva o livro para criar um novo empréstimo.");

        var newDate = loan.Devolution.AddDays(10);

        loan.LoanRenewal(newDate);

        _dbContext.Update(loan);

        await _dbContext.SaveChangesAsync();

        return Ok(loan);
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var loan = await _dbContext.Loans.SingleOrDefaultAsync(l => l.Id == id);

        if (loan is null) return NotFound();

        _dbContext.Loans.Remove(loan);

        await _dbContext.SaveChangesAsync();

        return Ok(loan);
    }
}

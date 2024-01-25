using BookManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Application.Commands.RenewalLoan;

public class RenewalLoanCommandHandler : IRequestHandler<RenewalLoanCommand, Unit>
{
    private readonly BooksManagementDbContext _dbContext;
    public RenewalLoanCommandHandler(BooksManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Unit> Handle(RenewalLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _dbContext.Loans.SingleOrDefaultAsync(l => l.Id == request.Id);

        var validation = loan.CheckDevolution();

        var newDate = loan.Devolution.AddDays(10);

        loan.LoanRenewal(newDate);

        _dbContext.Update(loan);

        await _dbContext.SaveChangesAsync();

        return Unit.Value;
    }
}


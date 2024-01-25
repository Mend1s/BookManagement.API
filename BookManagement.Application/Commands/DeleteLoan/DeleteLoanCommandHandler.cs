using BookManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Application.Commands.DeleteLoan;

public class DeleteLoanCommandHandler : IRequestHandler<DeleteLoanCommand, Unit>
{
    private readonly BooksManagementDbContext _dbContext;
    public DeleteLoanCommandHandler(BooksManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Unit> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _dbContext.Loans.SingleOrDefaultAsync(l => l.Id == request.Id);

        _dbContext.Loans.Remove(loan);

        await _dbContext.SaveChangesAsync();

        return Unit.Value;
    }
}

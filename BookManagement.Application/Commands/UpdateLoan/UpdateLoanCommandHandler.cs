using BookManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Application.Commands.UpdateLoan;

public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, Unit>
{
    private readonly BooksManagementDbContext _dbContext;
    public UpdateLoanCommandHandler(BooksManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Unit> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _dbContext.Loans.SingleOrDefaultAsync(l => l.Id == request.Id);

        loan.UpdateLoan(request.IdUser, request.IdBook);

        await _dbContext.SaveChangesAsync();

        return Unit.Value;
    }
}

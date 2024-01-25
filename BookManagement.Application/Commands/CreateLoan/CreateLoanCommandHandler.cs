using BookManagement.Core.Entities;
using BookManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Application.Commands.CreateLoan;

public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, int>
{
    private readonly BooksManagementDbContext _dbContext;
    public CreateLoanCommandHandler(BooksManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.IdUser);

        var loan = new Loan(request.IdUser, request.IdBook);

        await _dbContext.Loans.AddAsync(loan);

        await _dbContext.SaveChangesAsync();

        return loan.Id;
    }
}

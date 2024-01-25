using BookManagement.Application.ViewModels;
using BookManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Application.Queries.GetLoanById;

public class GetLoanByIdQueryHandler : IRequestHandler<GetLoanByIdQuery, LoanViewModel>
{
    private readonly BooksManagementDbContext _dbContext;
    public GetLoanByIdQueryHandler(BooksManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<LoanViewModel> Handle(GetLoanByIdQuery request, CancellationToken cancellationToken)
    {
        var loan = await _dbContext.Loans.SingleOrDefaultAsync(l => l.Id == request.Id);

        if (loan is null) return null;

        return new LoanViewModel(loan.Id, loan.IdUser, loan.IdBook, loan.LoanDate, loan.Devolution);
    }
}

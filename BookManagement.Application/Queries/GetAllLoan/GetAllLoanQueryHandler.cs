using BookManagement.Application.ViewModels;
using BookManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Application.Queries.GetAllLoan;

public class GetAllLoanQueryHandler : IRequestHandler<GetAllLoanQuery, List<LoanViewModel>>
{
    private readonly BooksManagementDbContext _dbContext;
    public GetAllLoanQueryHandler(BooksManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<LoanViewModel>> Handle(GetAllLoanQuery request, CancellationToken cancellationToken)
    {
        var loans = _dbContext.Loans;

        var loansViewModel = await loans.Select(l => new LoanViewModel(l.Id, l.IdUser, l.IdBook, l.LoanDate, l.Devolution)).ToListAsync();

        return loansViewModel;
    }
}

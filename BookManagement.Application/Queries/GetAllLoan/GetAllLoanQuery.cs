using BookManagement.Application.ViewModels;
using MediatR;

namespace BookManagement.Application.Queries.GetAllLoan;

public class GetAllLoanQuery : IRequest<List<LoanViewModel>>
{
}

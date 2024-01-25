using BookManagement.Application.ViewModels;
using MediatR;

namespace BookManagement.Application.Queries.GetLoanById;

public class GetLoanByIdQuery : IRequest<LoanViewModel>
{
    public GetLoanByIdQuery(int id)
    {
        Id = id;
    }
    public int Id { get; private set; }
}

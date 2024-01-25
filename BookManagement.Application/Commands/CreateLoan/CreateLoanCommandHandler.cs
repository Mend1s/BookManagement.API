using BookManagement.Core.Entities;
using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Commands.CreateLoan;

public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, int>
{
    private readonly ILoanRepository _loanRepository;
    public CreateLoanCommandHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }
    public async Task<int> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = new Loan(request.IdUser, request.IdBook);

        await _loanRepository.CreateAsync(loan);

        return loan.Id;
    }
}

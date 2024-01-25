using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Commands.RenewalLoan;

public class RenewalLoanCommandHandler : IRequestHandler<RenewalLoanCommand, Unit>
{
    private readonly ILoanRepository _loanRepository;

    public RenewalLoanCommandHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }
    public async Task<Unit> Handle(RenewalLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _loanRepository.GetByIdAsync(request.Id);

        var validation = loan.CheckDevolution();

        var newDate = loan.Devolution.AddDays(10);

        loan.LoanRenewal(newDate);

        await _loanRepository.UpdateAsync(loan);

        return Unit.Value;
    }
}


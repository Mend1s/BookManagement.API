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

        if (loan is null)
        {
            throw new Exception("Empréstimo não encontrado.");
        }

        if (DateTime.Now >= loan.Devolution)
        {
            throw new Exception("Não é possível renovar um livro no dia de devolução dele ou com atraso, devolva e crie um novo empréstimo!");
        }

        var newDate = loan.Devolution.AddDays(10);

        loan.LoanRenewal(newDate);

        await _loanRepository.UpdateAsync(loan);

        return Unit.Value;
    }
}


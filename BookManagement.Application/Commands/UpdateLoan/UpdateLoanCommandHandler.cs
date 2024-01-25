using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Commands.UpdateLoan;

public class UpdateLoanCommandHandler : IRequestHandler<UpdateLoanCommand, Unit>
{
    private readonly ILoanRepository _loanRepository;
    public UpdateLoanCommandHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }
    public async Task<Unit> Handle(UpdateLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _loanRepository.GetByIdAsync(request.Id);

        loan.UpdateLoan(request.IdUser, request.IdBook);

        await _loanRepository.UpdateAsync(loan);

        return Unit.Value;
    }
}

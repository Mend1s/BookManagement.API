using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Commands.DeleteLoan;

public class DeleteLoanCommandHandler : IRequestHandler<DeleteLoanCommand, Unit>
{
    private readonly ILoanRepository _loanRepository;
    public DeleteLoanCommandHandler(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }
    public async Task<Unit> Handle(DeleteLoanCommand request, CancellationToken cancellationToken)
    {
        var loan = await _loanRepository.GetByIdAsync(request.Id);

        await _loanRepository.DeleteAsync(loan);

        return Unit.Value;
    }
}

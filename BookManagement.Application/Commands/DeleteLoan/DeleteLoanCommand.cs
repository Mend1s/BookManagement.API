using MediatR;

namespace BookManagement.Application.Commands.DeleteLoan;

public class DeleteLoanCommand : IRequest<Unit>
{
    public DeleteLoanCommand(int id)
    {
        Id = id;
    }

    public int Id { get; private set; }
}

using MediatR;

namespace BookManagement.Application.Commands.UpdateLoan;

public class UpdateLoanCommand : IRequest<Unit>
{
    public UpdateLoanCommand(int id, int idUser, int idBook)
    {
        Id = id;
        IdUser = idUser;
        IdBook = idBook;
    }

    public int Id { get; set; }
    public int IdUser { get; set; }
    public int IdBook { get; set; }
}

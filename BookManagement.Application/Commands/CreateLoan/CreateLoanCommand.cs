using MediatR;

namespace BookManagement.Application.Commands.CreateLoan
{
    public class CreateLoanCommand : IRequest<int>
    {
        public int IdUser { get; set; }
        public int IdBook { get; set; }
    }
}

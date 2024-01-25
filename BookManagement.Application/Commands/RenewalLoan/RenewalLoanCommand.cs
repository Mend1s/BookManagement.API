using MediatR;

namespace BookManagement.Application.Commands.RenewalLoan
{
    public class RenewalLoanCommand : IRequest<Unit>
    {
        public RenewalLoanCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}

using MediatR;

namespace BookManagement.Application.Commands.CreateUser;

public class CreateUserCommand : IRequest<int>
{
    public string Name { get; set; }
    public string Email { get; set; }
}

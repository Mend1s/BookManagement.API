using MediatR;

namespace BookManagement.Application.Commands.CreateUser;

public class CreateUserCommand : IRequest<int>
{
    public string Name { get; private set; }
    public string Email { get; private set; }
}

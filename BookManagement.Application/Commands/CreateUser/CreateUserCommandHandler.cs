using BookManagement.Core.Entities;
using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUserRepository _userRepository;
    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.Name, request.Email);

        await _userRepository.CreateAsync(user);

        return user.Id;
    }
}

using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        user.UpdateUser(request.Name, request.Email);

        await _userRepository.UpdateAsync(user);

        return Unit.Value;
    }
}

using BookManagement.Application.ViewModels;
using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserViewModel>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<List<UserViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();

        var usersViewModel = users
            .Select(u => new UserViewModel(
                u.Id,
                u.Name,
                u.Email))
            .ToList();

        return usersViewModel;
    }
}

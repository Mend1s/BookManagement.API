using BookManagement.Application.ViewModels;
using BookManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Application.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserViewModel>>
{
    private readonly BooksManagementDbContext _dbContext;
    public GetAllUsersQueryHandler(BooksManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<UserViewModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _dbContext.Users;

        var usersViewModel = await users.Select(u => new UserViewModel(u.Id, u.Name, u.Email)).ToListAsync();

        return usersViewModel;
    }
}

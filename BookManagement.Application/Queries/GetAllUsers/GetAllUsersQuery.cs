using BookManagement.Application.ViewModels;
using MediatR;

namespace BookManagement.Application.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<List<UserViewModel>>
{
}

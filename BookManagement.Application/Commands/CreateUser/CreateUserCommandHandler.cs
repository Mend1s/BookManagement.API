using BookManagement.Core.Entities;
using BookManagement.Infrastructure.Persistence;
using MediatR;

namespace BookManagement.Application.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly BooksManagementDbContext _dbContext;
    public CreateUserCommandHandler(BooksManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.Name, request.Email);

        await _dbContext.Users.AddAsync(user);

        await _dbContext.SaveChangesAsync();

        return user.Id;
    }
}

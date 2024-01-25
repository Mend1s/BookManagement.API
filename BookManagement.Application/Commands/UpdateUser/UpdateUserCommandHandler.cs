using BookManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Application.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly BooksManagementDbContext _dbContext;
    public UpdateUserCommandHandler(BooksManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == request.Id);

        user.UpdateUser(request.Name, request.Email);

        await _dbContext.SaveChangesAsync();

        return Unit.Value;
    }
}

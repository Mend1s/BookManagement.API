using BookManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Application.Commands.DeleteBook;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Unit>
{
    private readonly BooksManagementDbContext _dbContext;
    public DeleteBookCommandHandler(BooksManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books.SingleOrDefaultAsync(b => b.Id == request.Id);

        _dbContext.Books.Remove(book);

        await _dbContext.SaveChangesAsync();

        return Unit.Value;
    }
}

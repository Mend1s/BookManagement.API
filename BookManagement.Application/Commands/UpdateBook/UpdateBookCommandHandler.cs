using BookManagement.Application.InputModels;
using BookManagement.Infrastructure.Persistence;
using MediatR;

namespace BookManagement.Application.Commands.UpdateBook;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Unit>
{
    private readonly BooksManagementDbContext _dbContext;
    public UpdateBookCommandHandler(BooksManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = _dbContext.Books.SingleOrDefault(b => b.Id == request.Id);

        book.UpdateBook(request.Title, request.Author, request.ISBN, request.YearOfPublication);

        await _dbContext.SaveChangesAsync();

        return Unit.Value;
    }
}

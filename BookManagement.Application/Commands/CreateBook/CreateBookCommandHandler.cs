using BookManagement.Core.Entities;
using BookManagement.Infrastructure.Persistence;
using MediatR;

namespace BookManagement.Application.Commands.CreateBook;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
{
    private readonly BooksManagementDbContext _dbContext;
    public CreateBookCommandHandler(BooksManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book(request.Title, request.Author, request.ISBN, request.YearOfPublication);

        await _dbContext.Books.AddAsync(book);
        await _dbContext.SaveChangesAsync();

        return book.Id;
    }
}

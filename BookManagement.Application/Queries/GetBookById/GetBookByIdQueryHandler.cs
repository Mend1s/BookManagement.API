using BookManagement.Application.ViewModels;
using BookManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Application.Queries.GetBookById;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookViewModel>
{
    private readonly BooksManagementDbContext _dbContext;
    public GetBookByIdQueryHandler(BooksManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<BookViewModel> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books.SingleOrDefaultAsync(b => b.Id == request.Id);

        if (book is null) return null;

        return new BookViewModel(book.Id, book.Title, book.Author, book.Isbn, book.YearOfPublication);
    }
}

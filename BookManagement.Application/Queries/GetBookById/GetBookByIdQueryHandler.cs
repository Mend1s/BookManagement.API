using BookManagement.Application.ViewModels;
using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Queries.GetBookById;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookViewModel>
{
    private readonly IBookReposiroty _bookReposiroty;
    public GetBookByIdQueryHandler(IBookReposiroty bookReposiroty)
    {
        _bookReposiroty = bookReposiroty;
    }
    public async Task<BookViewModel> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _bookReposiroty.GetByIdAsync(request.Id);

        if (book is null) return null;

        return new BookViewModel(book.Id, book.Title, book.Author, book.Isbn, book.YearOfPublication);
    }
}

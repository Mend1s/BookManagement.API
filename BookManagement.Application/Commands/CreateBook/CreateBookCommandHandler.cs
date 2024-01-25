using BookManagement.Core.Entities;
using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Commands.CreateBook;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
{
    private readonly IBookReposiroty _bookReposiroty;
    public CreateBookCommandHandler(IBookReposiroty bookReposiroty)
    {
        _bookReposiroty = bookReposiroty;
    }
    public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book(request.Title, request.Author, request.Isbn, request.YearOfPublication);

        await _bookReposiroty.CreateAsync(book);

        return book.Id;
    }
}

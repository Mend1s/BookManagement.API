using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Commands.UpdateBook;

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Unit>
{
    private readonly IBookReposiroty _bookReposiroty;
    public UpdateBookCommandHandler(IBookReposiroty bookReposiroty)
    {
        _bookReposiroty = bookReposiroty;
    }
    public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookReposiroty.GetByIdAsync(request.Id);

        book.UpdateBook(request.Title, request.Author, request.Isbn, request.YearOfPublication);

        await _bookReposiroty.UpdateAsync(book);

        return Unit.Value;
    }
}

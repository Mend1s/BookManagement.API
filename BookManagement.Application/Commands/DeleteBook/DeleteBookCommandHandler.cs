using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Commands.DeleteBook;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Unit>
{
    private readonly IBookReposiroty _bookReposiroty;
    public DeleteBookCommandHandler(IBookReposiroty bookReposiroty)
    {
        _bookReposiroty = bookReposiroty;
    }
    public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookReposiroty.GetByIdAsync(request.Id);

        await _bookReposiroty.DeleteAsync(book);

        return Unit.Value;
    }
}

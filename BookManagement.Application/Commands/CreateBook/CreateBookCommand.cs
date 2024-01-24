using MediatR;

namespace BookManagement.Application.Commands.CreateBook;

public class CreateBookCommand : IRequest<int>
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string Isbn { get; private set; }
    public int YearOfPublication { get; private set; }
}

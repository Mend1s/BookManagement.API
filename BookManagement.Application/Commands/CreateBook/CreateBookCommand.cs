using MediatR;

namespace BookManagement.Application.Commands.CreateBook;

public class CreateBookCommand : IRequest<int>
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Isbn { get; set; }
    public int YearOfPublication { get; set; }
}

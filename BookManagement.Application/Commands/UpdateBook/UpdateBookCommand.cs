using MediatR;

namespace BookManagement.Application.Commands.UpdateBook;

public class UpdateBookCommand : IRequest<Unit>
{
    public UpdateBookCommand(int id, string title, string author, string isbn, int yearOfPublication)
    {
        Id = id;
        Title = title;
        Author = author;
        Isbn = isbn;
        YearOfPublication = yearOfPublication;
    }
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string Isbn { get; private set; }
    public int YearOfPublication { get; private set; }
}

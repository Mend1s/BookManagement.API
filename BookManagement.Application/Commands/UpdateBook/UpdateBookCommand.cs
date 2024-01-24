using MediatR;

namespace BookManagement.Application.Commands.UpdateBook;

public class UpdateBookCommand : IRequest<Unit>
{
    public UpdateBookCommand(int id, string title, string author, string ISBN, int yearOfPublication)
    {
        Id = id;
        Title = title;
        Author = author;
        this.ISBN = ISBN;
        YearOfPublication = yearOfPublication;
    }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int YearOfPublication { get; set; }
}

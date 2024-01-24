namespace BookManagement.Core.Entities;

public class Book : BaseEntity
{
    public Book() { }
    public Book(string title, string author, string isbn, int yearOfPublication)
    {
        Title = title;
        Author = author;
        Isbn = isbn;
        YearOfPublication = yearOfPublication;
    }

    public string Title { get; private set; }
    public string Author { get; private set; }
    public string Isbn { get; private set; }
    public int YearOfPublication { get; private set; }

    public void UpdateBook (string title, string author, string isbn, int yearOfPublication)
    {
        Title = title;
        Author = author;
        Isbn = isbn;
        YearOfPublication = yearOfPublication;
    }
}

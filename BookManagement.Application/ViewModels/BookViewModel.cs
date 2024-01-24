namespace BookManagement.Application.ViewModels;

public class BookViewModel
{
    public BookViewModel(int id, string title, string author, string iSBN, int yearOfPublication)
    {
        Id = id;
        Title = title;
        Author = author;
        ISBN = iSBN;
        YearOfPublication = yearOfPublication;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int YearOfPublication { get; set; }
}

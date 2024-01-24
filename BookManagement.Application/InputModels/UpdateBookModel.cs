namespace BookManagement.Application.InputModels;

public class UpdateBookModel
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Isbn { get; set; }
    public int YearOfPublication { get; set; }
}

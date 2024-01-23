namespace BookManagement.Application.InputModels;

public class CreateBookInputModel
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int YearOfPublication { get; set; }
}

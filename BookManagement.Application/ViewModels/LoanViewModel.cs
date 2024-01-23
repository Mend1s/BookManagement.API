namespace BookManagement.Application.ViewModels;

public class LoanViewModel
{
    public int Id { get; set; }
    public int IdUser { get; set; }
    public int IdBook { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime Devolution { get; set; }
}

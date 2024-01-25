namespace BookManagement.Application.ViewModels;

public class LoanViewModel
{
    public LoanViewModel(int id, int idUser, int idBook, DateTime loanDate, DateTime devolution)
    {
        Id = id;
        IdUser = idUser;
        IdBook = idBook;
        LoanDate = loanDate;
        Devolution = devolution;
    }

    public int Id { get; set; }
    public int IdUser { get; set; }
    public int IdBook { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime Devolution { get; set; }
}

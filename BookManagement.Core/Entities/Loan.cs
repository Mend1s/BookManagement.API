using System.Text.Json.Serialization;

namespace BookManagement.Core.Entities;

public class Loan : BaseEntity
{
    public Loan(int idUser, int idBook)
    {
        IdUser = idUser;
        IdBook = idBook;
    }

    public int IdUser { get; private set; }
    [JsonIgnore]
    public User User { get; private set; }
    public int IdBook { get; private set; }
    [JsonIgnore]
    public Book Book { get; private set; }
    public DateTime LoanDate { get; private set; } = DateTime.Now;
    public DateTime Devolution { get; private set; } = DateTime.Now.Date.AddDays(10);

    public void UpdateLoan(int idUser, int idBook)
    {
        IdUser = idUser;
        IdBook = idBook;
    }

    public void LoanRenewal(DateTime date)
    {
        Devolution = date;
    }

    public string CheckDevolution()
    {
        if (DateTime.Now >= Devolution)
        {
            return("Devolução em atraso! Devolva o livro para criar um novo empréstimo.");      
        }
        return "Devolução em dia!";
    }
}

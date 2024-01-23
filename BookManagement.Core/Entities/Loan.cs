using System.Text.Json.Serialization;

namespace BookManagement.Core.Entities;

public class Loan : BaseEntity
{
    public Loan(int idUser, int idBook, DateTime loanDate)
    {
        IdUser = idUser;
        IdBook = idBook;
        LoanDate = loanDate;
    }

    public int IdUser { get; private set; }
    [JsonIgnore]
    public User User { get; private set; }
    public int IdBook { get; private set; }
    [JsonIgnore]
    public Book Book { get; private set; }
    public DateTime LoanDate { get; private set; }

    public void UpdateLoan()
    {
        LoanDate = DateTime.Now;
    }
}

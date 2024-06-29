using System.ComponentModel.DataAnnotations.Schema;
using dtaquito_backend_web_app.Payments.Domain.Model.Commands;
using dtaquito_backend_web_app.Users.Domain.Model.Aggregates;

namespace dtaquito_backend_web_app.Payments.Domain.Model.Aggregates;

[Table("payments")]
public class Payment
{
    public int Id { get; }

    public string CardNumber { get; set; }

    public DateTime ExpirationDate { get; private set; }

    public string CardHolder { get; private set; }

    public string CardIssuer { get; private set; }

    public string CVV { get; private set; }

    [Column("user_id")] public int UserId { get; set; } // This is the foreign key property

    [ForeignKey("UserId")] // Use the name of the foreign key property here
    public User User { get; set; }

    public int Balance { get; set; }

    protected Payment()
    {

        CardNumber = string.Empty;
        ExpirationDate = DateTime.Now;
        CardHolder = string.Empty;
        CardIssuer = string.Empty;
        CVV = string.Empty;
        Balance = 0;
    }

    public Payment(CreatePaymentCommand command, User user)
    {
        CardNumber = command.CardNumber;
        ExpirationDate = command.ExpirationDate;
        CardHolder = command.CardHolder;
        CardIssuer = command.CardIssuer;
        CVV = command.CVV;
        User = user;
        Balance = command.Balance;
    }
    
    public void UpdateBalance(int balance)
    {
        Balance = balance;
    }
}
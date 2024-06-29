namespace dtaquito_backend_web_app.Payments.Domain.Model.Commands;

public record CreatePaymentCommand(string CardNumber, DateTime ExpirationDate, string CardHolder, string CardIssuer, string CVV, int UserId, int Balance);
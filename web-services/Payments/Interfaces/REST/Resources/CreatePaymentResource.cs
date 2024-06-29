namespace dtaquito_backend_web_app.Payments.Interfaces.REST.Resources;

public record CreatePaymentResource(string CardNumber, DateTime ExpirationDate, string CardHolder, string CardIssuer, string CVV, int UserId, int Balance);
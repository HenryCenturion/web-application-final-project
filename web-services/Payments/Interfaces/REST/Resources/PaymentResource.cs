using dtaquito_backend_web_app.Users.Domain.Model.Aggregates;

namespace dtaquito_backend_web_app.Payments.Interfaces.REST.Resources;

public record PaymentResource(int Id, string CardNumber, DateTime ExpirationDate, string CardHolder, string CardIssuer, string CVV, User user, int Balance);
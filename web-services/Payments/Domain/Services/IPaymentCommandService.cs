using dtaquito_backend_web_app.Payments.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Payments.Domain.Model.Commands;

namespace dtaquito_backend_web_app.Payments.Domain.Services;

public interface IPaymentCommandService
{
    Task<(Payment? payment, string? ErrorMessage)> Handle(CreatePaymentCommand command, int userId);
    Task Delete(int id);
    Task Update(Payment payment);

}
using dtaquito_backend_web_app.Payments.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Payments.Domain.Model.Queries;

namespace dtaquito_backend_web_app.Payments.Domain.Services;

public interface IPaymentQueryService
{
    Task<Payment?> Handle(GetPaymentByIdQuery query);
    Task<Payment?> GetPaymentByUserId(int userId);

    Task<Payment?> GetAllPayments();

}
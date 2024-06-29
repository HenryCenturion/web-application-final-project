using dtaquito_backend_web_app.Payments.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Shared.Domain.Repositories;

namespace dtaquito_backend_web_app.Payments.Domain.Repositories;

public interface IPaymentRepository : IBaseRepository<Payment>
{
    IQueryable<Payment> GetAll();
}
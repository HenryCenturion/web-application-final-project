using dtaquito_backend_web_app.Payments.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Payments.Domain.Repositories;
using dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Configuration;
using dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Repositories;

namespace dtaquito_backend_web_app.Payments.Infrastructure.Persistance.EFC.Repositories;

public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
{
    public PaymentRepository(AppDBContext context) : base(context) { }

    public IQueryable<Payment> GetAll()
    {
        return Context.Set<Payment>();
    }    
}
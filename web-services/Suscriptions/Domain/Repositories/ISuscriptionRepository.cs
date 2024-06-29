using dtaquito_backend_web_app.Shared.Domain.Repositories;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.Aggregates;

namespace dtaquito_backend_web_app.Suscriptions.Domain.Repositories;

public interface ISuscriptionRepository : IBaseRepository<Suscription>
{
    IQueryable<Suscription> GetAll();
}
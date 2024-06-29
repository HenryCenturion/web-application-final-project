using dtaquito_backend_web_app.Suscriptions.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.Queries;

namespace dtaquito_backend_web_app.Suscriptions.Domain.Services;

public interface ISuscriptionQueryService
{
    Task<Suscription?> Handle(GetSuscriptionByIdQuery query);
    Task<Suscription?> GetSuscriptionByUserId(long userId);

}
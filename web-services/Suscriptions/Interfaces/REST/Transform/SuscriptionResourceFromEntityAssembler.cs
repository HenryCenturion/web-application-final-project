using dtaquito_backend_web_app.Suscriptions.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Suscriptions.Interfaces.REST.Resources;

namespace dtaquito_backend_web_app.Suscriptions.Interfaces.REST.Transform;

public class SuscriptionResourceFromEntityAssembler
{
    public static SuscriptionResource ToResourceFromEntity(Suscription suscription)
    {
        return new SuscriptionResource((int)suscription.Plan, suscription.UserId);
    }
}
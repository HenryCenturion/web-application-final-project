using dtaquito_backend_web_app.Payments.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Payments.Interfaces.REST.Resources;

namespace dtaquito_backend_web_app.Payments.Interfaces.REST.Transform;

public class PaymentResourceFromEntityAssembler
{
    public static PaymentResource ToResourceFromEntity(Payment entity)
    {
        return new PaymentResource(entity.Id, entity.CardNumber, entity.ExpirationDate, entity.CardHolder, entity.CardIssuer, entity.CVV, entity.User, entity.Balance);
    }
}
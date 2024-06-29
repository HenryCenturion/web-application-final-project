using dtaquito_backend_web_app.Payments.Domain.Model.Commands;
using dtaquito_backend_web_app.Payments.Interfaces.REST.Resources;

namespace dtaquito_backend_web_app.Payments.Interfaces.REST.Transform;

public class CreatePaymentCommandFromResourceAssembler
{
    public static CreatePaymentCommand ToCommandFromResource(CreatePaymentResource resource)
    {
        return new CreatePaymentCommand(resource.CardNumber, resource.ExpirationDate, resource.CardHolder, resource.CardIssuer, resource.CVV, resource.UserId, resource.Balance);
    }
}
using dtaquito_backend_web_app.Suscriptions.Domain.Model.Commands;
using dtaquito_backend_web_app.Suscriptions.Interfaces.REST.Resources;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.ValueObjects; // Ensure you import the namespace where PlanType is defined

namespace dtaquito_backend_web_app.Suscriptions.Interfaces.REST.Transform;

public class CreateSuscriptionCommandFromResourceAssembler
{
    public static CreateSuscriptionCommand ToCommandFromResource(CreateSuscriptionResource resource)
    {
        PlanTypes planTypes = Enum.Parse<PlanTypes>(resource.PlanId.ToString());

        return new CreateSuscriptionCommand(planTypes, resource.UserId);
    }
}
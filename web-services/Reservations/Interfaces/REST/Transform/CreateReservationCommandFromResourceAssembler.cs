using dtaquito_backend_web_app.Reservations.Domain.Model.Commands;
using dtaquito_backend_web_app.Reservations.Interfaces.REST.Resources;

namespace dtaquito_backend_web_app.Reservations.Interfaces.REST.Transform;

public class CreateReservationCommandFromResourceAssembler
{
    public static CreateReservationCommand ToCommandFromResource(CreateReservationResource resource)
    {
        return new CreateReservationCommand(resource.Time, resource.Hours, resource.UserId, resource.SportSpaceId);
    }
}
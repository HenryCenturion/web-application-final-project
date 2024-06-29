using dtaquito_backend_web_app.Reservations.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Reservations.Interfaces.REST.Resources;

namespace dtaquito_backend_web_app.Reservations.Interfaces.REST.Transform;

public class ReservationResourceFromEntityAssembler
{
    public static ReservationResource ToResourceFromEntity(Reservation reservation)
    {
        return new ReservationResource(reservation.Id, reservation.Time, reservation.Hours, reservation.User, reservation.SportSpace);
    }

    public static List<ReservationResource> ToResourceFromEntity(List<Reservation> reservations)
    {
        return reservations.Select(reservation => ToResourceFromEntity(reservation)).ToList();
    }
}

using dtaquito_backend_web_app.Reservations.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Reservations.Domain.Model.Queries;

namespace dtaquito_backend_web_app.Reservations.Domain.Services;

public interface IReservationQueryService
{
    Task<Reservation?> Handle(GetReservationByIdQuery query);
    Task<List<Reservation>> GetReservationBySportSpacesId(int sportSpacesId);
    Task<List<Reservation>> GetAllReservations();
    Task<List<Reservation>> Handle(GetReservationsByUserIdQuery query);

    Task<List<Reservation>> GetReservationsByUserId(int userId);
}
using dtaquito_backend_web_app.Reservations.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Reservations.Domain.Model.Commands;

namespace dtaquito_backend_web_app.Reservations.Domain.Services;

public interface IReservationCommandService
{
    Task<(Reservation? reservation, string? ErrorMessage)> Handle(int userId, CreateReservationCommand command);
    Task Delete(int id);
}
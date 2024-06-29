using dtaquito_backend_web_app.Reservations.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Shared.Domain.Repositories;

namespace dtaquito_backend_web_app.Reservations.Domain.Repositories;

public interface IReservationRepository : IBaseRepository<Reservation>
{
    IQueryable<Reservation> GetAll();
}
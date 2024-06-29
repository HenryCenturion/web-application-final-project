using dtaquito_backend_web_app.Reservations.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Reservations.Domain.Repositories;
using dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Configuration;
using dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Repositories;

namespace dtaquito_backend_web_app.Reservations.Infrastructure.Persistance.EFC.Repositories;

public class ReservationRepository : BaseRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(AppDBContext context) : base(context) { }
    
    public IQueryable<Reservation> GetAll()
    {
        return Context.Set<Reservation>();
    }
}
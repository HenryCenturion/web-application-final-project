using dtaquito_backend_web_app.Reservations.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Reservations.Domain.Model.Queries;
using dtaquito_backend_web_app.Reservations.Domain.Repositories;
using dtaquito_backend_web_app.Reservations.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace dtaquito_backend_web_app.Reservations.Application.QueryServices;

public class ReservationQueryService : IReservationQueryService
{
    private readonly IReservationRepository reservationRepository;
    
    public ReservationQueryService(IReservationRepository reservationRepository)
    {
        this.reservationRepository = reservationRepository;
    }
    
    public async Task<Reservation?> Handle(GetReservationByIdQuery query)
    {
        return await reservationRepository
            .GetAll()
            .Include(reservation => reservation.User)
            .FirstOrDefaultAsync(reservation => reservation.Id == query.Id);
    }
    
    public async Task<List<Reservation>> GetReservationBySportSpacesId(int sportSpacesId)
    {
        return await reservationRepository
            .GetAll()
            .Include(reservation => reservation.User)
            .Where(reservation => reservation.SportSpaceId == sportSpacesId)
            .ToListAsync();
    }
    
    public async Task<List<Reservation>> GetAllReservations()
    {
        return await reservationRepository
            .GetAll()
            .Include(reservation => reservation.User)
            .ToListAsync();
    }
    
    public async Task<List<Reservation>> Handle(GetReservationsByUserIdQuery query)
    {
        return await reservationRepository
            .GetAll()
            .Include(reservation => reservation.User)
            .Where(reservation => reservation.UserId == query.UserId)
            .ToListAsync();
    }

    public async Task<List<Reservation>> GetReservationsByUserId(int userId)
    {
        return await reservationRepository
            .GetAll()
            .Include(reservation => reservation.User)
            .Where(reservation => reservation.UserId == userId)
            .ToListAsync();
    }
}
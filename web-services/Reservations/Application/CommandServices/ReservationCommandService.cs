
using dtaquito_backend_web_app.Reservations.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Reservations.Domain.Model.Commands;
using dtaquito_backend_web_app.Reservations.Domain.Repositories;
using dtaquito_backend_web_app.Reservations.Domain.Services;
using dtaquito_backend_web_app.Shared.Domain.Repositories;
using dtaquito_backend_web_app.SportSpaces.Domain.Repositories;
using dtaquito_backend_web_app.Users.Domain.Repositories;

namespace dtaquito_backend_web_app.Reservations.Application.CommandServices;

public class ReservationCommandService(IReservationRepository reservationRepository, 
    IUnitOfWork unitOfWork, ISportSpacesRepository sportSpacesRepository, IUserRepository userRepository) : IReservationCommandService
{
    public async Task<(Reservation? reservation, string? ErrorMessage)> Handle(int userId, CreateReservationCommand command)
    {
        var user = await userRepository.FindByIdAsync(userId);        
        if (user == null)
        {
            throw new ArgumentException("User not found");
        }

        var sportSpace = await sportSpacesRepository.FindByIdAsync(command.SportSpaceId);
        if (sportSpace == null)
        {
            throw new ArgumentException("SportSpace not found");
        }

        var reservation = new Reservation(command, user, sportSpace);
        await reservationRepository.AddAsync(reservation);
        return (reservation, null);
    }

    public async Task Delete(int id)
    {
        var reservation = await reservationRepository.FindByIdAsync(id);
        if (reservation != null)
        {
            reservationRepository.Remove(reservation);
            await unitOfWork.CompleteAsync();
        }
    }
}
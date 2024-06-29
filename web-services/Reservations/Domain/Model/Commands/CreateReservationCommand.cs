namespace dtaquito_backend_web_app.Reservations.Domain.Model.Commands;

public record CreateReservationCommand(DateTime Time, int Hours, int UserId, int SportSpaceId);
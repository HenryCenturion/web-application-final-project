namespace dtaquito_backend_web_app.Reservations.Interfaces.REST.Resources;

public record CreateReservationResource(DateTime Time, int Hours, int UserId, int SportSpaceId);
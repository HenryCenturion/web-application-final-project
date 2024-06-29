using dtaquito_backend_web_app.SportSpaces.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Users.Domain.Model.Aggregates;

namespace dtaquito_backend_web_app.Reservations.Interfaces.REST.Resources;

public record ReservationResource(int Id, DateTime Time, int Hours, User user, SportSpace sportSpace);
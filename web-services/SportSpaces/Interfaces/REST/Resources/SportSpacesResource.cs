using dtaquito_backend_web_app.Users.Domain.Model.Aggregates;

namespace dtaquito_backend_web_app.SportSpaces.Interfaces.REST.Resources;

public record SportSpacesResource(int Id, string Name, string ImageUrl, int Price, string Description, User user, string StartTime, string EndTime, int Rating);
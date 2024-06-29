namespace dtaquito_backend_web_app.SportSpaces.Interfaces.REST.Resources;

public record CreateSportSpacesResource(string Name, string ImageUrl, int Price, string Description, int UserId, string StartTime, string EndTime, int Rating);
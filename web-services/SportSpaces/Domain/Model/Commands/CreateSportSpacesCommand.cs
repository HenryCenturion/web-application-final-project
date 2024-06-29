namespace dtaquito_backend_web_app.SportSpaces.Domain.Model.Commands;

public record CreateSportSpacesCommand(string Name, string ImageUrl, int Price, string Description, int UserId, string StartTime, string EndTime, int Rating);
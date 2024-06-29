using dtaquito_backend_web_app.SportSpaces.Domain.Model.Aggregates;
using dtaquito_backend_web_app.SportSpaces.Domain.Model.Commands;

namespace dtaquito_backend_web_app.SportSpaces.Domain.Services;

public interface ISportSpacesCommandService
{
    Task<(SportSpace? SportSpace, string? ErrorMessage)> Handle(CreateSportSpacesCommand command, int userId);
    Task Delete(int id);
}
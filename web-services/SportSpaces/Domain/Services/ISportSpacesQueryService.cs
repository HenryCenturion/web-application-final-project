using dtaquito_backend_web_app.SportSpaces.Domain.Model.Aggregates;
using dtaquito_backend_web_app.SportSpaces.Domain.Model.Queries;

namespace dtaquito_backend_web_app.SportSpaces.Domain.Services;

public interface ISportSpacesQueryService
{
    Task<SportSpace?> Handle(GetSportSpacesByIdQuery query);
    Task<IEnumerable<SportSpace>> GetAllSportSpaces();

    Task<SportSpace?> Handle(GetSportSpacesByUserIdQuery query);

}
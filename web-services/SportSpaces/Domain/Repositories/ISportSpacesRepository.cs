using dtaquito_backend_web_app.Shared.Domain.Repositories;
using dtaquito_backend_web_app.SportSpaces.Domain.Model.Aggregates;

namespace dtaquito_backend_web_app.SportSpaces.Domain.Repositories;

public interface ISportSpacesRepository : IBaseRepository<SportSpace>
{
    IQueryable<SportSpace> GetAll();

    IQueryable<SportSpace> FindByUserId(int UserId);
}
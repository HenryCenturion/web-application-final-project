using dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Configuration;
using dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Repositories;
using dtaquito_backend_web_app.SportSpaces.Domain.Model.Aggregates;
using dtaquito_backend_web_app.SportSpaces.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace dtaquito_backend_web_app.SportSpaces.Infrastructure.Persistance.EFC.Repositories;

public class SportSpacesRepository : BaseRepository<SportSpace>, ISportSpacesRepository
{
    public SportSpacesRepository(AppDBContext context) : base(context) { }
    
    public IQueryable<SportSpace> GetAll()
    {
        return Context.Set<SportSpace>();
    }

    public IQueryable<SportSpace> FindByUserId(int UserId)
    {
        return Context.Set<SportSpace>().Where(s => s.UserId == UserId);
    }
}
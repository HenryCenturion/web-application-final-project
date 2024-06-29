using dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Configuration;
using dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Repositories;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Suscriptions.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace dtaquito_backend_web_app.Suscriptions.Infrastructure.Persistance.EFC.Repositories;

public class SuscriptionRepository : BaseRepository<Suscription>, ISuscriptionRepository
{
    public SuscriptionRepository(AppDBContext context) : base(context) { }

    public async Task<Suscription?> FindByUserId(int userId)
    {
        return await Context.Set<Suscription>()
            .Where(suscription => suscription.UserId == userId)
            .FirstOrDefaultAsync();
    }
    
    public IQueryable<Suscription> GetAll()
    {
        return Context.Set<Suscription>();
    }
}
using dtaquito_backend_web_app.Suscriptions.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.Queries;
using dtaquito_backend_web_app.Suscriptions.Domain.Repositories;
using dtaquito_backend_web_app.Suscriptions.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace dtaquito_backend_web_app.Suscriptions.Application.QueryServices;

public class SuscriptionQueryService : ISuscriptionQueryService
{
    private readonly ISuscriptionRepository suscriptionRepository;
    
    public SuscriptionQueryService(ISuscriptionRepository suscriptionRepository)
    {
        this.suscriptionRepository = suscriptionRepository;
    }
    
    public async Task<Suscription?> Handle(GetSuscriptionByIdQuery query)
    {
        return await suscriptionRepository
            .GetAll()
            .Include(suscription => suscription.User)
            .ThenInclude(user => user.Role) // Include the Role
            .FirstOrDefaultAsync(suscription => suscription.Id == query.Id);
    }
    
    public async Task<Suscription?> GetSuscriptionByUserId(long userId)
    {
        return await suscriptionRepository
            .GetAll()
            .Include(suscription => suscription.User)
            .ThenInclude(user => user.Role) // Include the Role
            .FirstOrDefaultAsync(suscription => suscription.UserId == userId);
    }
}
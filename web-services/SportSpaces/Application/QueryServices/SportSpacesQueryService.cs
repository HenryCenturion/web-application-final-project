using dtaquito_backend_web_app.SportSpaces.Domain.Model.Aggregates;
using dtaquito_backend_web_app.SportSpaces.Domain.Model.Queries;
using dtaquito_backend_web_app.SportSpaces.Domain.Repositories;
using dtaquito_backend_web_app.SportSpaces.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace dtaquito_backend_web_app.SportSpaces.Application.QueryServices;

public class SportSpacesQueryService : ISportSpacesQueryService
{
    private readonly ISportSpacesRepository sportSpacesRepository;
    
    public SportSpacesQueryService(ISportSpacesRepository sportSpacesRepository)
    {
        this.sportSpacesRepository = sportSpacesRepository;
    }
    
    public async Task<SportSpace?> Handle(GetSportSpacesByIdQuery query)
    {
        return await sportSpacesRepository
            .GetAll()
            .Include(sportSpace => sportSpace.User)
            .FirstOrDefaultAsync(sportSpace => sportSpace.Id == query.Id);
    }

    public async Task<IEnumerable<SportSpace>> GetAllSportSpaces()
    {
        return await sportSpacesRepository
            .GetAll()
            .Include(sportSpace => sportSpace.User)
            .ToListAsync();
    }

    public async Task<SportSpace?> Handle(GetSportSpacesByUserIdQuery query)
    {
        return await sportSpacesRepository
           .GetAll()
           .Include(sportSpace => sportSpace.User)
           .FirstOrDefaultAsync(sportSpace => sportSpace.UserId == query.UserId);
    }
}
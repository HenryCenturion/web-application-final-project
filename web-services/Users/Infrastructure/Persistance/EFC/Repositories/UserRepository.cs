using dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Configuration;
using dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Repositories;
using dtaquito_backend_web_app.Users.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Users.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace dtaquito_backend_web_app.Users.Infrastructure.Persistance.EFC.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDBContext context) : base(context)
    {
    }

    public async Task<User?> FindByName(string name)
    {
        return await Context.Set<User>()
            .Where(user => user.Name == name)
            .FirstOrDefaultAsync();
    }

    public async Task<User?> FindByEmailAndPassword(string email, string password)
    {
        return await Context.Set<User>()
            .Where(user => user.Email == email && user.Password == password)
            .FirstOrDefaultAsync();
    }
}
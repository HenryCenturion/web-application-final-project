using dtaquito_backend_web_app.Users.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Shared.Domain.Repositories;

namespace dtaquito_backend_web_app.Users.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByName(string name);
    Task<User?> FindByEmailAndPassword(string email, string password);
}
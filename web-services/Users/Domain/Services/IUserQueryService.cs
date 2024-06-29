using dtaquito_backend_web_app.Users.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Users.Domain.Model.Queries;

namespace dtaquito_backend_web_app.Users.Domain.Services;

public interface IUserQueryService
{
    Task<User> Handle(GetUsersByIdQuery query);
    Task<int?> GetUserRoleById(int id);

    Task<string?> GetUserPlanById(int userId);
    Task<User> GetUserById(int id);
    Task<User> GetUserByEmailAndPassword(string email, string password);
}
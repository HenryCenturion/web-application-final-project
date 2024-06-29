using dtaquito_backend_web_app.Users.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Users.Domain.Model.Commands;

namespace dtaquito_backend_web_app.Users.Domain.Services;

public interface IUserCommandService
{
    Task<User?> Handle(CreateUserCommand command);

    Task<User?> HandleUpdate(int id, CreateUserCommand command);
}
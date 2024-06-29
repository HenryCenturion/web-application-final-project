using dtaquito_backend_web_app.Users.Domain.Model.Commands;
using dtaquito_backend_web_app.Users.Interfaces.REST.Resources;

namespace dtaquito_backend_web_app.Users.Interfaces.REST.Transform;

public class CreateUserCommandFromResourceAssembler
{
    public static CreateUserCommand ToCommandFromResource(CreateUserResource resource)
    {
        return new CreateUserCommand(resource.Name, resource.RoleId, resource.Email, resource.Password);
    }
}
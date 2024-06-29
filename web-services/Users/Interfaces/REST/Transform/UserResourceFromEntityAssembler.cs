using dtaquito_backend_web_app.Users.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Users.Interfaces.REST.Resources;

namespace dtaquito_backend_web_app.Users.Interfaces.REST.Transform;

public class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
    {
        return new UserResource(user.Id, user.Name, user.RoleId, user.Email, user.Password);
    }
}
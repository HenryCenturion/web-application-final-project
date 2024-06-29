namespace dtaquito_backend_web_app.Users.Domain.Exceptions;

public class UserWithRoleNoEqualToRorP : Exception
{
    public UserWithRoleNoEqualToRorP(string message) : base(message)
    {
    }
}
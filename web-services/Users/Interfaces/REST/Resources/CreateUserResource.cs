namespace dtaquito_backend_web_app.Users.Interfaces.REST.Resources;

public record CreateUserResource(string Name, int RoleId, string Email, string Password);
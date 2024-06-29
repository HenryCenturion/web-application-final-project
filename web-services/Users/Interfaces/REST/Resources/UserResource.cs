using dtaquito_backend_web_app.Users.Domain.Model.ValueObjects;

namespace dtaquito_backend_web_app.Users.Interfaces.REST.Resources;

public record UserResource(int Id, string Name, int RoleId, string Email, string Password);
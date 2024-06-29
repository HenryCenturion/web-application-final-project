namespace dtaquito_backend_web_app.Users.Domain.Model.Commands;

public record CreateUserCommand(string Name, 
    int RoleId, string Email, string Password);
using System.ComponentModel.DataAnnotations.Schema;
using dtaquito_backend_web_app.Users.Domain.Model.Commands;
using dtaquito_backend_web_app.Users.Domain.Model.Entities;
using dtaquito_backend_web_app.Users.Domain.Model.ValueObjects;


namespace dtaquito_backend_web_app.Users.Domain.Model.Aggregates;

public partial class User : UserAudit
{
    public int Id { get; }
    
    public string Name { get; set; }
    
    [Column("role_id")]
    public int RoleId { get; set; }
    
    [ForeignKey("RoleId")]
    public Role Role { get; set; }

    public string Email { get; set; }
    
    public string Password { get; set; }

    protected User() {}

    public User(CreateUserCommand command, int roleId){
        
        this.Name = command.Name;
        this.Email = command.Email;
        this.Password = command.Password;
        this.RoleId = roleId;
    }
    
}
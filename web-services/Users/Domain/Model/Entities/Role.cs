using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using dtaquito_backend_web_app.Users.Domain.Model.Aggregates;
using Newtonsoft.Json;

namespace dtaquito_backend_web_app.Users.Domain.Model.Entities;

[Table("role_types")]
public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Type { get; set; } // Change this to string
    
    public Role() {}
    public Role(int id, string type) // Change this to string
    {
        Id = id;
        Type = type;
    }
    
    [JsonIgnore]
    public ICollection<User> Users { get; set; }
}
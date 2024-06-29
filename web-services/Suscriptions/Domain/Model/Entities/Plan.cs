using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.Aggregates;

namespace dtaquito_backend_web_app.Suscriptions.Domain.Model.Entities;

[Table("plan_types")]
public class Plan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public string Type { get; set; }
    
    public Plan() {}
    
    public Plan(int id, string type)
    {
        Id = id;
        Type = type;
    }
    
    public ICollection<Suscription> Suscriptions { get; set; }
}
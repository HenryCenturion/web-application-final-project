using System.ComponentModel.DataAnnotations.Schema;
using dtaquito_backend_web_app.SportSpaces.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.Entities;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.ValueObjects;
using dtaquito_backend_web_app.Users.Domain.Model.Aggregates;

namespace dtaquito_backend_web_app.Suscriptions.Domain.Model.Aggregates;

[Table("suscriptions")]
public class Suscription
{
    public int Id { get; }
    
    public PlanTypes Plan { get; set; }
    
    [Column("user_id")]
    public int UserId { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; }

    protected Suscription(){}
    
    public Suscription(User user, PlanTypes plan) // Cambia el tipo del parámetro plan a PlanTypes
    {
        User = user;
        Plan = plan;
    }

    public void Update(int userId, PlanTypes plan)
    {
        UserId = userId;
        Plan = plan;
    }
}
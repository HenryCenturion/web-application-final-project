using System.ComponentModel.DataAnnotations.Schema;
using dtaquito_backend_web_app.Reservations.Domain.Model.Commands;
using dtaquito_backend_web_app.SportSpaces.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Users.Domain.Model.Aggregates;

namespace dtaquito_backend_web_app.Reservations.Domain.Model.Aggregates;

[Table("reservations")]
public class Reservation
{
    public int Id { get; set; }

    public DateTime Time { get; set; }
    
    public int Hours { get; set; }
    
    [Column("user_id")]
    public int UserId { get; set; }
    
    [ForeignKey("UserId")]
    public User User { get; set; }
    
    [Column("sport_space_id")]
    public int SportSpaceId { get; set; }
    
    [ForeignKey("SportSpaceId")]
    public SportSpace SportSpace { get; set; }
    
    protected Reservation(){}
    
    public Reservation(CreateReservationCommand comnmand, User user, SportSpace sportSpace)
    {
        Time = comnmand.Time;
        Hours = comnmand.Hours;
        User = user;
        SportSpace = sportSpace;
    }
}
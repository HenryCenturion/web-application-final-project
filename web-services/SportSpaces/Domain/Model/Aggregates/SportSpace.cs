using System.ComponentModel.DataAnnotations.Schema;
using dtaquito_backend_web_app.SportSpaces.Domain.Model.Commands;
using dtaquito_backend_web_app.Users.Domain.Model.Aggregates;

namespace dtaquito_backend_web_app.SportSpaces.Domain.Model.Aggregates;

[Table("sport_spaces")]
public class SportSpace
{
    public int Id { get; }
    
    public string Name { get; private set; }
    
    public string ImageUrl { get; private set; }
    
    public int Price { get; private set; }
    
    public string Description { get; private set; }
    
    [Column("user_id")]
    public int UserId { get; set; } // This is the foreign key property

    [ForeignKey("UserId")] // Use the name of the foreign key property here
    public User User { get; set; }
    
    public string StartTime { get; private set; }
    
    public string EndTime { get; private set; }
    
    public int Rating { get; private set; }
    
    protected SportSpace(){
    
        this.Name= string.Empty;
        this.ImageUrl= string.Empty;
        this.Price= 0;
        this.Description= string.Empty;
        this.StartTime= string.Empty;
        this.EndTime= string.Empty;
        this.Rating= 0;
    }
    
    public SportSpace(CreateSportSpacesCommand command, User user){
        Name = command.Name;
        ImageUrl = command.ImageUrl;
        Price = command.Price;
        Description = command.Description;
        User = user;
        StartTime = command.StartTime;
        EndTime = command.EndTime;
        Rating = command.Rating;
    }
    
    public void Update(CreateSportSpacesCommand command){
        Name = command.Name;
        ImageUrl = command.ImageUrl;
        Price = command.Price;
        Description = command.Description;
        StartTime = command.StartTime;
        EndTime = command.EndTime;
    }
    
    public void UpdateRating(int rating){
        Rating = rating;
    }
}
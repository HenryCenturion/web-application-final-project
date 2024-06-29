using dtaquito_backend_web_app.SportSpaces.Domain.Model.Commands;
using dtaquito_backend_web_app.SportSpaces.Interfaces.REST.Resources;

namespace dtaquito_backend_web_app.SportSpaces.Interfaces.REST.Transform;

public class CreateSportSpacesCommandFromResourceAssembler
{
    public static CreateSportSpacesCommand ToCommandFromResource(CreateSportSpacesResource resource)
    {
        return new CreateSportSpacesCommand(resource.Name, resource.ImageUrl, resource.Price, resource.Description, resource.UserId, resource.StartTime, resource.EndTime, resource.Rating);
    }
}
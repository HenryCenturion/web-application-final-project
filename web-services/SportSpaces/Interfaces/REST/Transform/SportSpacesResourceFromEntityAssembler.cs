using dtaquito_backend_web_app.SportSpaces.Domain.Model.Aggregates;
using dtaquito_backend_web_app.SportSpaces.Interfaces.REST.Resources;

namespace dtaquito_backend_web_app.SportSpaces.Interfaces.REST.Transform;

public class SportSpacesResourceFromEntityAssembler
{
    public static SportSpacesResource ToResourceFromEntity(SportSpace entity)
    {
        return new SportSpacesResource(entity.Id, entity.Name, entity.ImageUrl, entity.Price, entity.Description, entity.User, entity.StartTime, entity.EndTime, entity.Rating);
    }
}
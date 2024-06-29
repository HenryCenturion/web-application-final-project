using dtaquito_backend_web_app.Suscriptions.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.Commands;

namespace dtaquito_backend_web_app.Suscriptions.Domain.Services;

public interface ISuscriptionCommandService
{
    Task<(Suscription? Suscription, string? ErrorMessage)> Handle(CreateSuscriptionCommand command, int userId); 
}
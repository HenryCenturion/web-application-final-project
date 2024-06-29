using dtaquito_backend_web_app.Suscriptions.Domain.Model.ValueObjects;

namespace dtaquito_backend_web_app.Suscriptions.Interfaces.REST.Resources;

public record CreateSuscriptionResource(int PlanId, int UserId);
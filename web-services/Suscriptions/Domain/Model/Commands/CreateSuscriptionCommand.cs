using dtaquito_backend_web_app.SportSpaces.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.Entities;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.ValueObjects; // Ensure you import the namespace where PlanTypes is defined

namespace dtaquito_backend_web_app.Suscriptions.Domain.Model.Commands;

public record CreateSuscriptionCommand(PlanTypes Plan, int UserId);
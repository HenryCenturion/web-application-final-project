using dtaquito_backend_web_app.Shared.Domain.Repositories;
using dtaquito_backend_web_app.SportSpaces.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.Commands;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.ValueObjects;
using dtaquito_backend_web_app.Suscriptions.Domain.Repositories;
using dtaquito_backend_web_app.Suscriptions.Domain.Services;
using dtaquito_backend_web_app.Users.Domain.Repositories;

namespace dtaquito_backend_web_app.Suscriptions.Application.CommandServices;

public class SuscriptionCommandService(ISuscriptionRepository suscriptionRepository, IUnitOfWork unitOfWork, IUserRepository userRepository) : ISuscriptionCommandService
{
    
        public async Task<(Suscription? Suscription, string? ErrorMessage)> Handle(CreateSuscriptionCommand command, int userId)
        {
            Console.WriteLine($"Buscando usuario con ID: {command.UserId} ");
            var user = await userRepository.FindByIdAsync(command.UserId);
            if (user == null)
            {
                return (null, $"User with id {command.UserId} not found");
            }
            Console.WriteLine($"Usuario encontrado: {user}");
    
            var suscription = new Suscription(user, PlanTypes.Free);
    
            try
            {
                await suscriptionRepository.AddAsync(suscription);
                await unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return (null, e.Message);
            }
            return (suscription, null);
        }
    
        public async Task<Suscription?> HandleUpdate(CreateSuscriptionCommand command, int id)
        {
            var suscription = await suscriptionRepository.FindByIdAsync(id);
            if (suscription == null)
            {
                return null;
            }

            suscription.Update(command.UserId, command.Plan);
    
            try
            {
                suscriptionRepository.Update(suscription);
                await unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                return null;
            }
    
            return suscription;
        }
}

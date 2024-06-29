using dtaquito_backend_web_app.Shared.Domain.Repositories;
using dtaquito_backend_web_app.SportSpaces.Domain.Model.Aggregates;
using dtaquito_backend_web_app.SportSpaces.Domain.Model.Commands;
using dtaquito_backend_web_app.SportSpaces.Domain.Repositories;
using dtaquito_backend_web_app.SportSpaces.Domain.Services;
using dtaquito_backend_web_app.Users.Domain.Repositories;

namespace dtaquito_backend_web_app.SportSpaces.Application.CommandServices;

public class SportSpacesCommandService(ISportSpacesRepository sportSpacesRepository, IUnitOfWork unitOfWork, IUserRepository userRepository) : ISportSpacesCommandService
{

    public async Task<(SportSpace? SportSpace, string? ErrorMessage)> Handle(CreateSportSpacesCommand command, int userId)
    {
        Console.WriteLine($"Buscando usuario con nombre: {command.Name} ");
        var user = await userRepository.FindByIdAsync(command.UserId);
        if (user == null)
        {
            return (null, $"User with id {command.UserId} not found");
        }
        Console.WriteLine($"Usuario encontrado: {user}");

        var sportSpace = new SportSpace(command, user);

        try
        {
            await sportSpacesRepository.AddAsync(sportSpace);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return (null, e.Message);
        }
        return (sportSpace, null);
    }

    public async Task<SportSpace?> HandleUpdate(CreateSportSpacesCommand command, int id)
    {
        var sportSpace = await sportSpacesRepository.FindByIdAsync(id);
        if (sportSpace == null)
        {
            return null;
        }

        sportSpace.Update(command);

        try
        {
            sportSpacesRepository.Update(sportSpace);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            return null;
        }
  
        return sportSpace;
    }

    public async Task Delete(int id)
    {
        var sportSpace = await sportSpacesRepository.FindByIdAsync(id);
        if (sportSpace == null)
        {
            throw new ArgumentException("SportSpace not found");
        }

        sportSpacesRepository.Remove(sportSpace);
        await unitOfWork.CompleteAsync();
    }
}
using dtaquito_backend_web_app.Users.Domain.Repositories;
using dtaquito_backend_web_app.Users.Domain.Services;
using dtaquito_backend_web_app.Users.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Users.Domain.Model.Commands;
using dtaquito_backend_web_app.Shared.Domain.Repositories;
using dtaquito_backend_web_app.Users.Domain.Model.Entities;
using dtaquito_backend_web_app.Users.Domain.Model.ValueObjects;
using dtaquito_backend_web_app.Users.Infrastructure.Persistance.EFC.Repositories;

namespace dtaquito_backend_web_app.Users.Application.CommandServices;

public class UserCommandService(IUserRepository userRepository, 
    IUnitOfWork unitOfWork) : IUserCommandService
{
   public async Task<User?> Handle(CreateUserCommand command)
    {
        Console.WriteLine($"Buscando usuario con nombre: {command.Name}");
        var existingUser = await userRepository.FindByName(command.Name);
        Console.WriteLine($"Usuario encontrado: {existingUser}");

        if (existingUser != null)
        {
            throw new Exception("User already exists");
        }

        // Crear un nuevo usuario si existingUser es null
        var newUser = new User(command, command.RoleId);

        try
        {
            await userRepository.AddAsync(newUser);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            // Log the error if needed
            Console.WriteLine(ex.Message);
            return null;
        }

        return newUser;
    }
    
    public async Task<User?> HandleUpdate(int id, CreateUserCommand command)
    {
        // Buscar el usuario existente
        var existingUser = await userRepository.FindByIdAsync(id);
        if (existingUser == null)
        {
            // Si no se encuentra el usuario, devolver null
            return null;
        }

        // Actualizar las propiedades del usuario
        // Actualizar las propiedades del usuario si los valores no son nulos
        existingUser.Name = command.Name ?? existingUser.Name;
        existingUser.Email = command.Email ?? existingUser.Email;
        existingUser.Password = command.Password ?? existingUser.Password;

        // Asegúrate de actualizar todas las propiedades necesarias

        try
        {
            userRepository.Update(existingUser);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            // Si hay un error al guardar, devolver null
            return null;
        }

        // Devolver el usuario actualizado
        return existingUser;

    }

}


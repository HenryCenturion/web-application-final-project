using dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Configuration;
using dtaquito_backend_web_app.Users.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Users.Domain.Model.Queries;
using dtaquito_backend_web_app.Users.Domain.Services;
using dtaquito_backend_web_app.Users.Interfaces.REST.Resources;
using dtaquito_backend_web_app.Users.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text;
using dtaquito_backend_web_app.SportSpaces.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.Commands;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.Entities;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.ValueObjects;
using dtaquito_backend_web_app.Suscriptions.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace dtaquito_backend_web_app.Users.Interfaces.REST;

[ApiController]
[Route("api/v1/users")]
[Produces(MediaTypeNames.Application.Json)]

public class UserController : ControllerBase
{
    private readonly IUserCommandService _userCommandService;
    private readonly AppDBContext _dbContext;
    private readonly ISuscriptionCommandService _suscriptionCommandService;
    private readonly IUserQueryService _userQueryService;
    
    public UserController(IUserCommandService userCommandService, AppDBContext dbContext, ISuscriptionCommandService suscriptionCommandService, IUserQueryService userQueryService)
    {
        _userCommandService = userCommandService;
        _dbContext = dbContext;
        _suscriptionCommandService = suscriptionCommandService;
        _userQueryService = userQueryService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserResource createUserResource)
    {
        Console.WriteLine("Starting CreateUser method...");

        var createUserCommand = CreateUserCommandFromResourceAssembler.ToCommandFromResource(createUserResource);
        var user = await _userCommandService.Handle(createUserCommand);

        if (user == null)
        {
            Console.WriteLine("User creation failed.");
            return BadRequest();
        }
        try
        {
            Console.WriteLine("Checking if 'suscriptions' table exists...");
            var tableExists = await _dbContext.Suscriptions.FromSqlRaw("SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'suscriptions'").AnyAsync();

            Console.WriteLine("'suscriptions' table exists: " + tableExists);

            // Create a new subscription for the user
            CreateSuscriptionCommand command = new CreateSuscriptionCommand(PlanTypes.Free, user.Id);

            if (!tableExists)
            {
                Console.WriteLine("'suscriptions' table does not exist. Creating...");
                await _dbContext.Database.EnsureCreatedAsync();
            }

            Console.WriteLine("Handling subscription command...");
            await _suscriptionCommandService.Handle(command, user.Id);

            Console.WriteLine("Saving changes to the database...");
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            return StatusCode(500, "An error occurred while creating the subscription.");
        }
        
        var resource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        Console.WriteLine("User created successfully. Returning response...");
        return CreatedAtAction(nameof(GetUsersById), new { id = resource.Id }, resource);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsersById(int id)
    {
        var user = await _userQueryService.Handle(new GetUsersByIdQuery(id));
        if (user == null) return NotFound();
        var resource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(resource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] CreateUserResource updateUserResource)
    {
        // Obtén el usuario que deseas actualizar
        var user = await _userQueryService.Handle(new GetUsersByIdQuery(id));
        if (user == null)
        {
            return NotFound();
        }

        // Actualiza los campos del usuario
        var updatedUserTask = _userCommandService.HandleUpdate(id, CreateUserCommandFromResourceAssembler.ToCommandFromResource(updateUserResource));

        return await updatedUserTask.ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                return StatusCode(500);
            }
            return Ok();
        });
    }

    [HttpGet("")]
    public async Task<IActionResult> GetUserByEmailAndPassword([FromQuery] string email, [FromQuery] string password)
    {
        var user = await _userQueryService.GetUserByEmailAndPassword(email, password);
        if (user == null)
        {
            return NotFound();
        }
        var resource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(resource);
    }

}

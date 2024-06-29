using System.Net;
using System.Net.Mime;
using dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Configuration;
using dtaquito_backend_web_app.SportSpaces.Domain.Model.Queries;
using dtaquito_backend_web_app.SportSpaces.Domain.Services;
using dtaquito_backend_web_app.SportSpaces.Interfaces.REST.Resources;
using dtaquito_backend_web_app.SportSpaces.Interfaces.REST.Transform;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.ValueObjects;
using dtaquito_backend_web_app.Users.Domain.Model.ValueObjects;
using dtaquito_backend_web_app.Users.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace dtaquito_backend_web_app.SportSpaces.Interfaces.REST;

[ApiController]
[Route("api/v1/sport-spaces")]
[Produces(MediaTypeNames.Application.Json)]
public class SportSpacesController: ControllerBase
{
    private readonly ISportSpacesCommandService sportSpacesCommandService;
    private readonly ISportSpacesQueryService sportSpacesQueryService;
    private readonly IUserQueryService queryService;
    private readonly AppDBContext _dbContext;
    private readonly ILogger<SportSpacesController> _logger;


    public SportSpacesController(ISportSpacesCommandService sportSpacesCommandService, ISportSpacesQueryService sportSpacesQueryService, IUserQueryService queryService, AppDBContext dbContext, ILogger<SportSpacesController> logger)
    {
        this.sportSpacesCommandService = sportSpacesCommandService;
        this.sportSpacesQueryService = sportSpacesQueryService;
        this.queryService = queryService;
        _dbContext = dbContext;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateSportSpace([FromBody] CreateSportSpacesResource createSportSpaceResource)
    {
        int userId = createSportSpaceResource.UserId; // Get userId from createSportSpaceResource
        int? userRoleId = await queryService.GetUserRoleById(userId);
        string? userPlanIdString = await queryService.GetUserPlanById(userId); // Get user's plan

        _logger.LogInformation($"User role ID: {userRoleId}");
        _logger.LogInformation($"User plan ID string: {userPlanIdString}");

        if (userRoleId == null || userRoleId != (int)RoleTypes.P)
        {
            _logger.LogInformation($"Forbidden access for user with ID: {userId} and role ID: {userRoleId}");
            return BadRequest(new { error = $"Access denied for user with ID: {userId}. User does not have the required role." });
        }
        _logger.LogInformation($"User role ID: {userRoleId}");
        _logger.LogInformation($"Expected role ID for P: {(int)RoleTypes.P}");

        if (userPlanIdString == null || !Enum.TryParse<PlanTypes>(userPlanIdString, out PlanTypes userPlan) || userPlan != PlanTypes.Premium)
        {
            _logger.LogInformation($"Forbidden access for user with ID: {userId} and plan ID: {userPlanIdString}");
            return BadRequest(new { error = $"Access denied for user with ID: {userId}. User does not have the required plan." });
        }

        _logger.LogInformation($"User plan: {userPlan}");
        _logger.LogInformation($"Expected plan for Premium: {PlanTypes.Premium}");

        var (sportSpace, errorMessage) = await sportSpacesCommandService.Handle(CreateSportSpacesCommandFromResourceAssembler.ToCommandFromResource(createSportSpaceResource), userId);
        if (errorMessage != null)
        {
            _logger.LogError($"Error creating sport space: {errorMessage}");
            return NotFound(errorMessage);
        }
        var resource = SportSpacesResourceFromEntityAssembler.ToResourceFromEntity(sportSpace);
        return CreatedAtAction(nameof(GetSportSpacesById), new { id = sportSpace.Id }, resource);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSportSpacesById(int id)
    {
        try
        {
            var sportSpace = await sportSpacesQueryService.Handle(new GetSportSpacesByIdQuery(id));
            if (sportSpace == null) return NotFound();
            var resource = SportSpacesResourceFromEntityAssembler.ToResourceFromEntity(sportSpace);
            return Ok(resource);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting sport space by id {id}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSportSpaces(int id, [FromBody] CreateSportSpacesResource updateSportSpacesResource)
    {
        if (sportSpacesQueryService == null || _dbContext == null)
        {
            return BadRequest("Services are not initialized.");
        }

        var sportSpace = await sportSpacesQueryService.Handle(new GetSportSpacesByIdQuery(id));
        if (sportSpace == null)
        {
            return NotFound();
        }

        var updateSportSpacesCommand = CreateSportSpacesCommandFromResourceAssembler.ToCommandFromResource(updateSportSpacesResource);
        if (updateSportSpacesCommand == null)
        {
            return BadRequest("Invalid updateSportSpacesResource.");
        }

        sportSpace.Update(updateSportSpacesCommand);

        await _dbContext.SaveChangesAsync();

        var resource = SportSpacesResourceFromEntityAssembler.ToResourceFromEntity(sportSpace);
        return Ok(resource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSportSpaces(int id)
    {
        
        var sportSpace = await sportSpacesQueryService.Handle(new GetSportSpacesByIdQuery(id));
        if (sportSpace == null)
        {
            return NotFound();
        }
        
        await sportSpacesCommandService.Delete(id);
        
        return NoContent();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllSportSpaces()
    {
        var sportSpaces = await sportSpacesQueryService.GetAllSportSpaces();
        var resources = sportSpaces.Select(SportSpacesResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetSportSpacesByUserId(int userId)
    {
        try
        {
            var sportSpace = await sportSpacesQueryService.Handle(new GetSportSpacesByUserIdQuery(userId));
            if (sportSpace == null) return NotFound();
            var resource = SportSpacesResourceFromEntityAssembler.ToResourceFromEntity(sportSpace);
            return Ok(resource);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting sport space by user id {userId}");
            return StatusCode(500, "Internal server error");
        }
    }

}
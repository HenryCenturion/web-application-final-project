using System.Net.Mime;
using dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Configuration;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.Commands;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.Queries;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.ValueObjects;
using dtaquito_backend_web_app.Suscriptions.Domain.Services;
using dtaquito_backend_web_app.Suscriptions.Interfaces.REST.Resources;
using dtaquito_backend_web_app.Suscriptions.Interfaces.REST.Transform;
using dtaquito_backend_web_app.Users.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dtaquito_backend_web_app.Suscriptions.Interfaces.REST;

[ApiController]
[Route("api/v1/suscriptions")]
[Produces(MediaTypeNames.Application.Json)]

public class SuscriptionController : ControllerBase
{
    private readonly ISuscriptionQueryService _suscriptionQueryService;
    private readonly AppDBContext _dbContext;

    public SuscriptionController(ISuscriptionQueryService suscriptionQueryService, AppDBContext dbContext)
    {
        _suscriptionQueryService = suscriptionQueryService;
        _dbContext = dbContext;

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSuscriptionById(int id)
    {
        var suscription = await _suscriptionQueryService.Handle(new GetSuscriptionByIdQuery(id));
        if (suscription == null) return NotFound();
        var resource = SuscriptionResourceFromEntityAssembler.ToResourceFromEntity(suscription);
        return Ok(resource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSuscription(int id, [FromBody] CreateSuscriptionResource request)
    {
        if (_suscriptionQueryService == null || _dbContext == null)
        {
            return BadRequest("Services are not initialized");
        }

        var suscription = await _suscriptionQueryService.Handle(new GetSuscriptionByIdQuery(id));
        if (suscription == null) return NotFound();

        var updatedSuscriptionCommand = CreateSuscriptionCommandFromResourceAssembler.ToCommandFromResource(request);

        if (updatedSuscriptionCommand == null)
        {
            return BadRequest("invalid update request");
        }

        var plan = await _dbContext.Plans.FirstOrDefaultAsync(p => p.Id == (int)updatedSuscriptionCommand.Plan);        
        if (plan == null)
        {
            return BadRequest("Plan not found");
        }
        
        PlanTypes planType;
        if (!Enum.TryParse(plan.Type, out planType))
        {
            return BadRequest("Invalid plan type");
        }
        
        if (suscription.Plan == PlanTypes.Free && planType == PlanTypes.Premium)
        {
            var payment = await _dbContext.Payments.FirstOrDefaultAsync(p => p.UserId == suscription.UserId);
            if (payment == null)
            {
                return BadRequest("No payment found for the user");
            }

            if (payment.Balance < 70)
            {
                return BadRequest("Insufficient balance");
            }

            payment.Balance -= 70;
            _dbContext.Payments.Update(payment);
            await _dbContext.SaveChangesAsync();
        }

        suscription.Update(request.UserId, (PlanTypes)plan.Id);

        await _dbContext.SaveChangesAsync();

        var resource = SuscriptionResourceFromEntityAssembler.ToResourceFromEntity(suscription);
        return Ok(resource);
    }
}


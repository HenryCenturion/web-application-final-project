using System.Net.Mime;
using dtaquito_backend_web_app.Payments.Domain.Model.Queries;
using dtaquito_backend_web_app.Payments.Domain.Services;
using dtaquito_backend_web_app.Payments.Interfaces.REST.Resources;
using dtaquito_backend_web_app.Payments.Interfaces.REST.Transform;
using dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Configuration;
using dtaquito_backend_web_app.Users.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace dtaquito_backend_web_app.Payments.Interfaces.REST;

[ApiController]
[Route("api/v1/payments")]
[Produces(MediaTypeNames.Application.Json)]

public class PaymentController : ControllerBase
{
    private readonly IPaymentCommandService paymentCommandService;
    private readonly IPaymentQueryService paymentQueryService;
    private readonly IUserQueryService userQueryService;
    private readonly ILogger<PaymentController> _logger;
    
    public PaymentController(IPaymentCommandService paymentCommandService, IPaymentQueryService paymentQueryService, IUserQueryService userQueryService, ILogger<PaymentController> logger)
    {
        this.paymentCommandService = paymentCommandService;
        this.paymentQueryService = paymentQueryService;
        this.userQueryService = userQueryService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentResource createPaymentResource)
    {
        int userId = createPaymentResource.UserId; // Get userId from createPaymentResource
        int? userRoleId = await userQueryService.GetUserRoleById(userId);
        string? userPlanIdString = await userQueryService.GetUserPlanById(userId);
        
        _logger.LogInformation($"User role ID: {userRoleId}");
        _logger.LogInformation($"User plan ID string: {userPlanIdString}");
        
        var resource = CreatePaymentCommandFromResourceAssembler.ToCommandFromResource(createPaymentResource);
        var (payment, errorMessage) = await paymentCommandService.Handle(resource, userId);
        return CreatedAtAction(nameof(GetPaymentById), new { id = payment?.Id }, resource);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPaymentById(int id)
    {
        var payment = await paymentQueryService.Handle(new GetPaymentByIdQuery(id));
        if (payment == null) return NotFound();
        var resource = PaymentResourceFromEntityAssembler.ToResourceFromEntity(payment);
        return Ok(resource);
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllPayments()
    {
        var payments = await paymentQueryService.GetAllPayments();
        if (payments == null) return NotFound();
        var resources = PaymentResourceFromEntityAssembler.ToResourceFromEntity(payments);
        return Ok(resources);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePayment(int id)
    {
        await paymentCommandService.Delete(id);
        return NoContent();
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetPaymentByUserId(int userId)
    {
        var payment = await paymentQueryService.GetPaymentByUserId(userId);
        if (payment == null) return NotFound();
        var resource = PaymentResourceFromEntityAssembler.ToResourceFromEntity(payment);
        return Ok(resource);
    }


}
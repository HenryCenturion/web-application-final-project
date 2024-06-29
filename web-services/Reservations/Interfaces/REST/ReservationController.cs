using System.Net.Mime;
using dtaquito_backend_web_app.Payments.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Payments.Domain.Services;
using dtaquito_backend_web_app.Reservations.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Reservations.Domain.Model.Queries;
using dtaquito_backend_web_app.Reservations.Domain.Services;
using dtaquito_backend_web_app.Reservations.Interfaces.REST.Resources;
using dtaquito_backend_web_app.Reservations.Interfaces.REST.Transform;
using dtaquito_backend_web_app.SportSpaces.Domain.Model.Aggregates;
using dtaquito_backend_web_app.SportSpaces.Domain.Model.Queries;
using dtaquito_backend_web_app.SportSpaces.Domain.Services;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Suscriptions.Domain.Model.ValueObjects;
using dtaquito_backend_web_app.Suscriptions.Domain.Services;
using dtaquito_backend_web_app.Users.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Users.Domain.Model.ValueObjects;
using dtaquito_backend_web_app.Users.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace dtaquito_backend_web_app.Reservations.Interfaces.REST;

[ApiController]
[Route("api/v1/reservations")]
[Produces(MediaTypeNames.Application.Json)]

public class ReservationController : ControllerBase
{
    private readonly IReservationCommandService reservationCommandService;
    private readonly IReservationQueryService reservationQueryService;
    private readonly ISportSpacesQueryService sportSpaceQueryService;
    private readonly IPaymentQueryService paymentsQueryService;
    private readonly IUserQueryService userQueryService;
    private readonly ISuscriptionQueryService suscriptionsQueryService;
    private readonly IPaymentCommandService paymentsCommandService;
    private readonly ILogger<ReservationController> _logger;
    
    
    public ReservationController(IReservationCommandService reservationCommandService, IReservationQueryService reservationQueryService, ISportSpacesQueryService sportSpaceQueryService, IUserQueryService userQueryService, ILogger<ReservationController> logger,
        IPaymentQueryService paymentsQueryService, ISuscriptionQueryService suscriptionsQueryService, IPaymentCommandService paymentsCommandService)
    {
        this.reservationCommandService = reservationCommandService;
        this.reservationQueryService = reservationQueryService;
        this.sportSpaceQueryService = sportSpaceQueryService;
        this.userQueryService = userQueryService;
        this.paymentsQueryService = paymentsQueryService;
        this.suscriptionsQueryService = suscriptionsQueryService;
        this.paymentsCommandService = paymentsCommandService;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationResource resource)
    {
        if (resource == null)
        {
            return BadRequest("Resource is null");
        }

        int userId = resource.UserId;

        User userOptional = await userQueryService.GetUserById(resource.UserId);

        if (userOptional == null)
        {
            _logger.LogWarning("User with id {0} not found", userId);
            return BadRequest("User not found or user role is null");
        }

        if (userOptional.Role == null)
        {
            _logger.LogWarning("Role of user with id {0} is null", userId);
            return BadRequest("User not found or user role is null");
        }

        Payment? paymentOptional = await paymentsQueryService.GetPaymentByUserId(userOptional.Id);

        if (paymentOptional == null)
        {
            return BadRequest("Payment not found");
        }

        double balance = paymentOptional.Balance;

        SportSpace? sportSpacesOptional = await sportSpaceQueryService.Handle(new GetSportSpacesByIdQuery(resource.SportSpaceId));

        if (sportSpacesOptional == null)
        {
            return BadRequest("SportSpaces not found");
        }

        Suscription? subscriptionOptional = await suscriptionsQueryService.GetSuscriptionByUserId(userId);

        if (subscriptionOptional == null)
        {
            return BadRequest("Subscription not found");
        }

        PlanTypes subscriptionPlan = subscriptionOptional.Plan;

        SportSpace sportSpaces = sportSpacesOptional;

        DateTime reservationDateTime = resource.Time.ToUniversalTime();

        // Parse the sport space's available time range
        TimeSpan sportSpacesStartTime = TimeSpan.Parse(sportSpaces.StartTime);
        TimeSpan sportSpacesEndTime = TimeSpan.Parse(sportSpaces.EndTime);

        // Check if the reservation time is within the sport space's available time range
        if (reservationDateTime.TimeOfDay < sportSpacesStartTime || reservationDateTime.TimeOfDay > sportSpacesEndTime)
        {
            return BadRequest("Reservation time is not within the allowed range");
        }

        // Calculate the reservation end time
        DateTime reservationEndTime = reservationDateTime.AddHours(resource.Hours);

        // Check if the reservation end time is within the sport space's available time range
        if (reservationEndTime.TimeOfDay < sportSpacesStartTime || reservationEndTime.TimeOfDay > sportSpacesEndTime)
        {
            return BadRequest("Reservation duration is not within the allowed range");
        }

        // Get existing reservations for the sport space
        List<Reservation> existingReservations = await reservationQueryService.GetReservationBySportSpacesId(resource.SportSpaceId);

        // Check if the time slot is already reserved
        bool isAlreadyReserved = existingReservations.Any(reservation =>
        {
            DateTime existingReservationTime = reservation.Time.ToUniversalTime();
            return existingReservationTime.Date == reservationDateTime.Date && existingReservationTime.TimeOfDay == reservationDateTime.TimeOfDay;
        });

        if (isAlreadyReserved)
        {
            return BadRequest("Reservation time is already reserved");
        }

        CreateReservationResource newResource = new CreateReservationResource(reservationDateTime, resource.Hours, resource.UserId, resource.SportSpaceId);

        // Calculate the reservation end time

        // Check if the reservation end time is within the sport space's available time range
        if (reservationEndTime.TimeOfDay < sportSpacesStartTime || reservationEndTime.TimeOfDay > sportSpacesEndTime)
        {
            return BadRequest("Reservation duration is not within the allowed range");
        }

        var result = await reservationCommandService.Handle(userId, CreateReservationCommandFromResourceAssembler.ToCommandFromResource(newResource));

        if (result.reservation != null)
        {
            Reservation reservation = result.reservation;

            if (subscriptionPlan == PlanTypes.Free)
            {
                balance -= sportSpacesOptional.Price;
            }
            else if (subscriptionPlan == PlanTypes.Premium)
            {
                balance -= sportSpacesOptional.Price / 2;
            }

            paymentOptional.Balance = (int)balance;
            await paymentsCommandService.Update(paymentOptional);

            _logger.LogInformation("Reservation created successfully for user {0}", resource.UserId);
            return CreatedAtAction(nameof(GetReservationById), new { id = reservation.Id }, ReservationResourceFromEntityAssembler.ToResourceFromEntity(reservation));
        }
        else
        {
            _logger.LogWarning("Reservation could not be created for user {0}", resource.UserId);
            return BadRequest("Reservation could not be created");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReservationById(int id)
    {
        Reservation? reservationOptional = await reservationQueryService.Handle(new GetReservationByIdQuery(id));
        if (reservationOptional != null)
        {
            return Ok(ReservationResourceFromEntityAssembler.ToResourceFromEntity(reservationOptional));
        }
        else
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        Reservation? reservationOptional = await reservationQueryService.Handle(new GetReservationByIdQuery(id));
        if (reservationOptional != null)
        {
            await reservationCommandService.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllReservations()
    {
        List<Reservation> reservations = await reservationQueryService.GetAllReservations();
        return Ok(ReservationResourceFromEntityAssembler.ToResourceFromEntity(reservations));
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetReservationsByUserId(int userId)
    {
        List<Reservation> reservations = await reservationQueryService.GetReservationsByUserId(userId);
        return Ok(ReservationResourceFromEntityAssembler.ToResourceFromEntity(reservations));
    }


}
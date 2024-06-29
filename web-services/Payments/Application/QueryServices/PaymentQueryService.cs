using dtaquito_backend_web_app.Payments.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Payments.Domain.Model.Commands;
using dtaquito_backend_web_app.Payments.Domain.Model.Queries;
using dtaquito_backend_web_app.Payments.Domain.Repositories;
using dtaquito_backend_web_app.Payments.Domain.Services;
using dtaquito_backend_web_app.Shared.Domain.Repositories;
using dtaquito_backend_web_app.Users.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace dtaquito_backend_web_app.Payments.Application.QueryServices;

public class PaymentQueryService: IPaymentQueryService
{
    private readonly IPaymentRepository paymentRepository;
    
    public PaymentQueryService(IPaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }
    
    public async Task<Payment?> Handle(GetPaymentByIdQuery query)
    {
        return await paymentRepository
            .GetAll()
            .Include(payment => payment.User)
            .FirstOrDefaultAsync(payment => payment.Id == query.Id);
    }
    
    public async Task<Payment?> GetPaymentByUserId(int userId)
    {
        return await paymentRepository
            .GetAll()
            .Include(payment => payment.User)
            .FirstOrDefaultAsync(payment => payment.UserId == userId);
    }

    public async Task<Payment?> GetAllPayments()
    {
        return await paymentRepository
            .GetAll()
            .Include(payment => payment.User)
            .FirstOrDefaultAsync();
    }
}
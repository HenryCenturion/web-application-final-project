using dtaquito_backend_web_app.Payments.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Payments.Domain.Model.Commands;
using dtaquito_backend_web_app.Payments.Domain.Repositories;
using dtaquito_backend_web_app.Payments.Domain.Services;
using dtaquito_backend_web_app.Shared.Domain.Repositories;
using dtaquito_backend_web_app.Users.Domain.Repositories;

namespace dtaquito_backend_web_app.Payments.Application.CommandServices;

public class PaymentCommandService(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork, IUserRepository userRepository) : IPaymentCommandService
{
    public async Task<(Payment? payment, string? ErrorMessage)> Handle(CreatePaymentCommand command, int userId)
    {
        Console.WriteLine($"Buscando usuario con Id: {command.UserId} ");
        var user = await userRepository.FindByIdAsync(command.UserId);
        if (user == null)
        {
            return (null, $"User with id {command.UserId} not found");
        }
        Console.WriteLine($"Usuario encontrado: {user}");

        var payment = new Payment(command, user);
        payment.UpdateBalance(1000);

        try
        {
            await paymentRepository.AddAsync(payment);
            await unitOfWork.CompleteAsync();
        }
        
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return (null, e.Message);
        }
        
        return (payment, null);
    }
    
    public async Task Update(Payment payment)
    {
        paymentRepository.Update(payment);
        await unitOfWork.CompleteAsync();
    }
    
    public async Task Delete(int id)
    {
        var payment = await paymentRepository.FindByIdAsync(id);
        if (payment == null)
        {
            return;
        }
        paymentRepository.Remove(payment);
        await unitOfWork.CompleteAsync();
    }
}
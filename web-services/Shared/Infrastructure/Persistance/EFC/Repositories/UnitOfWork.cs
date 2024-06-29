using dtaquito_backend_web_app.Shared.Domain.Repositories;
using dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Configuration;

namespace dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDBContext _context;
    public UnitOfWork(AppDBContext context) => _context = context;

    public async Task CompleteAsync() => await _context.SaveChangesAsync();
}
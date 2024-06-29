namespace dtaquito_backend_web_app.Shared.Domain.Repositories;

public interface  IUnitOfWork
{
    Task CompleteAsync();
}
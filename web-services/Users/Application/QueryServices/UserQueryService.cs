using dtaquito_backend_web_app.Shared.Infrastructure.Persistance.EFC.Configuration;
using dtaquito_backend_web_app.Users.Domain.Model.Aggregates;
using dtaquito_backend_web_app.Users.Domain.Model.Queries;
using dtaquito_backend_web_app.Users.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace dtaquito_backend_web_app.Users.Domain.Services
{
    public class UserQueryService : IUserQueryService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppDBContext _dbContext;
        private readonly ILogger<UserQueryService> _logger;

        public UserQueryService(IUserRepository userRepository, AppDBContext dbContext, ILogger<UserQueryService> logger)
        {
            _userRepository = userRepository;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<User> Handle(GetUsersByIdQuery query)
        {
            var user = await _userRepository.FindByIdAsync(query.Id);
            return user;
        }
        
        public async Task<int?> GetUserRoleById(int userId)
        {
            var user = await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == userId);
            return user?.Role?.Id;
        }
        
        public async Task<string?> GetUserPlanById(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return null;
            }

            var suscription = await _dbContext.Suscriptions.FirstOrDefaultAsync(s => s.UserId == userId);
            if (suscription == null)
            {
                return null;
            }

            return suscription.Plan.ToString();
        }
        
       public async Task<User> GetUserById(int userId)
        {
            User? user = await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                _logger.LogWarning("User with id {0} not found", userId);
            }
            else if (user.Role == null)
            {
                _logger.LogWarning("Role of user with id {0} is null. User: {1}", userId, user);
            }
            else
            {
                _logger.LogInformation("User with id {0} found, role id: {1}. User: {2}", userId, user.Role.Id, user);
            }

            return user;
        }

        public async Task<User> GetUserByEmailAndPassword(string email, string password)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            return user;
        }
    }
}
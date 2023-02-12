using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PricingEngine.Db;
using PricingEngine.Db.Entities;
using PricingEngine.Models.Requests;

namespace PricingEngine.Repositories
{

    public interface IUserInputRepository
    {
        Task AddUserInputAsync(AddUserInputRequest request);
        Task SaveChangesAsync();
    }

    public class UserInputRepository : IUserInputRepository
    {
        private readonly PricingEngineDbContext _context;

        public UserInputRepository(PricingEngineDbContext context)
        {
            _context = context;
        }


        public async Task AddUserInputAsync(AddUserInputRequest request)
        {

        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

using TaskHub.Data;
using TaskHub.Interfaces;
using TaskHub.Models;

namespace TaskHub.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<User> GetUsers() 
        {
            return _context.Users.OrderBy(u => u.Id).ToList();
        }
    }
}

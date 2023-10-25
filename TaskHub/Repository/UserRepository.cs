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

        public User GetUserbyId(int UserId)
        {
            return _context.Users.Where(u => u.Id == UserId).FirstOrDefault();
        }

        public User GetUserbyEmail(string email)
        {
            return _context.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.Where(u => u.UserName == username).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.Id).ToList();
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.Id == userId);
        }

        public ICollection<Comment> GetComments(int userId)
        {
            return _context.Comments.Where(p => p.UserId == userId).ToList();
        }

        public bool CreateUser(User user)
        {
            // Change tracker - add, updating, modifying 
            // Disconnected and connected state 
            // EntityState.Added - disconnected state
            _context.Add(user);
            return Save();
        }

        public bool Save()
        {
            // Actual sql generated and send to the database
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }
    }
}

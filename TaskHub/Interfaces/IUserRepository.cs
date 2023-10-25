using TaskHub.Models;

namespace TaskHub.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUserbyId(int UserId);
        User GetUserbyEmail(string email);
        User GetUserByUsername(string username);
        bool UserExists(int userId);

        ICollection<Comment> GetComments(int userId);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool Save();


    }
}

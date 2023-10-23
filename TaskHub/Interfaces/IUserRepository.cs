using TaskHub.Models;

namespace TaskHub.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
    }
}

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
        ICollection<ProjectTasks> GetUserTaskList(int userId);
        ICollection<Project> GetProjectsManagedbyUser(int userId);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool AddUserToProject(int userId, int projectId);
        bool RemoveUserFromProject(int userId, int projectId);

        bool ProjectExists(int projectId);
        bool Save();


    }
}

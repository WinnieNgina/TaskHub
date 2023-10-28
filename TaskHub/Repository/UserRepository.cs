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

        public ICollection<ProjectTasks> GetUserTaskList(int userId)
        {
            return _context.ProjectTasks.Where(p => p.UserId == userId).ToList();
        }

        public ICollection<Project> GetProjectsManagedbyUser(int userId)
        {
            return _context.Projects.Where(p => p.ProjectManagerId == userId).ToList();
        }

        public bool AddUserToProject(int userId, int projectId)
        {
            if (!_context.Users.Any(u => u.Id == userId) || !_context.Projects.Any(p => p.Id == projectId))
                return false;

            var userProject = new UserProject
            {
                UserId = userId,
                ProjectId = projectId
            };
            _context.UserProjects.Add(userProject);
            return Save();
        }

        public bool RemoveUserFromProject(int userId, int projectId)
        {

            var userProject = _context.UserProjects.FirstOrDefault(up => up.UserId == userId && up.ProjectId == projectId);
            if (userProject == null)
                return false;

            _context.UserProjects.Remove(userProject);
            return Save();
        }

        public bool ProjectExists(int projectId)
        {
            return _context.Projects.Any(p => p.Id == projectId);
        }

    }
}

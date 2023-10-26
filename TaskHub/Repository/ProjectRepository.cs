using System.Threading.Tasks;
using TaskHub.Data;
using TaskHub.Interfaces;
using TaskHub.Models;

namespace TaskHub.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _context;
        public bool CreateProject(Project project)
        {
            _context.Add(project);
            return Save();
        }

        public bool DeleteProject(Project project)
        {
            _context.Remove(project);
            return Save();
        }

        public ICollection<Project> GetProjectbyDesciptionKey(string keyword)
        {
            return _context.Projects.Where(pt => pt.Description.Contains(keyword)).ToList();
        }

        public Project GetProjectById(int projectId)
        {
            return _context.Projects.Where(pt => pt.Id == projectId).FirstOrDefault();
        }

        public Project GetProjectByName(string projectName)
        {
            return _context.Projects.Where(pt => pt.ProjectName == projectName).FirstOrDefault();
        }

        public Project GetProjectByTitle(string title)
        {
            return _context.Projects.Where(pt => pt.ProjectTitle == title).FirstOrDefault();
        }

        public ICollection<Comment> GetProjectComments(int projectId)
        {
            return _context.Comments.Where(t => t.ProjectId == projectId).ToList();
        }

        public string GetProjectDescription(int projectId)
        {
            return _context.Projects.Where(p => p.Id == projectId).Select(p => p.Description).FirstOrDefault();
        }

        public DateTime GetProjectEndDate(int projectId)
        {
            return _context.Projects.Where(p => p.Id == projectId).Select(p => p.EndDate).FirstOrDefault();
        }

        public User GetProjectManager(int projectId)
        {
            return _context.Projects.Where(p => p.Id == projectId).Select(p => p.ProjectManager).FirstOrDefault();
        }

        public ICollection<Project> GetProjects()
        {
            return _context.Projects.OrderBy(t => t.Id).ToList();
        }

        public DateTime GetProjectStartDate(int projectId)
        {
            return _context.Projects.Where(p => p.Id == projectId).Select(p => p.StartDate).FirstOrDefault();
        }

        public ProjectStatus GetProjectstatus(int projectId)
        {
            return _context.Projects.Where(t => t.Id == projectId).Select(t => t.Status).FirstOrDefault();
        }

        public ICollection<ProjectTasks> GetProjectTasks(int projectId)
        {
            return _context.ProjectTasks.Where(t => t.ProjectId == projectId).ToList();
        }

        public string GetProjectTitle(int projectId)
        {
            return _context.Projects.Where(p => p.Id == projectId).Select(p => p.ProjectTitle).FirstOrDefault();
        }

        public string GetProjectVersion(int projectId)
        {
            return _context.Projects.Where(p => p.Id == projectId).Select(p => p.ProjectVersion).FirstOrDefault();
        }

        public bool ProjectExists(int projectId)
        {
            return _context.Projects.Any(p => p.Id == projectId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProject(Project project)
        {
            _context.Update(project);
            return Save();
        }
    }
}

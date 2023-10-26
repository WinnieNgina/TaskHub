using TaskHub.Models;

namespace TaskHub.Interfaces
{
    public interface IProjectRepository
    {
        ICollection<Project> GetProjects();
        Project GetProjectById(int projectId);
        Project GetProjectByName(string projectName);
        Project GetProjectByTitle(string title);
        DateTime GetProjectStartDate(int projectId);
        DateTime GetProjectEndDate(int projectId);
        string GetProjectTitle(int projectId);
        string GetProjectDescription(int projectId);
        string GetProjectVersion (int projectId);
        ProjectStatus GetProjectstatus(int taskId);
        bool ProjectExists (int projectId);
        User GetProjectManager (int projectId);
        ICollection<ProjectTasks> GetProjectTasks (int projectId);
        ICollection<Comment> GetProjectComments (int projectId);
        ICollection<Project> GetProjectbyDesciptionKey(string keyword);
        bool CreateProject(Project project);
        bool UpdateProject(Project project);
        bool DeleteProject(Project project);
        bool Save();



    }
}

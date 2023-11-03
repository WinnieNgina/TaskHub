using TaskHub.Models;
using PriorityLevel = TaskHub.Models.PriorityLevel;
using TaskStatus = TaskHub.Models.TaskStatus;

namespace TaskHub.Interfaces
{
    public interface IProjectTasksRepository
    {
        ICollection<ProjectTasks> GetTasks();
        ProjectTasks GetTaskById(int taskId);
        ProjectTasks GetTaskByTitle(string title);
        User GetAssignee(int taskDd);
        Project GetProject(int id);
        ICollection<Comment> GetComments(int projectTasksId);
        bool TaskExists(int id);
        TaskStatus GetTaskStatus(int taskId);
        DateTime GetDueDate(int taskId);
        ICollection<ProjectTasks> GetTaskbyDesciptionKey(string keyword);
        PriorityLevel GetTaskPriorityLevel(int taskId);
        ICollection<ProjectTasks> GetDependentTasks(int taskId);
        bool AddDependency(int taskId, int dependentTaskId);
        bool DependencyExists(int taskId, int dependentTaskId);
        bool CreateTask(ProjectTasks projectTask);
        bool UpdateTask(ProjectTasks projectTask);
        bool DeleteTask(ProjectTasks projectTask);
        bool Save();



    }
}

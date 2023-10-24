using TaskHub.Models;
using TaskStatus = TaskHub.Models.TaskStatus;
using PriorityLevel = TaskHub.Models.PriorityLevel;

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
        

    }
}

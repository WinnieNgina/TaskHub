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
        AssignTaskResult AssignTask(int taskId, int userId);
        ReassignTaskResult ReassignTask(int taskId, int newUserId);
        ICollection<User> GetUsersInAssignmentHistoryForTask(int taskId);
        Task<(bool, string)> UploadTaskReportFilesAsync(int taskId, IEnumerable<IFormFile> files, string uploadsFolderPath);
        bool Save();

    }
    public enum AssignTaskResult
    {
        Success,
        Failure,
        TaskNotFound,
        TaskAlreadyAssigned,
        UserNotFound
    }
    public enum ReassignTaskResult
    {
        Success,
        TaskNotFound,
        NewUserNotFound,
        AlreadyAssignedToNewUser,
        TaskNeverAssigned,
        Failure
    }

}

using Microsoft.EntityFrameworkCore;
using TaskHub.Data;
using TaskHub.Interfaces;
using TaskHub.Models;
using PriorityLevel = TaskHub.Models.PriorityLevel;
using TaskStatus = TaskHub.Models.TaskStatus;


namespace TaskHub.Repository
{
    public class ProjectTasksRepository : IProjectTasksRepository
    {
        private readonly DataContext _context;
        public ProjectTasksRepository(DataContext context)
        {
            _context = context;
        }

        public bool AddDependency(int taskId, int dependentTaskId)
        {
            if (taskId == dependentTaskId)
            {
                // You cannot add a task as a dependency of itself.
                return false;
            }

            if (DependencyExists(taskId, dependentTaskId))
            {
                // The dependency already exists.
                return false;
            }

            var task = GetTaskById(taskId);
            var dependentTask = GetTaskById(dependentTaskId);

            if (task == null || dependentTask == null)
            {
                // One or both tasks do not exist.
                return false;
            }

            // Create a new TaskDependency entry in the database.
            var taskDependency = new TaskDependency
            {
                TaskId = taskId,
                DependentTaskId = dependentTaskId
            };

            _context.TaskDependencies.Add(taskDependency);

            return Save();
        }

        public AssignTaskResult AssignTask(int taskId, int userId)
        {
            if (!TaskExists(taskId))
            {
                return AssignTaskResult.TaskNotFound;
            }

            var task = GetTaskById(taskId);

            if (task.UserId != null)
            {
                return AssignTaskResult.TaskAlreadyAssigned;
            }

            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return AssignTaskResult.UserNotFound;
            }

            // Update the task's status to "Open" and assign it to the user.
            task.Status = TaskStatus.Open;
            task.UserId = userId;

            if (Save())
            {
                return AssignTaskResult.Success;
            }
            else
            {
                return AssignTaskResult.Failure;
            }
        }

        public bool CreateTask(ProjectTasks projectTask)
        {
            _context.Add(projectTask);
            return Save();
        }

        public bool DeleteTask(ProjectTasks projectTask)
        {
            _context.Remove(projectTask);
            return Save();
        }

        public bool DependencyExists(int taskId, int dependentTaskId)
        {
            return _context.TaskDependencies.Any(td => td.TaskId == taskId && td.DependentTaskId == dependentTaskId);
        }

        public User GetAssignee(int taskId)
        {
            return _context.ProjectTasks.Where(pt => pt.Id == taskId).Select(pt => pt.User).FirstOrDefault();
        }

        public ICollection<Comment> GetComments(int projectTasksId)
        {
            return _context.Comments.Where(t => t.ProjectTasksId == projectTasksId).ToList();
        }

        public ICollection<ProjectTasks> GetDependentTasks(int taskId)
        {
            return _context.TaskDependencies.Where(td => td.TaskId == taskId).Select(td => td.DependentTask).ToList();
        }

        public DateTime GetDueDate(int taskId)
        {
            return _context.ProjectTasks.Where(p => p.Id == taskId).Select(p => p.DueDate).FirstOrDefault();
        }

        public Project GetProject(int tasksId)
        {
            return _context.ProjectTasks.Where(pt => pt.Id == tasksId).Select(pt => pt.Project).FirstOrDefault();
        }

        public ICollection<ProjectTasks> GetTaskbyDesciptionKey(string keyword)
        {
            return _context.ProjectTasks.Where(pt => pt.Description.Contains(keyword)).ToList();
        }

        public ProjectTasks GetTaskById(int taskId)
        {
            return _context.ProjectTasks.Where(pt => pt.Id == taskId).FirstOrDefault();
        }

        public ProjectTasks GetTaskByTitle(string title)
        {
            return _context.ProjectTasks.Where(pt => pt.Title == title).FirstOrDefault();
        }

        public PriorityLevel GetTaskPriorityLevel(int taskId)
        {
            return _context.ProjectTasks.Where(pt => pt.Id == taskId).Select(pt => pt.Priority).FirstOrDefault();
        }

        public ICollection<ProjectTasks> GetTasks()
        {
            return _context.ProjectTasks.OrderBy(t => t.Id).ToList();
        }

        public TaskStatus GetTaskStatus(int taskId)
        {
            return _context.ProjectTasks.Where(t => t.Id == taskId).Select(t => t.Status).FirstOrDefault();
        }

        public bool Save()
        {
            // Actual sql generated and send to the database
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool TaskExists(int id)
        {
            return _context.ProjectTasks.Any(t => t.Id == id);
        }

        public bool UpdateTask(ProjectTasks projectTask)
        {
            _context.Update(projectTask);
            return Save();
        }
        public ReassignTaskResult ReassignTask(int taskId, int newUserId)
        {
            var task = GetTaskById(taskId);

            if (task == null)
            {
                return ReassignTaskResult.TaskNotFound;
            }

            int? oldUserId = task.UserId;

            if (oldUserId == newUserId)
            {
                return ReassignTaskResult.AlreadyAssignedToNewUser;
            }

            var isTaskAssignedToNewUserBefore = _context.TaskAssignmentHistories
                .Any(history => history.TaskId == taskId &&
                (history.OldUserId == newUserId || history.NewUserId == newUserId));

            if (isTaskAssignedToNewUserBefore)
            {
                return ReassignTaskResult.AlreadyAssignedToNewUser;
            }

            if (!oldUserId.HasValue)
            {
                // The task was never assigned before.
                return ReassignTaskResult.TaskNeverAssigned;
            }

            if (oldUserId.Value != newUserId)
            {
                // Log the historical assignment change.
                var historyEntry = new TaskAssignmentHistory
                {
                    TaskId = taskId,
                    OldUserId = oldUserId.Value, // Make sure this is the current assignee.
                    NewUserId = newUserId, // This is the new assignee.
                    ReassignmentDate = DateTime.UtcNow
                };

                // Store this history entry in your database.
                _context.TaskAssignmentHistories.Add(historyEntry);

                // Now update the task's UserId to the new user.
                task.UserId = newUserId;

                // Update the status of the old user's task to "Cancelled."
                var oldUserTask = _context.ProjectTasks.FirstOrDefault(pt => pt.Id == taskId && pt.UserId == oldUserId.Value);
                if (oldUserTask != null)
                {
                    oldUserTask.Status = TaskStatus.Cancelled;
                }

                // This part should only happen after the historical entry is added.
                task.Status = TaskStatus.Open;

                if (Save()) // This should save both the history log and the task update in one transaction.
                {
                    return ReassignTaskResult.Success;
                }
                else
                {
                    return ReassignTaskResult.Failure;
                }
            }
            else
            {
                // This case should never happen as we check for oldUserId == newUserId above.
                // Log this as an error or handle according to your business logic.
                return ReassignTaskResult.Failure;
            }
        }
        public ICollection<User> GetUsersInAssignmentHistoryForTask(int taskId)
        {
            // Retrieve the task and its assignment history
            var task = _context.ProjectTasks
                .Include(t => t.AssignmentHistory)
                .FirstOrDefault(t => t.Id == taskId);

            if (task == null)
            {
                return null; // Task not found
            }

            // Extract unique user IDs from the assignment history
            var userIds = task.AssignmentHistory
                .SelectMany(history => new[] { history.OldUserId, history.NewUserId })
                .Distinct(); // This ensures each user ID is only considered once.

            // Retrieve user information for the unique user IDs in the assignment history
            var usersInAssignmentHistory = _context.Users
                .Where(user => userIds.Contains(user.Id))
                .ToList();

            return usersInAssignmentHistory;
        }

    }
}

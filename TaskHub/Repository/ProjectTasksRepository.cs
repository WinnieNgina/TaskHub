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
    }
}

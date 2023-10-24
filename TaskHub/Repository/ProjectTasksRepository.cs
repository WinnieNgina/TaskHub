using Microsoft.EntityFrameworkCore;
using TaskHub.Data;
using TaskHub.Interfaces;
using TaskHub.Models;
using TaskStatus = TaskHub.Models.TaskStatus;
using PriorityLevel = TaskHub.Models.PriorityLevel;


namespace TaskHub.Repository
{
    public class ProjectTasksRepository : IProjectTasksRepository
    {
        private readonly DataContext _context;
        public ProjectTasksRepository(DataContext context)
        {
            _context = context;
        }
        public User GetAssignee(int taskId)
        {
            return _context.ProjectTasks.Where(pt => pt.Id == taskId).Select(pt => pt.User).FirstOrDefault();
        }

        public ICollection<Comment> GetComments(int projectTasksId)
        {
            return _context.Comments.Where(t => t.ProjectTasksId == projectTasksId).ToList();
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

        public bool TaskExists(int id)
        {
            return _context.ProjectTasks.Any(t => t.Id == id);
        }
    }
}

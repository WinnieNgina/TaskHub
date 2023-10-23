using System.ComponentModel.DataAnnotations.Schema;

namespace TaskHub.Models
{
    public class ProjectTasks
    {

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public PriorityLevel Priority { get; set; }
        public int AssignedBy { get; set; }

        // object that represents the user who created the task
        [ForeignKey("User")]
        public int UserId { get; set; } // Foreign key

        public User User { get; set; }
        // User object that represents the user who is assigned to complete the task
        

        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public ICollection<Comment> Comments { get; set; }  
       
    }
    public enum TaskStatus
    {
        ToDo,
        Open,
        InProgress,
        Completed
    }
    public enum PriorityLevel
    {
        Low,
        Medium,
        High,
        Critical
    }

}

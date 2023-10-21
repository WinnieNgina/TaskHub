namespace TaskHub.Models
{
    public class Tasks : BaseModel
    {
       
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public int AssignedByUserId { get; set; }
        public User AssignedBy { get; set; }
        // object that represents the user who created the task
        public int AssignedToUserId { get; set; } // Foreign key
        public User AssignedTo { get; set; }
        // User object that represents the user who is assigned to complete the task
       
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public ICollection<Comment> Comments { get; set; }
        
       
    }
    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Completed
    }

}

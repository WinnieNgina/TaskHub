namespace TaskHub.Models
{
    public class Tasks : BaseModel
    {
       
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public User AssignedBy { get; set; }
        public Project Project { get; set; }
        public string ProjectId { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public int UserId { get; set; }
        // object that represents the user who created the task
        public User AssignedTo { get; set; }
        // User object that represents the user who is assigned to complete the task
       
    }
    public enum TaskStatus
    {
        ToDo,
        InProgress,
        Completed
    }

}

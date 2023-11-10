namespace TaskHub.Models
{
    public class ProjectTasks
    {

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public PriorityLevel Priority { get; set; }
        public int? UserId { get; set; } // Foreign key
        public User User { get; set; }

        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<TaskDependency> TaskDependencies { get; set; }
        public ICollection<TaskAssignmentHistory> AssignmentHistory { get; set; }
        public ICollection<TaskReportFile> ReportFiles { get; set; }

    }
    public enum TaskStatus
    {
        ToDo,
        Open,
        Due,
        Overdue,
        InProgress,
        Completed,
        Rejected,
        Rescheduled,
        UnderReview,
        OnHold,
        Deferred,
        Cancelled,
        Archived,
    }
    public enum PriorityLevel
    {
        Low,
        Medium,
        High,
        Critical
    }

}

namespace TaskHub.Models
{
    public class TaskAssignmentHistory
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int OldUserId { get; set; }
        public int NewUserId { get; set; }
        public DateTime ReassignmentDate { get; set; }

        // Define a navigation property to access the associated task.
        public ProjectTasks Task { get; set; }
    }
}

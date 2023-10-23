namespace TaskHub.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Content { get; set; }
        public string ContentTitle { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public int ProjectTasksId { get; set; }
        public ProjectTasks ProjectTasks { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }


    }
}

namespace TaskHub.Models
{
    public class Project : BaseModel
    {

        public string ProjectName { get; set; }
        public string ProjectVersion { get; set; }
        public string ProjectTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public User ProjectManager { get; set; } //Project manager
        public int ProjectManagerId { get; set; } // project manager for various proejcts
        public ICollection<Tasks> Tasks { get; set; }
        public ICollection<UserProject> Team { get; set; }
        // Represents all the users that are members of the project.
        public ProjectStatus Status { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
    public enum ProjectStatus
    {
        NotStarted,
        InProgress,
        Completed
    }

}

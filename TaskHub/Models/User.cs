namespace TaskHub.Models
{
    public class User
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string UserName { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfilePicturePath { get; set; }
        public ICollection<ProjectTasks> AssignedTasks { get; set; }
        // List of Task objects that represents all the tasks that have been assigned to the user

        public ICollection<Project> ManagedProjects { get; set; }
        // Represents all projects whereby User is a project manager
        public ICollection<UserProject> UserProjects { get; set; }
        // Represents all projects whereby user is a team member
        public ICollection<Comment> Comments { get; set; }

    }
}

namespace TaskHub.Models
{
    public class User : BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Tasks> AssignedTasks { get; set; }
        // List of Task objects that represents all the tasks that have been assigned to the user
        public ICollection<Tasks> CreatedTasks { get; set;}
        //  list of Task objects that represents all the tasks that the user has created
        public ICollection<Project> ProjectManagerList { get; set; }
        // Represents all projects whereby User is a project manager
        public ICollection<UserProject> UserProjects { get; set; }
        // Represents all projects whereby user is a team member
    }
}

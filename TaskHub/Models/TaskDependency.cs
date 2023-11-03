namespace TaskHub.Models
{
    public class TaskDependency
    {
        public int TaskId { get; set; }
        public ProjectTasks ParentTask { get; set; }

        public int DependentTaskId { get; set; }
        public ProjectTasks DependentTask { get; set; }
    }
}

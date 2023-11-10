namespace TaskHub.Models
{
    public class TaskReportFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public DateTime UploadedOn { get; set; }
        public string FilePath { get; set; } // Physical or virtual path to the file

        // Foreign key to the ProjectTasks
        public int ProjectTaskId { get; set; }
        public ProjectTasks ProjectTask { get; set; }
    }
}

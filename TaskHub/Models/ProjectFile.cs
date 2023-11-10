namespace TaskHub.Models
{
    public class ProjectFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public long Size { get; set; }
        public DateTime UploadedOn { get; set; }
        public string FilePath { get; set; } // The path where the file is stored

        // Foreign key to the Project
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}

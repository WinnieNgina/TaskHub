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
        
        public int TaskId { get; set; }
       
        public int ProjectId { get; set; }
        

    }
}

namespace TaskHub.Models
{
    public class Comment : BaseModel
    {

        public string Content { get; set; }
        public string ContentTitle { get; set; }
        public User User { get; set; }
        public int TaskId { get; set; }
        public int ProjectId { get; set; }

    }
}

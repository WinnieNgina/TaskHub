using TaskHub.Models;

namespace TaskHub.Interfaces
{
    public interface ICommentRepository
    {
        Comment GetComment(int commentId);
        ICollection<Comment> GetComments();
        Comment GetCommentByTitle(string commentTitle);
        ICollection<Comment> GetCommentByContentKey(string keyword);
        User GetUser(int commentId);
        Project GetProject(int commentId);
        ProjectTasks GetProjectTasks(int commentId);
        bool CommentExists(int commentId);
        string GetCommentContent(int commentId);
        bool CreateComment(Comment comment);
        bool UpdateComment(Comment comment);
        bool DeleteComment(Comment comment);
        bool Save();

    }
}

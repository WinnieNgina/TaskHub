using TaskHub.Data;
using TaskHub.Interfaces;
using TaskHub.Models;

namespace TaskHub.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;
        public CommentRepository(DataContext dataContext)
        {
            _context = dataContext;
        }

        public bool CommentExists(int commentId)
        {
            return _context.Comments.Any(c => c.Id == commentId);
        }

        public bool CreateComment(Comment comment)
        {
            _context.Add(comment);
            return Save();
        }

        public bool DeleteComment(Comment comment)
        {
            _context.Remove(comment);
            return Save();
        }

        public Comment GetComment(int commentId)
        {
            return _context.Comments.Where(c => c.Id == commentId).FirstOrDefault();
        }

        public ICollection<Comment> GetCommentByContentKey(string keyword)
        {
            return _context.Comments.Where(c => c.Content.Contains(keyword)).ToList();
        }

        public Comment GetCommentByTitle(string commentTitle)
        {
            return _context.Comments.Where(c => c.ContentTitle == commentTitle).FirstOrDefault();
        }

        public string GetCommentContent(int commentId)
        {
            return _context.Comments.Where(c => c.Id == commentId).Select(c => c.Content).FirstOrDefault();
        }

        public ICollection<Comment> GetComments()
        {
            return _context.Comments.OrderBy(c => c.Id).ToList();
        }

        public Project GetProject(int commentId)
        {
            return _context.Comments.Where(c => c.Id == commentId).Select(c => c.Project).FirstOrDefault();
        }

        public ProjectTasks GetProjectTasks(int commentId)
        {
            return _context.Comments.Where(c => c.Id == commentId).Select(c => c.ProjectTasks).FirstOrDefault();
        }

        public User GetUser(int commentId)
        {
            return _context.Comments.Where(c => c.Id == commentId).Select(c => c.User).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateComment(Comment comment)
        {
            _context.Update(comment);
            return Save();
        }
    }
}

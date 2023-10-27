using Microsoft.AspNetCore.Mvc;
using TaskHub.Interfaces;
using TaskHub.Models;

namespace TaskHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Comment>))]
        public IActionResult GetComments()
        {
            var comments = _commentRepository.GetComments();
            return Ok(comments);
        }
        [HttpGet("Id/{commentId}")]
        [ProducesResponseType(200, Type = typeof(Comment))]
        [ProducesResponseType(400)]
        public IActionResult GetComment(int commentId)
        {
            var comment = _commentRepository.GetComment(commentId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(comment);
        }
        [HttpGet("Title/{commentTitle}")]
        [ProducesResponseType(200, Type = typeof(Comment))]
        [ProducesResponseType(400)]
        public IActionResult GetCommentByTitle(string title)
        {
            var comment = _commentRepository.GetCommentByTitle(title);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(comment);
        }
        [HttpGet("{keyWord}/KeywordSearch")]
        [ProducesResponseType(200, Type = typeof(ICollection<Comment>))]
        public IActionResult GetCommentbyDescriptionKey(string keyWord)
        {
            var comment = _commentRepository.GetCommentByContentKey(keyWord);
            return Ok(comment);
        }
        [HttpGet("{commentId}/User")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUserWhoCommented(int commentId)
        {
            if (!_commentRepository.CommentExists(commentId))
                return NotFound();
            var user = _commentRepository.GetUser(commentId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }
        [HttpGet("{commentId}/Project")]
        [ProducesResponseType(200, Type = typeof(Project))]
        [ProducesResponseType(400)]
        public IActionResult GetProjectCommented(int commentId)
        {
            if (!_commentRepository.CommentExists(commentId))
                return NotFound();
            var project = _commentRepository.GetProject(commentId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(project);
        }
        [HttpGet("{commentId}/ProjectTask")]
        [ProducesResponseType(200, Type = typeof(ProjectTasks))]
        [ProducesResponseType(400)]
        public IActionResult GetProjectTaskCommented(int commentId)
        {
            if (!_commentRepository.CommentExists(commentId))
                return NotFound();
            var projectTask = _commentRepository.GetProjectTasks(commentId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(projectTask);
        }
        [HttpGet("{commentId}/Content")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public IActionResult GetContent(int commentId)
        {
            if (!_commentRepository.CommentExists(commentId))
                return NotFound();
            var content = _commentRepository.GetCommentContent(commentId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(content);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult CreateComment([FromBody] Comment commentCreate)
        {
            if (commentCreate == null)
                return BadRequest(ModelState);
            var comment = _commentRepository.GetComments()
                .Where(c => c.Content.Trim().ToUpper() == commentCreate.Content.ToUpper())
                .FirstOrDefault();
            if (comment != null)
            {
                ModelState.AddModelError("", "Comment already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_commentRepository.CreateComment(commentCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Comment successfully created");
        }
        [HttpPut("{commentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateComment(int commentId, [FromBody] Comment commentUpdate)
        {
            if (commentUpdate == null)
                // check if instance is null
                return BadRequest(ModelState);
            if (commentId != commentUpdate.Id)
                // check if the id passed for update matches ID provided in the instance for updates
                return BadRequest(ModelState);
            if (!_commentRepository.CommentExists(commentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();
            if (!_commentRepository.UpdateComment(commentUpdate))
            {
                ModelState.AddModelError("", "Something went wrong while updating comment");
                return StatusCode(500, ModelState);
            }
            return Ok("Comment successfully updated");
            //when executed will produce a 204 No Content response therefore return     No Content()
        }
        [HttpDelete("{commentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteComment(int commentId)
        {
            if (!_commentRepository.CommentExists(commentId))
            {
                return NotFound();
            }
            var commentToDelete = _commentRepository.GetComment(commentId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_commentRepository.DeleteComment(commentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting comment");
            }
            return Ok("Comment succesfully deleted");
        }
    }
}

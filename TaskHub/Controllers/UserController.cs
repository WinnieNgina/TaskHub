using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskHub.Interfaces;
using TaskHub.Models;

namespace TaskHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //Attribute indicates controller responds to web API requests
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<User>))]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(users);
        }
        [HttpGet("id/{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUserById(int userId)
        {
            var user = _userRepository.GetUserbyId(userId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }
        [HttpGet("email/{email}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUserByEmail(string email) 
        {
            var user = _userRepository.GetUserbyEmail(email);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }
        [HttpGet("username/{username}")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetUserbyUserName(string userName)
        {
            var user = _userRepository.GetUserByUsername(userName);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }
        [HttpGet("{userId}/comments")]
        [ProducesResponseType(200, Type=typeof(ICollection<Comment>))]
        [ProducesResponseType(400)]
        public IActionResult GetComments(int userId) 
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();
            var comments = _userRepository.GetComments(userId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(comments);
        }
    }
}

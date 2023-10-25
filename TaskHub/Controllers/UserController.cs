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
            return Ok(users.Select(x => new
            {
                x.Id,
                x.UserName,
                x.Email,
                x.FirstName,
                x.LastName,
                x.Phone
            }));
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
        public IActionResult GetUserbyUserName(string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            return Ok(user);
        }
        [HttpGet("{userId}/comments")]
        [ProducesResponseType(200, Type = typeof(ICollection<Comment>))]
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
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult CreateUser([FromBody] User userCreate)
        {
            if (userCreate == null)
                return BadRequest(ModelState);
            var user = _userRepository.GetUsers()
                .Where(c => c.UserName.Trim().ToUpper() == userCreate.UserName.ToUpper())
                .FirstOrDefault();
            if (user != null)
            {
                ModelState.AddModelError("", "User already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_userRepository.CreateUser(userCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("User successfully created");
        }
        [HttpPut("{userId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(204)]
        public IActionResult UpdateUser(int userId, [FromBody] User userUpdate)
        {
            // Refer to controller base class for deeper understanding of the methods and return types
            if (userUpdate == null)
                // check if instance is null
                return BadRequest(ModelState);
            if (userId != userUpdate.Id)
                // check if the id passed for update matches ID provided in the instance for updates
                return BadRequest(ModelState);
            if (!_userRepository.UserExists(userId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();
            if (!_userRepository.UpdateUser(userUpdate))
            {
                ModelState.AddModelError("", "Something went wrong while updating category");
                return StatusCode(500, ModelState);
            }
            return NoContent();
            //when executed will produce a 204 No Content response.
        }

    }
}

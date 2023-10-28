using Microsoft.AspNetCore.Mvc;
using TaskHub.Interfaces;
using TaskHub.Models;

namespace TaskHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Project>))]
        public IActionResult Getprojects()
        {
            var projects = _projectRepository.GetProjects();
            return Ok(projects);
        }
        [HttpGet("Id/{projectId}")]
        [ProducesResponseType(200, Type = typeof(Project))]
        [ProducesResponseType(400)]
        public IActionResult GetProjectById(int projectId)
        {
            var project = _projectRepository.GetProjectById(projectId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(project);
        }
        [HttpGet("Name/{projectName}")]
        [ProducesResponseType(200, Type = typeof(Project))]
        [ProducesResponseType(400)]
        public IActionResult GetProjectByName(string projectName)
        {
            var project = _projectRepository.GetProjectByName(projectName);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(project);
        }
        [HttpGet("Title/{projectTitle}")]
        [ProducesResponseType(200, Type = typeof(Project))]
        [ProducesResponseType(400)]
        public IActionResult GetProjectByTitle(string projectTitle)
        {
            var project = _projectRepository.GetProjectByTitle(projectTitle);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(project);
        }
        [HttpGet("{projectId}/comments")]
        [ProducesResponseType(200, Type = typeof(ICollection<Comment>))]
        [ProducesResponseType(400)]
        public IActionResult GetComments(int projectId)
        {
            if (!_projectRepository.ProjectExists(projectId))
                return NotFound();
            var comments = _projectRepository.GetProjectComments(projectId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(comments);
        }
        [HttpGet("{projectId}/StartDate")]
        [ProducesResponseType(200, Type = typeof(DateTime))]
        [ProducesResponseType(400)]
        public IActionResult GetProjectStartDate(int projectId)
        {
            if (!_projectRepository.ProjectExists(projectId))
                return NotFound();
            var startDate = _projectRepository.GetProjectStartDate(projectId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(startDate);
        }
        [HttpGet("{projectId}/EndDate")]
        [ProducesResponseType(200, Type = typeof(DateTime))]
        [ProducesResponseType(400)]
        public IActionResult GetProjectEndDate(int projectId)
        {
            if (!_projectRepository.ProjectExists(projectId))
                return NotFound();
            var endDate = _projectRepository.GetProjectEndDate(projectId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(endDate);
        }
        [HttpGet("{projectId}/ProjectTitle")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public IActionResult GetProjectTitle(int projectId)
        {
            if (!_projectRepository.ProjectExists(projectId))
                return NotFound();
            var title = _projectRepository.GetProjectTitle(projectId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(title);
        }
        [HttpGet("{projectId}/Description")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public IActionResult GetProjectDescription(int projectId)
        {
            if (!_projectRepository.ProjectExists(projectId))
                return NotFound();
            var description = _projectRepository.GetProjectDescription(projectId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(description);
        }
        [HttpGet("{projectId}/ProjectVersion")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public IActionResult GetProjectVersion(int projectId)
        {
            if (!_projectRepository.ProjectExists(projectId))
                return NotFound();
            var version = _projectRepository.GetProjectVersion(projectId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(version);
        }
        [HttpGet("{projectId}/Status")]
        [ProducesResponseType(200, Type = typeof(ProjectStatus))]
        [ProducesResponseType(400)]
        public IActionResult GetProjectStatus(int projectTaskId)
        {
            if (!_projectRepository.ProjectExists(projectTaskId))
                return NotFound();
            var status = _projectRepository.GetProjectstatus(projectTaskId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(status);
        }
        [HttpGet("{projectId}/ProjectManager")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetProjectManager(int projectId)
        {
            if (!_projectRepository.ProjectExists(projectId))
                return NotFound();
            var user = _projectRepository.GetProjectManager(projectId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }
        [HttpGet("{projectId}/Tasks")]
        [ProducesResponseType(200, Type = typeof(ICollection<ProjectTasks>))]
        [ProducesResponseType(400)]
        public IActionResult GetProjectTasks(int projectId)
        {
            if (!_projectRepository.ProjectExists(projectId))
                return NotFound();
            var projectTasks = _projectRepository.GetProjectTasks(projectId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(projectTasks);
        }
        [HttpGet("{keyWord}/KeywordSearch")]
        [ProducesResponseType(200, Type = typeof(ICollection<Project>))]
        public IActionResult GetProjectbyDescriptionKey(string keyWord)
        {
            var project = _projectRepository.GetProjectbyDesciptionKey(keyWord);
            return Ok(project);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult CreateProject([FromBody] Project projectCreate)
        {
            if (projectCreate == null)
                return BadRequest(ModelState);
            var user = _projectRepository.GetProjects()
                .Where(c => c.ProjectName.Trim().ToUpper() == projectCreate.ProjectName.ToUpper())
                .FirstOrDefault();
            if (user != null)
            {
                ModelState.AddModelError("", "Project already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_projectRepository.CreateProject(projectCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Project successfully created");
        }
        [HttpPut("{projectId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateProject(int projectId, [FromBody] Project projectUpdate)
        {
            if (projectUpdate == null)
                // check if instance is null
                return BadRequest(ModelState);
            if (projectId != projectUpdate.Id)
                // check if the id passed for update matches ID provided in the instance for updates
                return BadRequest(ModelState);
            if (!_projectRepository.ProjectExists(projectId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();
            if (!_projectRepository.UpdateProject(projectUpdate))
            {
                ModelState.AddModelError("", "Something went wrong while updating project");
                return StatusCode(500, ModelState);
            }
            return Ok("Project successfully updated");
            //when executed will produce a 204 No Content response therefore return     No Content()
        }
        [HttpDelete("{projectId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProject(int projectId)
        {
            if (!_projectRepository.ProjectExists(projectId))
            {
                return NotFound();
            }
            var projectToDelete = _projectRepository.GetProjectById(projectId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_projectRepository.DeleteProject(projectToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting project");
            }
            return Ok("Project succesfully deleted");
        }
        [HttpGet("{projectId}/Team")]
        [ProducesResponseType(200, Type = typeof(ICollection<User>))]
        [ProducesResponseType(400)] // Bad Request for model state validation errors
        [ProducesResponseType(404)]
        public IActionResult GetUsersByProject(int projectId)
        {
            if (projectId <= 0)
            {
                ModelState.AddModelError("projectId", "Project ID must be a positive integer.");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the project exists
            if (!_projectRepository.ProjectExists(projectId))
            {
                return NotFound();
            }

            var users = _projectRepository.GetProjectTeam(projectId);

            if (users == null || users.Count == 0)
            {
                return NotFound();
            }

            return Ok(users);
        }

    }
}

﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TaskHub.Dto;
using TaskHub.Interfaces;
using TaskHub.Models;
using TaskStatus = TaskHub.Models.TaskStatus;

namespace TaskHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTasksController : ControllerBase
    {
        private readonly IProjectTasksRepository _projectTasksRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public ProjectTasksController(IProjectTasksRepository projectTasksRepository, IUserRepository userRepository, IConfiguration configuration)
        {
            _projectTasksRepository = projectTasksRepository;
            _userRepository = userRepository;
            _configuration = configuration;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<ProjectTasks>))]
        public IActionResult GetTasks()
        {
            var projectTasks = _projectTasksRepository.GetTasks();
            return Ok(projectTasks);
        }
        [HttpGet("{taskId}/AssignmentHistory")]
        [ProducesResponseType(200, Type = typeof(ICollection<User>))]
        [ProducesResponseType(404)]
        public IActionResult GetAssignmentHistory(int taskId)
        {
            var task = _projectTasksRepository.GetTaskById(taskId);

            if (task == null)
            {
                return NotFound("Task not found");
            }

            var assignmentHistory = _projectTasksRepository.GetUsersInAssignmentHistoryForTask(taskId);

            if (assignmentHistory == null || !assignmentHistory.Any())
            // .Any() method is a LINQ extension method that checks if there are any elements in the collection.
            {
                // Return a different message if there's no assignment history
                return NotFound("No assignment history found for the task");
            }

            // Optionally transform the user entities into a DTO representation
            var assignmentHistoryDto = assignmentHistory.Select(user => new UserDto
            {
                Id = user.Id,
                UserName = user.UserName

            });

            return Ok(assignmentHistoryDto);
        }


        [HttpGet("DependentTasks/{taskId}")]
        [ProducesResponseType(200, Type = typeof(ICollection<ProjectTasks>))]
        public IActionResult GetDependentTasks(int taskId)
        {
            // Retrieve dependent tasks based on the provided taskId
            var dependentTasks = _projectTasksRepository.GetDependentTasks(taskId);
            return Ok(dependentTasks);
        }
        [HttpGet("id/{projectTaskId}")]
        [ProducesResponseType(200, Type = typeof(ProjectTasks))]
        [ProducesResponseType(400)]
        public IActionResult GetTaskById(int projectTaskId)
        {
            var projectTask = _projectTasksRepository.GetTaskById(projectTaskId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(projectTask);
        }
        [HttpGet("Title/{title}")]
        [ProducesResponseType(200, Type = typeof(ProjectTasks))]
        [ProducesResponseType(400)]
        public IActionResult GetTaskByTitle(string title)
        {
            var projectTask = _projectTasksRepository.GetTaskByTitle(title);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(projectTask);
        }
        [HttpGet("{projectTaskId}/comments")]
        [ProducesResponseType(200, Type = typeof(ICollection<Comment>))]
        [ProducesResponseType(400)]
        public IActionResult GetComments(int projectTaskId)
        {
            if (!_projectTasksRepository.TaskExists(projectTaskId))
                return NotFound();
            var comments = _projectTasksRepository.GetComments(projectTaskId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(comments);
        }
        [HttpGet("{projectTaskId}/Assignee")]
        [ProducesResponseType(200, Type = typeof(User))]
        [ProducesResponseType(400)]
        public IActionResult GetAssignee(int projectTaskId)
        {
            if (!_projectTasksRepository.TaskExists(projectTaskId))
                return NotFound();
            var user = _projectTasksRepository.GetAssignee(projectTaskId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(user);
        }
        [HttpGet("{projectTaskId}/DueDate")]
        [ProducesResponseType(200, Type = typeof(DateTime))]
        [ProducesResponseType(400)]
        public IActionResult GetDueDate(int projectTaskId)
        {
            if (!_projectTasksRepository.TaskExists(projectTaskId))
                return NotFound();
            var dueDate = _projectTasksRepository.GetDueDate(projectTaskId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(dueDate);
        }
        [HttpGet("{projectTaskId}/Project")]
        [ProducesResponseType(200, Type = typeof(Project))]
        [ProducesResponseType(400)]
        public IActionResult GetProject(int projectTaskId)
        {
            if (!_projectTasksRepository.TaskExists(projectTaskId))
                return NotFound();
            var project = _projectTasksRepository.GetProject(projectTaskId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(project);
        }
        [HttpGet("{keyWord}/KeywordSearch")]
        [ProducesResponseType(200, Type = typeof(ICollection<ProjectTasks>))]
        public IActionResult GetTaskbyDescriptionKey(string keyWord)
        {
            var projectTasks = _projectTasksRepository.GetTaskbyDesciptionKey(keyWord);
            return Ok(projectTasks);
        }
        [HttpGet("{projectTaskId}/Priority")]
        [ProducesResponseType(200, Type = typeof(PriorityLevel))]
        [ProducesResponseType(400)]
        public IActionResult GetTaskPriorityLevel(int projectTaskId)
        {
            if (!_projectTasksRepository.TaskExists(projectTaskId))
                return NotFound();
            var priority = _projectTasksRepository.GetTaskPriorityLevel(projectTaskId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(priority);
        }
        [HttpGet("{projectTaskId}/Status")]
        [ProducesResponseType(200, Type = typeof(TaskStatus))]
        [ProducesResponseType(400)]
        public IActionResult GetTaskStatus(int projectTaskId)
        {
            if (!_projectTasksRepository.TaskExists(projectTaskId))
                return NotFound();
            var status = _projectTasksRepository.GetTaskStatus(projectTaskId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(status);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult CreateTask([FromBody] ProjectTasks taskCreate)
        {
            if (taskCreate == null)
                return BadRequest(ModelState);
            var projectTask = _projectTasksRepository.GetTasks()
                .Where(c => c.Title.Trim().ToUpper() == taskCreate.Title.ToUpper())
                .FirstOrDefault();
            if (projectTask != null)
            {
                ModelState.AddModelError("", "Task already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_projectTasksRepository.CreateTask(taskCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Task successfully created");
        }
        [HttpPut("{taskId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateTask(int taskId, [FromBody] ProjectTasks taskUpdate)
        {
            if (taskUpdate == null)
                // check if instance is null
                return BadRequest(ModelState);
            if (taskId != taskUpdate.Id)
                // check if the id passed for update matches ID provided in the instance for updates
                return BadRequest(ModelState);
            if (!_projectTasksRepository.TaskExists(taskId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();
            if (!_projectTasksRepository.UpdateTask(taskUpdate))
            {
                ModelState.AddModelError("", "Something went wrong while updating task");
                return StatusCode(500, ModelState);
            }
            return Ok("Task successfully updated");
            //when executed will produce a 204 No Content response therefore return     No Content()
        }
        [HttpDelete("{taskId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTask(int taskId)
        {
            if (!_projectTasksRepository.TaskExists(taskId))
            {
                return NotFound();
            }
            var taskToDelete = _projectTasksRepository.GetTaskById(taskId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_projectTasksRepository.DeleteTask(taskToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting task");
            }
            return Ok("Task succesfully deleted");
        }
        [HttpPost("{taskId}/Assign/{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)] // Conflict status if the task is already assigned
        public IActionResult AssignTask(int taskId, int userId)
        {
            var result = _projectTasksRepository.AssignTask(taskId, userId);

            if (result == AssignTaskResult.Success)
            {
                return Ok("Task assigned successfully");
            }
            else if (result == AssignTaskResult.TaskNotFound)
            {
                return NotFound("Task not found");
            }
            else if (result == AssignTaskResult.TaskAlreadyAssigned)
            {
                return Conflict("Task is already assigned");
            }
            else if (result == AssignTaskResult.UserNotFound)
            {
                return NotFound("User not found");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong assigning the task");
                return BadRequest(ModelState);
            }
        }
        [HttpPut("{taskId}/Reassign/{newUserId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)] // HTTP 422 Unprocessable Entity
        public IActionResult ReassignTask(int taskId, int newUserId)
        {
            var reassignmentResult = _projectTasksRepository.ReassignTask(taskId, newUserId);

            if (reassignmentResult == ReassignTaskResult.Success)
            {
                return Ok("Task reassigned successfully");
            }

            if (reassignmentResult == ReassignTaskResult.TaskNotFound)
            {
                return NotFound("Task not found");
            }

            if (reassignmentResult == ReassignTaskResult.NewUserNotFound)
            {
                return NotFound("New user not found");
            }

            if (reassignmentResult == ReassignTaskResult.AlreadyAssignedToNewUser)
            {
                return Conflict("Task is already assigned to this user and they couldn't complete it sucessfully");
            }

            if (reassignmentResult == ReassignTaskResult.TaskNeverAssigned)
            {
                // Respond with 422 Unprocessable Entity to indicate the task cannot be processed as it has never been assigned
                ModelState.AddModelError("", "Task was never assigned and cannot be reassigned");
                return UnprocessableEntity(ModelState);
            }

            if (reassignmentResult == ReassignTaskResult.Failure)
            {
                ModelState.AddModelError("", "Something went wrong reassigning the task");
                return BadRequest(ModelState);
            }

            // If none of the above, return a generic 409 Conflict
            ModelState.AddModelError("", "An unknown error occurred");
            return Conflict(ModelState);
        }

        [HttpDelete("{taskId}/Unassign")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public IActionResult UnassignTask(int taskId)
        {
            var task = _projectTasksRepository.GetTaskById(taskId);
            if (task == null)
            {
                return NotFound("Task not found");
            }

            if (task.UserId == null)
            {
                return Conflict("Task is not assigned to any user");
            }

            task.UserId = null; // Unassign the task by setting the UserId to null
            if (!_projectTasksRepository.UpdateTask(task))
            {
                ModelState.AddModelError("", "Something went wrong while unassigning the task");
                return BadRequest(ModelState);
            }

            return Ok("Task successfully unassigned"); // Task unassigned successfully
        }
        [HttpPost("{taskId}/Add-dependent-tasks{dependentTaskId}")]
        public IActionResult AddDependency(int taskId, int dependentTaskId)
        {
            // Check if the dependency already exists
            if (_projectTasksRepository.DependencyExists(taskId, dependentTaskId))
            {
                return BadRequest("Dependency already exists.");
            }

            // Add the dependency
            _projectTasksRepository.AddDependency(taskId, dependentTaskId);

            return Ok("Dependency added successfully");
        }
        [HttpPost("upload/{taskId}")]
        [ProducesResponseType(200)] // Indicates success
        [ProducesResponseType(400)] // Indicates a bad request
        [ProducesResponseType(404)] // Indicates that the task was not found
        [ProducesResponseType(500)] // Indicates a server error
        public async Task<IActionResult> UploadFiles(int taskId, [FromForm] IFormFile file, [FromForm] IEnumerable<IFormFile> files)
        {
            if (!_projectTasksRepository.TaskExists(taskId))
            {
                return NotFound("Task not found.");
            }

            var uploadsFolderPath = _configuration.GetValue<string>("FileStoragePath");
            if (string.IsNullOrEmpty(uploadsFolderPath))
            {
                return StatusCode(500, "Upload path is not configured correctly.");
            }

            var allFiles = new List<IFormFile>();
            if (file != null)
            {
                allFiles.Add(file);
            }
            if (files != null)
            {
                allFiles.AddRange(files);
            }
            if (!allFiles.Any())
            {
                return BadRequest("No files provided for upload.");
            }

            var (isSuccess, errorMessage) = await _projectTasksRepository.UploadTaskReportFilesAsync(taskId, allFiles, uploadsFolderPath);
            if (!isSuccess)
            {
                return BadRequest(errorMessage);
            }

            return Ok("Files uploaded successfully.");
        }
    }
}

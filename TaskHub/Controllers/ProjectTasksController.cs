﻿using Microsoft.AspNetCore.Mvc;
using TaskHub.Interfaces;
using TaskHub.Models;
using TaskHub.Repository;
using TaskStatus = TaskHub.Models.TaskStatus;

namespace TaskHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTasksController : ControllerBase
    {
        private readonly IProjectTasksRepository _projectTasksRepository;
        public ProjectTasksController(IProjectTasksRepository projectTasksRepository)
        {
            _projectTasksRepository = projectTasksRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<ProjectTasks>))]
        public IActionResult GetTasks()
        {
            var projectTasks = _projectTasksRepository.GetTasks();
            return Ok(projectTasks);
        }
        [HttpGet("id/{projectTaskId}")]
        [ProducesResponseType(200, Type = typeof(ProjectTasks))]
        [ProducesResponseType(400)]
        public IActionResult GetUserById(int projectTaskId)
        {
            var projectTask = _projectTasksRepository.GetTaskById(projectTaskId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(projectTask);
        }
        [HttpGet("Title/{title}")]
        [ProducesResponseType(200, Type = typeof(ProjectTasks))]
        [ProducesResponseType(400)]
        public IActionResult GetUserByTitle(string title)
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
    }
}
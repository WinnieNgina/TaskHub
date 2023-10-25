using Microsoft.AspNetCore.Mvc;
using TaskHub.Interfaces;

namespace TaskHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTasksController : ControllerBase
    {
        private readonly IProjectTasksRepository _projectTasksRepository;
        public ProductTasksController(IProjectTasksRepository projectTasksRepository)
        {
            _projectTasksRepository = projectTasksRepository;
        }
    }
}

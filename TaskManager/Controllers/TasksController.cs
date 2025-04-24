using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Requests.Tasks;
using TaskManager.Application.Responses.Tasks;
using TaskManager.Application.UseCases.Tasks;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly CreateTaskUseCase _createTaskUseCase;

        public TasksController(CreateTaskUseCase createTaskUseCase)
        {
            _createTaskUseCase = createTaskUseCase;
        }

        [HttpPost]
        public ActionResult<TaskResponse> Create(CreateTaskRequest request)
        {
            var response = _createTaskUseCase.Execute(request);
            return CreatedAtAction(
                nameof(GetTaskByIdUseCase),
                new { id = response.Id },
                response
            );
        }
    }
}

using TaskManager.Application.Requests.Tasks;
using TaskManager.Application.Responses.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.UseCases.Tasks
{
    public class CreateTaskUseCase
    {
        private readonly ITaskRepository _repository;

        public CreateTaskUseCase(ITaskRepository repository)
        {
            _repository = repository;
        }

        public TaskResponse Execute(CreateTaskRequest request)
        {
            var task = new TaskItem(request.Title, request.Description, request.DueDate);
            _repository.Add(task);

            return new TaskResponse(
                task.Id,
                task.Title,
                task.Description,
                task.DueDate,
                task.IsCompleted
             );
        }
    }
}

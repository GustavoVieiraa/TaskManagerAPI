using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.UseCases.Tasks
{
    public class GetTaskByIdUseCase
    {
        private readonly ITaskRepository _repository;

        public GetTaskByIdUseCase(ITaskRepository repository)
        {
            _repository = repository;
        }

        public TaskItem? Execute(Guid id)
        {
            return _repository.GetById(id);
        }
    }
}

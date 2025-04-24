using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.UseCases.Tasks
{
    public class GetAllTasksUseCase
    {
        private readonly ITaskRepository _repository;

        public GetAllTasksUseCase(ITaskRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<TaskItem> Execute()
        {
            return _repository.GetAll();
        }
    }
}

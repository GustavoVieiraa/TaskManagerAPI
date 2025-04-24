using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.UseCases.Tasks
{
    public class UpdateTaskUseCase
    {
        private readonly ITaskRepository _repository;

        public UpdateTaskUseCase(ITaskRepository repository)
        {
            _repository = repository;
        }

        public bool Execute(Guid id, string title, string description, DateTime dueDate)
        {
            var task = _repository.GetById(id);
            if (task is null)
            {
                return false;
            }

            task.UpdateDetails(title, description, dueDate);
            _repository.Update(task);
            return true;
        }
    }
}

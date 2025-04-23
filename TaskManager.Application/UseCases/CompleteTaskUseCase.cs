using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.UseCases
{
    public class CompleteTaskUseCase
    {
        private readonly ITaskRepository _repository;

        public CompleteTaskUseCase(ITaskRepository repository)
        {
            _repository = repository;
        }

        public bool Execute(Guid id)
        {
            var task = _repository.GetById(id);
            if (task is null)
            {
                return false;
            }

            task.MarkAsCompleted();
            _repository.Update(task);
            return true;
        }
    }
}

using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.UseCases.Tasks
{
    public class DeleteTaskUseCase
    {
        private readonly ITaskRepository _repository;

        public DeleteTaskUseCase(ITaskRepository repository)
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

            _repository.Delete(task.Id);
            return true;
        }
    }
}

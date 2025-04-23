using System;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Interfaces
{
    public interface ITaskRepository
    {
        void Add(TaskItem task);
        TaskItem? GetById(Guid id);
        IEnumerable<TaskItem> GetAll();
        void Update (TaskItem task);
        void Delete(Guid id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Infrastructure.Persistence
{
    public class InMemoryTaskRepository : ITaskRepository
    {
        private readonly List<TaskItem> _tasks = new();
        
        public void Add(TaskItem task)
        {
            _tasks.Add(task);
        }

        public TaskItem? GetById(Guid id)
        {
            return _tasks.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<TaskItem> GetAll()
        {
            return _tasks;
        }

        public void Update(TaskItem task)
        {
            var index = _tasks.FindIndex(t => t.Id == task.Id);
            if (index != -1)
            {
                _tasks[index] = task;
            }
        }

        public void Delete(Guid id)
        {
            var task = GetById(id);
            if (task is not null)
            {
                _tasks.Remove(task);
            }

        }

    }
}

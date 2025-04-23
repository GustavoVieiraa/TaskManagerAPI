using System;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.UseCases
{
    public class CreateTaskUseCase
    {
        private readonly ITaskRepository _repository;

        public CreateTaskUseCase(ITaskRepository repository)
        {
            _repository = repository;
        }

        public TaskItem Execute(string title, string description, DateTime dueDate)
        {
            var task = new TaskItem(title, description, dueDate);
            _repository.Add(task);
            return task;
        }
    }
}

using System;

namespace TaskManager.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime DueDate { get; private set; }
        public bool IsCompleted { get; private set; } = false;

        public TaskItem(string title, string description, DateTime dueDate)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty.");
            if (dueDate < DateTime.Today)
                throw new ArgumentException("Due date must be today or in the future.");

            Title = title;
            Description = description;
            DueDate = dueDate;
        }

        public void MarkAsCompleted()
        {
            IsCompleted = true;
        }

        public void UpdateDetails(string title, string description, DateTime dueDate)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty.");
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty.");
            if (dueDate < DateTime.Today)
                throw new ArgumentException("Due date must be today or in the future.");

            Title = title;
            Description = description;
            DueDate = dueDate;
        }
    }
}

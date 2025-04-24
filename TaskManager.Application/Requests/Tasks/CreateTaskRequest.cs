namespace TaskManager.Application.Requests.Tasks
{
    public record CreateTaskRequest(string Title, string Description, DateTime DueDate);
}

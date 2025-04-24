namespace TaskManager.Application.Responses.Tasks
{
    public record TaskResponse(Guid Id, string Title, string Description, DateTime DueDate, bool IsCompleted);
}

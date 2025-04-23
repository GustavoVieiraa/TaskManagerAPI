using FluentAssertions;
using Moq;
using TaskManager.Application.UseCases;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.UnitTests.Application.UseCases
{
    public class GetAllTasksUseCaseTests
    {
        [Fact]
        public void Execute_ShouldReturnAllTasks_WhenTasksExist()
        {
            var tasks = new List<TaskItem>
            {
                new TaskItem("Task 1", "Desc 1", DateTime.Today.AddDays(1)),
                new TaskItem("Task 2", "Desc 2", DateTime.Today.AddDays(2)),
            };

            var repo = new Mock<ITaskRepository>();
            repo.Setup(r => r.GetAll()).Returns(tasks);

            var useCase = new GetAllTasksUseCase(repo.Object);

            var result = useCase.Execute();

            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(tasks);

        }

        [Fact]
        public void Execute_ShouldReturnEmptyList_WhenNoTasksExist()
        {
            var repo = new Mock<ITaskRepository>();
            repo.Setup(r => r.GetAll()).Returns(new List<TaskItem>());

            var useCase = new GetAllTasksUseCase(repo.Object);

            var result = useCase.Execute();

            result.Should().BeEmpty();
        }
    }
}

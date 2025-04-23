using FluentAssertions;
using Moq;
using TaskManager.Application.UseCases;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.UnitTests.Application.UseCases
{
    public class GetTaskByIdUseCaseTests
    {
        [Fact]
        public void Execute_ShouldReturnTask_WhenTaskExists()
        {
            var repo = new Mock<ITaskRepository>();
            var task = new TaskItem("um titulo válido", "Clean Architecture", DateTime.Today.AddDays(3));

            repo.Setup(r => r.GetById(task.Id)).Returns(task);

            var useCase = new GetTaskByIdUseCase(repo.Object);

            var result = useCase.Execute(task.Id);

            result.Should().NotBeNull();
            result.Should().Be(task);
        }

        [Fact]
        public void Execute_ShouldReturnNull_WhenTaskDoesNotExist()
        {
            var repo = new Mock<ITaskRepository>();
            var randomId = Guid.NewGuid();
            repo.Setup(r => r.GetById(randomId)).Returns((TaskItem?)null);

            var useCase = new GetTaskByIdUseCase(repo.Object);

            var result = useCase.Execute(randomId);

            result.Should().BeNull();
        }

        [Fact]
        public void Execute_ShouldCallRepositoryWithCorrectId()
        {
            var repo = new Mock<ITaskRepository>();
            var id = Guid.NewGuid();
            repo.Setup(r => r.GetById(id)).Returns((TaskItem?)null);

            var useCase = new GetTaskByIdUseCase(repo.Object);

            useCase.Execute(id);

            repo.Verify(r => r.GetById(id), Times.Once);
        }

    }
}

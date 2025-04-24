using FluentAssertions;
using Moq;
using TaskManager.Application.UseCases.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.UnitTests.Application.UseCases
{
    public class DeleteTaskUseCaseTests
    {
        [Fact]
        public void Execute_ShouldDeleteTask_WhenTaskExists()
        {
            var task = new TaskItem("título Task", "Descrição válida", DateTime.Today.AddDays(1));
            var repo = new Mock<ITaskRepository>();

            repo.Setup(r => r.GetById(task.Id)).Returns(task);
            var useCase = new DeleteTaskUseCase(repo.Object);

            var result = useCase.Execute(task.Id);

            result.Should().BeTrue();
            repo.Verify(r => r.Delete(task.Id), Times.Once);

        }

        [Fact]
        public void Execute_ShouldReturnFalse_WhenTaskDoesNotExist()
        {
            var repo = new Mock<ITaskRepository>();
            repo.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((TaskItem?)null);
            var useCase = new DeleteTaskUseCase(repo.Object);

            var result = useCase.Execute(Guid.NewGuid());

            result.Should().BeFalse();
        }
    }
}

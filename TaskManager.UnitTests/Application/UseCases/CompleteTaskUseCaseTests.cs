using FluentAssertions;
using Moq;
using TaskManager.Application.UseCases.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.UnitTests.Application.UseCases
{
    public class CompleteTaskUseCaseTests
    {
        [Fact]
        public void Execute_ShouldMarkTaskAsCompleted_WhenTaskExists()
        {
            var task = new TaskItem("Task Title", "Descrição", DateTime.Today.AddDays(1));
            var repo = new Mock<ITaskRepository>();

            repo.Setup(r => r.GetById(task.Id)).Returns(task);
            var useCase = new CompleteTaskUseCase(repo.Object);

            var result = useCase.Execute(task.Id);

            result.Should().BeTrue();
            task.IsCompleted.Should().BeTrue();

            repo.Verify(r => r.Update(It.Is<TaskItem>(t => t.Id == task.Id && t.IsCompleted == true)), Times.Once);
        }

        [Fact]
        public void Execute_ShouldReturnFalse_WhenTaskDoesExist()
        {
            var repo = new Mock<ITaskRepository>();
            repo.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((TaskItem?)null);
            var useCase = new CompleteTaskUseCase(repo.Object);
            
            var result = useCase.Execute(Guid.NewGuid());
            
            result.Should().BeFalse();

            repo.Verify(r => r.Update(It.IsAny<TaskItem>()), Times.Never);
        }
    }
}

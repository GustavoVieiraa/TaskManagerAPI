using FluentAssertions;
using Moq;
using TaskManager.Application.UseCases.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.UnitTests.Application.UseCases
{
    public class UpdateTaskUseCaseTests
    {
        [Fact]
        public void Execute_ShouldUpdateTask_WhenTaskExists()
        {
            var task = new TaskItem("Old Title", "Old Description", DateTime.Today.AddDays(1));
            var repo = new Mock<ITaskRepository>();

            repo.Setup(r => r.GetById(task.Id)).Returns(task);
            var useCase = new UpdateTaskUseCase(repo.Object);

            var result = useCase.Execute(task.Id, "New Title", "New Description", DateTime.Today.AddDays(2));

            result.Should().BeTrue();
            task.Title.Should().Be("New Title");
            task.Description.Should().Be("New Description");
            task.DueDate.Should().Be(DateTime.Today.AddDays(2));

            repo.Verify(r => r.Update(It.Is<TaskItem>(t => t.Id == task.Id && t.Title == "New Title" && t.Description == "New Description" && t.DueDate == DateTime.Today.AddDays(2))), Times.Once);
        }

        [Fact]
        public void Execute_ShouldReturnFalse_WhenTaskDoesNotExist()
        {
            var repo = new Mock<ITaskRepository>();
            repo.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((TaskItem?)null);
            var useCase = new UpdateTaskUseCase(repo.Object);

            var result = useCase.Execute(Guid.NewGuid(), "New Title", "New Description", DateTime.Today.AddDays(2));

            result.Should().BeFalse();
            repo.Verify(r => r.Update(It.IsAny<TaskItem>()), Times.Never);
        }
    }
}

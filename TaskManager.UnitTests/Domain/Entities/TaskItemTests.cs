using FluentAssertions;

using TaskManager.Domain.Entities;

namespace TaskManager.UnitTests.Domain.Entities
{
    public class TaskItemTests
    {
        [Fact]
        public void Constructor_ShouldCreateTaskItem_WhenDataIsValid()
        {
            // arrange
            var title = "Implementar Testes";
            var description = "Criar testes de domínio para TaskItem";
            var dueDate = DateTime.Today.AddDays(1);

            // act
            var task = new TaskItem(title, description, dueDate);

            //assert
            task.Title.Should().Be(title);
            task.Description.Should().Be(description);
            task.DueDate.Should().Be(dueDate);
            task.IsCompleted.Should().BeFalse();
            task.Id.Should().NotBe(Guid.Empty);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Constructor_ShouldThrow_WhenTitleIsInvalid(string invalidTitle)
        {
            Action act = () => new TaskItem(invalidTitle, "descrição válida", DateTime.Today.AddDays(1));

            act.Should().Throw<ArgumentException>().WithMessage("Title cannot be empty.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void Constructor_ShouldThrow_WhenDescriptionIsInvalid(string invalidDescription)
        {
            Action act = () => new TaskItem("titulo válido", invalidDescription, DateTime.Today.AddDays(1));

            act.Should().Throw<ArgumentException>().WithMessage("Description cannot be empty.");
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenDueDateIsInPast()
        {
            var pastDate = DateTime.Today.AddDays(-1);

            Action act = () => new TaskItem("titulo válido", "descrição válida", pastDate);

            act.Should().Throw<ArgumentException>().WithMessage("Due date must be today or in the future.");
        }

        [Fact]
        public void MarkAsCompleted_ShouldSetIsCompletedToTrue()
        {
            var task = new TaskItem("titulo válido", "descrição válida", DateTime.Today.AddDays(3));

            task.MarkAsCompleted();

            task.IsCompleted.Should().BeTrue();
        }

        [Fact]
        public void Constructor_ShouldAssignUniqueIds_ToDifferentInstances()
        {
            var task1 = new TaskItem("Task 1", "Desc 1", DateTime.Today.AddDays(5));
            var task2 = new TaskItem("Task 2", "Desc 2", DateTime.Today.AddDays(3));

            task1.Id.Should().NotBe(task2.Id);
        }

    }
}

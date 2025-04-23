using FluentAssertions;
using Moq;
using TaskManager.Application.UseCases;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;

namespace TaskManager.UnitTests.Application.UseCases
{
    public class CreateTaskUseCaseTests
    {
        [Fact]
        public void Execute_ShouldCreateTask_WhenInputIsValid()
        {
            //arrange
            var repositoryMock = new Mock<ITaskRepository>();

            string title = "Configurar Design 3D";
            string description = "Acessar o Blender e fazer configuração";
            DateTime dueDate = DateTime.Today.AddDays(5);

            TaskItem? capturedTask = null;

            repositoryMock
                .Setup(r => r.Add(It.IsAny<TaskItem>()))
                .Callback<TaskItem>(t => capturedTask = t);

            var useCase = new CreateTaskUseCase(repositoryMock.Object);
            
            //act
            useCase.Execute(title, description, dueDate);

            //assert
            capturedTask.Should().NotBeNull();
            capturedTask!.Title.Should().Be(title);
            capturedTask.Description.Should().Be(description);
            capturedTask.DueDate.Should().Be(dueDate);
            capturedTask.IsCompleted.Should().BeFalse();

            repositoryMock.Verify(r => r.Add(It.IsAny<TaskItem>()), Times.Once);
        }

        [Fact]
        public void Execute_ShouldThrowArgumentException_WhenTitleIsNullOrEmpty()
        {
            var useCase = new CreateTaskUseCase(new Mock<ITaskRepository>().Object);
            var description = "Alterar os direcionamentos de NAS";
            var dueDate = DateTime.Today.AddDays(1);
            
            Action act = () => useCase.Execute(null!, description, dueDate);

            act.Should().Throw<ArgumentException>().WithMessage("Title cannot be empty.");
        }

        [Fact]
        public void Execute_ShouldThrowArgumentException_WhenDescriptionIsNullOrEmpty()
        {
            var useCase = new CreateTaskUseCase(new Mock<ITaskRepository>().Object);
            var title = "Título válido";
            var dueDate = DateTime.Today.AddDays(1);

            Action act = () => useCase.Execute(title, null!, dueDate);

            act.Should().Throw<ArgumentException>().WithMessage("Description cannot be empty.");
        }

        [Fact]
        public void Execute_ShouldThrowArgumentException_WhenDueDateIsIntPast()
        {
            var useCase = new CreateTaskUseCase(new Mock<ITaskRepository>().Object);

            Action act = () => useCase.Execute("Título válido", "Descrição válida", DateTime.Today.AddDays(-1));

            act.Should().Throw<ArgumentException>().WithMessage("Due date must be today or in the future.");
        }
    }
}

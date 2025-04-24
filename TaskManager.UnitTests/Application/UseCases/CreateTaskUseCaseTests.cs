using FluentAssertions;
using Moq;
using TaskManager.Application.Requests.Tasks;
using TaskManager.Application.UseCases.Tasks;
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

            var request = new CreateTaskRequest(
                "Configurar Design 3D",
                "Acessar o Blender e fazer configuração",
                DateTime.Today.AddDays(5)
             );

            TaskItem? capturedTask = null;

            repositoryMock
                .Setup(r => r.Add(It.IsAny<TaskItem>()))
                .Callback<TaskItem>(t => capturedTask = t);

            var useCase = new CreateTaskUseCase(repositoryMock.Object);
            
            //act
            var response = useCase.Execute(request);

            //assert
            capturedTask.Should().NotBeNull();
            capturedTask!.Title.Should().Be(request.Title);
            capturedTask.Description.Should().Be(request.Description);
            capturedTask.DueDate.Should().Be(request.DueDate);
            capturedTask.IsCompleted.Should().BeFalse();

            response.Title.Should().Be(request.Title);
            response.Description.Should().Be(request.Description);
            response.DueDate.Should().Be(request.DueDate);
            response.IsCompleted.Should().BeFalse();

            repositoryMock.Verify(r => r.Add(It.IsAny<TaskItem>()), Times.Once);
        }

        [Fact]
        public void Execute_ShouldThrowArgumentException_WhenTitleIsNullOrEmpty()
        {
            var useCase = new CreateTaskUseCase(new Mock<ITaskRepository>().Object);
            var request = new CreateTaskRequest(
                Title: null!,
                Description: "Alterar os direcionamentos de NAS",
                DueDate: DateTime.Today.AddDays(1)
            );

            Action act = () => useCase.Execute(request);

            act.Should().Throw<ArgumentException>().WithMessage("Title cannot be empty.");
        }

        [Fact]
        public void Execute_ShouldThrowArgumentException_WhenDescriptionIsNullOrEmpty()
        {
            var useCase = new CreateTaskUseCase(new Mock<ITaskRepository>().Object);
            var request = new CreateTaskRequest(
                Title: "Título válido",
                Description: null!,
                DueDate: DateTime.Today.AddDays(1)
            );

            Action act = () => useCase.Execute(request);

            act.Should().Throw<ArgumentException>().WithMessage("Description cannot be empty.");
        }

        [Fact]
        public void Execute_ShouldThrowArgumentException_WhenDueDateIsInPast()
        {
            var useCase = new CreateTaskUseCase(new Mock<ITaskRepository>().Object);
            var request = new CreateTaskRequest(
                Title: "Título válido",
                Description: "Descrição válida",
                DueDate: DateTime.Today.AddDays(-1)
            );

            Action act = () => useCase.Execute(request);

            act.Should().Throw<ArgumentException>().WithMessage("Due date must be today or in the future.");
        }
    }
}

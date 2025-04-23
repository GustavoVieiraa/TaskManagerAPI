using System;
using FluentAssertions;
using Moq;
using TaskManager.Application.UseCases;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using Xunit;

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
    }
}

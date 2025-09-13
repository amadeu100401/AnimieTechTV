using AnimieTechTv.Application.Commad.Animie.Create;
using AnimieTechTv.Application.Handlers.Animie.CreateAnimie;
using AnimieTechTv.Domain.Entities;
using AnimieTechTv.Domain.Repositories;
using AnimieTechTv.Domain.Repositories.Animie;
using AnimieTechTv.Exceptions;
using AnimieTechTv.Exceptions.ExceptionsBase;
using AnimieTechTv.Tests.Utils;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

public class CreateAnimieHandlerTests
{
    private readonly Mock<IAnimieReadOnlyRepository> _readOnlyRepoMock;
    private readonly Mock<IAnimieWriteOnlyRepository> _writeOnlyRepoMock;
    private readonly Mock<IUnityWork> _unityWorkMock;
    private readonly Mock<ILogger<CreateAnimieHandler>> _loggerMock;
    private readonly CreateAnimieHandler _handler;
    private readonly CreateAnimieCommandValidator _validator;
    private readonly CreateAnimieCommand _createAnimieCommand;

    public CreateAnimieHandlerTests()
    {
        _readOnlyRepoMock = new Mock<IAnimieReadOnlyRepository>();
        _writeOnlyRepoMock = new Mock<IAnimieWriteOnlyRepository>();
        _unityWorkMock = new Mock<IUnityWork>();
        _loggerMock = new Mock<ILogger<CreateAnimieHandler>>();

        _createAnimieCommand = EntityBuild.GetValidCreateAnimieCommand();

        _validator = new CreateAnimieCommandValidator();

        _handler = new CreateAnimieHandler(
            _readOnlyRepoMock.Object,
            _writeOnlyRepoMock.Object,
            _unityWorkMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_Should_Create_Animie_When_Valid()
    {
        // Arrange
        var command = _createAnimieCommand;

        _readOnlyRepoMock
            .Setup(x => x.ExistisAnimieWithNameAndDirector(command.Name, command.Director))
            .ReturnsAsync(false);

        _writeOnlyRepoMock
            .Setup(x => x.Add(It.IsAny<Animies>()))
            .Returns(Task.CompletedTask);

        _unityWorkMock
            .Setup(x => x.Commit())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(command.Name);
        result.Director.Should().Be(command.Director);
        result.Resume.Should().Be(command.Resume);

        _writeOnlyRepoMock.Verify(x => x.Add(It.IsAny<Animies>()), Times.Once);
        _unityWorkMock.Verify(x => x.Commit(), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_ErrorOnValidation_When_Animie_Already_Exists()
    {
        // Arrange
        var command = EntityBuild.GetValidCreateAnimieCommand();

        _readOnlyRepoMock
            .Setup(x => x.ExistisAnimieWithNameAndDirector(command.Name, command.Director))
            .ReturnsAsync(true);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        var exception = await Assert.ThrowsAsync<ErrorOnValidation>(act);

        exception.ErrorMessage.Should().Contain(ResourceMessageExceptions.ANIMIE_ALREADY_ADDED);

        _writeOnlyRepoMock.Verify(x => x.Add(It.IsAny<Animies>()), Times.Never);
        _unityWorkMock.Verify(x => x.Commit(), Times.Never);
    }

    [Theory]
    [InlineData(null, "Director Test")]
    [InlineData("", "Director Test")]
    [InlineData("Animie Test", null)]
    [InlineData("Animie Test", "")]
    public void Should_Have_Error_When_RequiredFieldsAreMissing(string name, string director)
    {
        // Arrange
        var command = new CreateAnimieCommand
        {
            Name = name,
            Director = director,
            Resume = "Some resume"
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        if (string.IsNullOrWhiteSpace(name))
            result.ShouldHaveValidationErrorFor(c => c.Name);

        if (string.IsNullOrWhiteSpace(director))
            result.ShouldHaveValidationErrorFor(c => c.Director);
    }

}

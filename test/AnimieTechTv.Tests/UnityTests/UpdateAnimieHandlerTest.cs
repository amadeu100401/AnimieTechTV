using AnimieTechTv.Application.Commad.Animie.Update;
using AnimieTechTv.Application.Handlers.Animie.UpdateAnimie;
using AnimieTechTv.Domain.Entities;
using AnimieTechTv.Domain.Repositories;
using AnimieTechTv.Domain.Repositories.Animie;
using AnimieTechTv.Exceptions.ExceptionsBase;
using Moq;
using Xunit;

namespace AnimieTechTv.Tests.Handlers.Animie
{
    public class UpdateAnimieInfoHandlerTests
    {
        private readonly Mock<IAnimieReadOnlyRepository> _animieRepositoryMock;
        private readonly Mock<IUnityWork> _unityWorkMock;
        private readonly UpdateAnimieInfoHandler _handler;

        public UpdateAnimieInfoHandlerTests()
        {
            _animieRepositoryMock = new Mock<IAnimieReadOnlyRepository>();
            _unityWorkMock = new Mock<IUnityWork>();
            _handler = new UpdateAnimieInfoHandler(_animieRepositoryMock.Object, _unityWorkMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldUpdateAnimie_WhenAnimieExists()
        {
            // Arrange
            var animieId = Guid.NewGuid();
            var animie = new Animies
            {
                Id = animieId,
                Name = "Naruto",
                Director = "Old Director",
                Resume = "Old Resume"
            };

            _animieRepositoryMock
                .Setup(r => r.GetByIdAsync(animieId))
                .ReturnsAsync(animie);

            var command = new UpdateAnimieInfoCommand
            {
                Id = animieId,
                Name = "Naruto Shippuden",
                Director = "New Director",
                Resume = "New Resume"
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(animieId, result.AnimieIdentification);
            Assert.Equal("Naruto Shippuden", result.Name);
            Assert.Equal("New Director", result.Director);
            Assert.Equal("New Resume", result.Resume);

            _unityWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldKeepOldValues_WhenRequestPropertiesAreNull()
        {
            // Arrange
            var animieId = Guid.NewGuid();
            var animie = new Animies
            {
                Id = animieId,
                Name = "Dragon Ball",
                Director = "Akira Toriyama",
                Resume = "Saiyans"
            };

            _animieRepositoryMock
                .Setup(r => r.GetByIdAsync(animieId))
                .ReturnsAsync(animie);

            var command = new UpdateAnimieInfoCommand
            {
                Id = animieId,
                Name = null,
                Director = null,
                Resume = null
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal("Dragon Ball", result.Name);
            Assert.Equal("Akira Toriyama", result.Director);
            Assert.Equal("Saiyans", result.Resume);

            _unityWorkMock.Verify(u => u.Commit(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenAnimieNotFound()
        {
            // Arrange
            var animieId = Guid.NewGuid();
            _animieRepositoryMock
                .Setup(r => r.GetByIdAsync(animieId))
                .ReturnsAsync((Animies?)null);

            var command = new UpdateAnimieInfoCommand
            {
                Id = animieId,
                Name = "One Piece"
            };

            // Act & Assert
            await Assert.ThrowsAsync<AnimieTechTVException>(
                () => _handler.Handle(command, CancellationToken.None));

            _unityWorkMock.Verify(u => u.Commit(), Times.Never);
        }
    }
}

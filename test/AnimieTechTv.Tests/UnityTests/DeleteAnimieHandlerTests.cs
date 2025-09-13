using AnimieTechTv.Application.Commad.Animie.Delete;
using AnimieTechTv.Application.Handlers.Animie.DeleteAnimie;
using AnimieTechTv.Domain.Repositories.Animie;
using AnimieTechTv.Exceptions;
using AnimieTechTv.Exceptions.ExceptionsBase;
using Moq;
using Xunit;

namespace AnimieTechTv.Tests.Handlers.Animie
{
    public class DeleteAnimieHandlerTests
    {
        private readonly Mock<IAnimieDeleteRepository> _deleteRepositoryMock;
        private readonly DeleteAnimieHandler _handler;

        public DeleteAnimieHandlerTests()
        {
            _deleteRepositoryMock = new Mock<IAnimieDeleteRepository>();
            _handler = new DeleteAnimieHandler(_deleteRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenDeleteSucceeds()
        {
            // Arrange
            var animieId = Guid.NewGuid();
            var command = new DeleteAnimieCommand(animieId);

            _deleteRepositoryMock
                .Setup(r => r.DeleteAnimieByIdAsync(animieId))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _deleteRepositoryMock.Verify(r => r.DeleteAnimieByIdAsync(animieId), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenDeleteFails()
        {
            // Arrange
            var animieId = Guid.NewGuid();
            var command = new DeleteAnimieCommand(animieId);

            _deleteRepositoryMock
                .Setup(r => r.DeleteAnimieByIdAsync(animieId))
                .ReturnsAsync(false);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<AnimieTechTVException>(
                () => _handler.Handle(command, CancellationToken.None));

            Assert.Equal(ResourceMessageExceptions.ANIMIE_ERROR_ON_DELETED, exception.Message);
            _deleteRepositoryMock.Verify(r => r.DeleteAnimieByIdAsync(animieId), Times.Once);
        }
    }
}

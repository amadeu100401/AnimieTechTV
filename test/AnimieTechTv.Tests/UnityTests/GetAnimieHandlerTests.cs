using AnimieTechTv.Application.Commad.Animie.Get;
using AnimieTechTv.Application.Handlers.Animie.GetAnimie;
using AnimieTechTv.Domain.DTOs;
using AnimieTechTv.Domain.DTOs.LocalAnimie;
using AnimieTechTv.Domain.Entities;
using AnimieTechTv.Domain.Repositories.Animie;
using AnimieTechTv.Tests.Utils;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AnimieTechTv.Tests.UnityTests;

public class GetAnimieHandlerTests
{
    private readonly Mock<IAnimieReadOnlyRepository> _repositoryMock;
    private readonly Mock<ILogger<GetAnimieHandler>> _loggerMock;
    private readonly GetAnimieHandler _handler;

    public GetAnimieHandlerTests()
    {
        _repositoryMock = new Mock<IAnimieReadOnlyRepository>();
        _loggerMock = new Mock<ILogger<GetAnimieHandler>>();
        _handler = new GetAnimieHandler(_loggerMock.Object, _repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllAnimies_WhenNoFilterProvided()
    {
        //Arrange
        var pagination = new PaginationDTO { PageNumber = 1, PageSize = 10 };
        var command = EntityBuild.GetValidGetAnimieCommand(pagination: pagination);

        var paginationResult = new PaginationResultDTO<Animies>
        {
            TotalItem = 2,
            PageNumber = 1,
            PageSize = 10,
            Items =
            [
                new Animies { Id = Guid.NewGuid(), Name = "Animie1", Director = "Director1", Resume = "Resume1" },
                new Animies { Id = Guid.NewGuid(), Name = "Animie2", Director = "Director2", Resume = "Resume2" }
            ]
        };

        _repositoryMock.Setup(r => r.GetAllAnimies(pagination))
            .ReturnsAsync(paginationResult);

        //Act
        var result = await _handler.Handle(command, default);

        //Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Items.Count);
        Assert.Equal(2, result.Pagination.TotalItem);
        _repositoryMock.Verify(r => r.GetAllAnimies(pagination), Times.Once);
    }


    [Fact]
    public async Task Handle_ShouldReturnFilteredAnimies_WhenFiltersProvided()
    {
        // Arrange
        var pagination = new PaginationDTO { PageNumber = 1, PageSize = 10 };
        var command = new GetAnimieCommand(pagination)
        {
            Name = "Naruto"
        };

        var filteredResult = new List<Animies>
        {
            new Animies { Id = Guid.NewGuid(), Name = "Naruto", Director = "Hayato Date", Resume = "Ninja boy" }
        };

        _repositoryMock.Setup(r => r.GetAnimieByFilter(It.IsAny<GetAnimieFilterDTO>()))
            .ReturnsAsync(filteredResult);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Items);
        Assert.Equal("Naruto", result.Items.First().Name);
        _repositoryMock.Verify(r => r.GetAnimieByFilter(It.IsAny<GetAnimieFilterDTO>()), Times.Once);
    }
}

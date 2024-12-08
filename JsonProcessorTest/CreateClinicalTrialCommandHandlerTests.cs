using Application.Commands.Create;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Moq;
using Xunit;

public class CreateClinicalTrialCommandHandlerTests
{
    private readonly Mock<IClinicalTrialRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly CreateClinicalTrialCommandHandler _handler;

    public CreateClinicalTrialCommandHandlerTests()
    {
        _repositoryMock = new Mock<IClinicalTrialRepository>();
        _mapperMock = new Mock<IMapper>();

        _handler = new CreateClinicalTrialCommandHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Should_Create_Clinical_Trial_Successfully()
    {
        // Arrange
        var command = new CreateClinicalTrialCommand
        {
            TrialId = "CT-001",
            Title = "Clinical Trial Study",
            StartDate = DateTime.UtcNow.AddDays(-10),
            Participants = 50,
            Status = TrialStatus.NotStarted
        };

        var mappedTrial = new ClinicalTrial
        {
            TrialId = command.TrialId,
            Title = command.Title,
            StartDate = command.StartDate,
            Participants = command.Participants,
            Status = command.Status
        };

        // Configure mock for mapper
        _mapperMock.Setup(m => m.Map<ClinicalTrial>(command)).Returns(mappedTrial);

        // Configure mock for repository
        _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<ClinicalTrial>(), CancellationToken.None))
                       .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<ClinicalTrial>(), CancellationToken.None), Times.Once);
        Assert.True(result == 0); // Assuming result is the default ID of the created trial
    }

    [Fact]
    public async Task Should_Throw_Exception_On_Failure()
    {
        // Arrange
        _repositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<ClinicalTrial>(), CancellationToken.None))
            .ThrowsAsync(new Exception("Database error"));

        var command = new CreateClinicalTrialCommand
        {
            TrialId = "CT-001",
            Title = "Clinical Trial Study",
            StartDate = DateTime.UtcNow.AddDays(-10),
            Participants = 50,
            Status = TrialStatus.NotStarted
        };

        var mappedTrial = new ClinicalTrial
        {
            TrialId = command.TrialId,
            Title = command.Title,
            StartDate = command.StartDate,
            Participants = command.Participants,
            Status = command.Status
        };

        _mapperMock.Setup(m => m.Map<ClinicalTrial>(command)).Returns(mappedTrial);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal("Database error", exception.Message);
    }
}
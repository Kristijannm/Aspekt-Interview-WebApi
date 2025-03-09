using CompanyContacts.Application.Features.Companies.CreateCompany;
using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Interfaces;
using FluentAssertions;
using Moq;

namespace CompanyContacts.UnitTests.Application.CompanyTests;

public class CreateCompanyCommandHandlerTests
{
    private Mock<ICompanyRepository> _mockCompanyRepo;
    private CreateCompanyCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _mockCompanyRepo = new Mock<ICompanyRepository>();
        _handler = new CreateCompanyCommandHandler(_mockCompanyRepo.Object);
    }

    [Test]
    public async Task Handle_ValidRequest_ShouldCreateCompanyAndReturnId()
    {
        // Arrange
        var command = new CreateCompanyCommand("TechCorp");
        var expectedCompany = new Company { Id = 1, Name = "TechCorp" };

        _mockCompanyRepo
            .Setup(repo => repo.AddAsync(It.IsAny<Company>()))
            .Callback<Company>(c => c.Id = 1) // simulating auto-increment
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(expectedCompany.Id);
        _mockCompanyRepo.Verify(repo => repo.AddAsync(It.IsAny<Company>()), Times.Once);
    }

    [Test]
    public async Task Handle_EmptyCompanyName_ShouldThrowException()
    {
        // Arrange
        var command = new CreateCompanyCommand("");

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage("Company name cannot be empty*");
    }
}
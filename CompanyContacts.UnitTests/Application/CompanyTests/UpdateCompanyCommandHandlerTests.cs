using CompanyContacts.Application.Features.Companies.UpdateCompany;
using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Interfaces;
using FluentAssertions;
using Moq;

namespace CompanyContacts.UnitTests.Application.CompanyTests;

public class UpdateCompanyCommandHandlerTests
{
    private Mock<ICompanyRepository> _mockCompanyRepo;
    private UpdateCompanyCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _mockCompanyRepo = new Mock<ICompanyRepository>();
        _handler = new UpdateCompanyCommandHandler(_mockCompanyRepo.Object);
    }

    [Test]
    public async Task Handle_ValidCommand_UpdatesCompany()
    {
        // Arrange
        var companyId = 1;
        var newName = "Updated Company";
        var command = new UpdateCompanyCommand(companyId, newName);

        var existingCompany = new Company
        {
            Id = companyId,
            Name = "Old Company"
        };

        _mockCompanyRepo.Setup(repo => repo.GetByIdAsync(companyId))
            .ReturnsAsync(existingCompany);

        _mockCompanyRepo.Setup(repo => repo.UpdateAsync(It.IsAny<Company>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(companyId);
        result.Name.Should().Be(newName);

        _mockCompanyRepo.Verify(repo => repo.UpdateAsync(It.Is<Company>(c => c.Id == companyId && c.Name == newName)), Times.Once);
    }
}

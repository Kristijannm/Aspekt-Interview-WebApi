using CompanyContacts.Application.Exceptions;
using CompanyContacts.Application.Features.Companies.DeleteCompany;
using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Interfaces;
using FluentAssertions;
using Moq;

namespace CompanyContacts.UnitTests.Application.CompanyTests;

public class DeleteCompanyCommandHandlerTests
{
    private Mock<ICompanyRepository> _mockCompanyRepo;
    private DeleteCompanyCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _mockCompanyRepo = new Mock<ICompanyRepository>();
        _handler = new DeleteCompanyCommandHandler(_mockCompanyRepo.Object);
    }

    [Test]
    public async Task Handle_ExistingCompany_ShouldCallDeleteAsync()
    {
        // Arrange
        var companyId = 1;
        var company = new Company { Id = companyId, Name = "Test Company" };
        var command = new DeleteCompanyCommand(companyId);

        _mockCompanyRepo.Setup(repo => repo.GetByIdAsync(companyId))
            .ReturnsAsync(company);

        _mockCompanyRepo.Setup(repo => repo.DeleteAsync(company))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockCompanyRepo.Verify(repo => repo.GetByIdAsync(companyId), Times.Once);
        _mockCompanyRepo.Verify(repo => repo.DeleteAsync(company), Times.Once);
    }

    [Test]
    public async Task Handle_NonExistingCompany_ShouldThrowNotFoundException()
    {
        // Arrange
        var companyId = 99;
        var command = new DeleteCompanyCommand(companyId);

        _ = _mockCompanyRepo.Setup(repo => repo.GetByIdAsync(companyId))
            .ReturnsAsync(null as Company); // Simulate company not found

        // Act
        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Company not found");

        _mockCompanyRepo.Verify(repo => repo.DeleteAsync(It.IsAny<Company>()), Times.Never);
    }
}

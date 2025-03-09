using CompanyContacts.Application.Features.Companies.GetCompany;
using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Interfaces;
using FluentAssertions;
using Moq;

namespace CompanyContacts.UnitTests.Application.CompanyTests;

public class GetCompanyQueryHandlerTests
{
    private Mock<ICompanyRepository> _mockCompanyRepo;
    private GetCompanyQueryHandler _handler;

    [SetUp]
    public void Setup()
    {
        _mockCompanyRepo = new Mock<ICompanyRepository>();
        _handler = new GetCompanyQueryHandler(_mockCompanyRepo.Object);
    }

    [Test]
    public async Task Handle_ValidCompanyId_ReturnsCompany()
    {
        // Arrange
        var companyId = 1;
        var company = new Company { Id = companyId, Name = "Test Company" };
        var command = new GetCompanyQuery(companyId);

        _mockCompanyRepo.Setup(repo => repo.GetByIdAsync(companyId))
            .ReturnsAsync(company);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(companyId);
        result.Name.Should().Be("Test Company");
    }
}
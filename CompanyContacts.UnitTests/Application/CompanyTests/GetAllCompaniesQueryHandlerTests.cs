using CompanyContacts.Application.Features.Companies.GetAllCompanies;
using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Interfaces;
using FluentAssertions;
using Moq;

namespace CompanyContacts.UnitTests.Application.CompanyTests;

public class GetAllCompaniesQueryHandlerTests
{
    private Mock<ICompanyRepository> _mockCompanyRepo;
    private GetAllCompaniesQueryHandler _handler;

    [SetUp]
    public void Setup()
    {
        _mockCompanyRepo = new Mock<ICompanyRepository>();
        _handler = new GetAllCompaniesQueryHandler(_mockCompanyRepo.Object);
    }

    [Test]
    public async Task Handle_ValidQuery_ReturnsPagedResult()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 5;
        var companies = new List<Company>
    {
        new() { Id = 1, Name = "Company 1" },
        new() { Id = 2, Name = "Company 2" }
    };
        var totalCount = 10;
        var command = new GetAllCompaniesQuery(pageNumber, pageSize);

        _mockCompanyRepo.Setup(repo => repo.GetAllPagedAsync(pageNumber, pageSize))
            .ReturnsAsync((companies, totalCount));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Items.Should().HaveCount(companies.Count);
        result.TotalCount.Should().Be(totalCount);
        result.TotalPages.Should().Be(2);
    }

    [Test]
    public async Task Handle_NoCompanies_ReturnsEmptyPagedResult()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 5;
        var companies = new List<Company>();
        var totalCount = 0;
        var command = new GetAllCompaniesQuery(pageNumber, pageSize);

        _mockCompanyRepo.Setup(repo => repo.GetAllPagedAsync(pageNumber, pageSize))
            .ReturnsAsync((companies, totalCount));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Items.Should().BeEmpty();
        result.TotalCount.Should().Be(0);
        result.TotalPages.Should().Be(0);


    }

    [Test]
    public async Task Handle_ShouldCallGetAllPagedAsyncOnce()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 5;
        var companies = new List<Company>
    {
        new() { Id = 1, Name = "Company 1" },
        new() { Id = 2, Name = "Company 2" }
    };
        var totalCount = 10;
        var command = new GetAllCompaniesQuery(pageNumber, pageSize);

        _mockCompanyRepo.Setup(repo => repo.GetAllPagedAsync(pageNumber, pageSize))
            .ReturnsAsync((companies, totalCount));

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockCompanyRepo.Verify(repo => repo.GetAllPagedAsync(pageNumber, pageSize), Times.Once);
    }
}

using CompanyContacts.Application.Features.Companies.CreateCompany;
using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Interfaces;
using Moq;
using Xunit;

namespace CompanyContacts.Tests.Application
{
    public class CreateCompanyCommandHandlerTests
    {
        private readonly Mock<ICompanyRepository> _mockCompanyRepo;
        private readonly CreateCompanyCommandHandler _handler;

        public CreateCompanyCommandHandlerTests()
        {
            _mockCompanyRepo = new Mock<ICompanyRepository>();

            _handler = new CreateCompanyCommandHandler(_mockCompanyRepo.Object);
        }

        [Fact]
        public async Task Handle_ValidRequest_ReturnsCompanyId()
        {
            // Arrange
            var command = new CreateCompanyCommand("Test Company");
            var company = new Company { Id = 1, Name = "Test Company" };

            _mockCompanyRepo.Setup(repo => repo.AddAsync(It.IsAny<Company>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(company.Id, result);
            _mockCompanyRepo.Verify(repo => repo.AddAsync(It.IsAny<Company>()), Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ReturnsDefaultCompanyId()
        {
            // Arrange
            var command = new CreateCompanyCommand("");
            var company = new Company { Id = 0, Name = "" };

            _mockCompanyRepo.Setup(repo => repo.AddAsync(It.IsAny<Company>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(company.Id, result);
            _mockCompanyRepo.Verify(repo => repo.AddAsync(It.IsAny<Company>()), Times.Once);
        }
    }
}

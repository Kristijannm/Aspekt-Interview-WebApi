using CompanyContacts.Application.Exceptions;
using CompanyContacts.Infrastructure.Interfaces;
using MediatR;

namespace CompanyContacts.Application.Features.Companies.DeleteCompany;

public sealed class DeleteCompanyCommandHandler(ICompanyRepository companyRepo) : IRequestHandler<DeleteCompanyCommand>
{
    private readonly ICompanyRepository _companyRepo = companyRepo;

    public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepo.GetByIdAsync(request.Id) ??
            throw new NotFoundException("Company not found");

        await _companyRepo.DeleteAsync(company);
    }
}
using CompanyContacts.Application.Exceptions;
using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Interfaces;
using MediatR;

namespace CompanyContacts.Application.Features.Companies.UpdateCompany;

public sealed class UpdateCompanyCommandHandler(ICompanyRepository companyRepository) :
    IRequestHandler<UpdateCompanyCommand, Company>
{
    private readonly ICompanyRepository _companyRepo = companyRepository ?? 
        throw new ArgumentNullException(nameof(companyRepository));

    public async Task<Company> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _companyRepo.GetByIdAsync(request.Id) ??
            throw new NotFoundException("Company not found");

        company.Name = request.Name;

        await _companyRepo.UpdateAsync(company);

        return company;
    }
}
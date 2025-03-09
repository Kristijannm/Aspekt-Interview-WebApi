using CompanyContacts.Application.Exceptions;
using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Interfaces;
using MediatR;

namespace CompanyContacts.Application.Features.Companies.GetCompany;

public sealed class GetCompanyQueryHandler(ICompanyRepository companyRepo) : IRequestHandler<GetCompanyQuery, Company?>
{
    private readonly ICompanyRepository _companyRepo = companyRepo;

    public async Task<Company?> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        return await _companyRepo.GetByIdAsync(request.Id) ??
            throw new NotFoundException("Company Not Found");
    }
}
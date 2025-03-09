using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Interfaces;
using CompanyContacts.Shared;
using MediatR;

namespace CompanyContacts.Application.Features.Companies.GetAllCompanies;

public sealed class GetAllCompaniesQueryHandler(ICompanyRepository companyRepo) : IRequestHandler<GetAllCompaniesQuery, PagedResult<Company>>
{
    private readonly ICompanyRepository _companyRepo = companyRepo;

    public async Task<PagedResult<Company>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
    {
        var (companies, totalCount) = await _companyRepo.GetAllPagedAsync(request.PageNumber, request.PageSize);

        return new PagedResult<Company>(companies, totalCount, request.PageSize);
    }

}
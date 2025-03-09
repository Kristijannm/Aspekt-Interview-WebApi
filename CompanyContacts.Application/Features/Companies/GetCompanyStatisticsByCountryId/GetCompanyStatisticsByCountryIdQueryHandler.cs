using CompanyContacts.Infrastructure.Interfaces;
using MediatR;

namespace CompanyContacts.Application.Features.Companies.GetCompanyStatisticsByCountryId;

public sealed class GetCompanyStatisticsByCountryIdQueryHandler(ICompanyRepository companyRepo) : 
    IRequestHandler<GetCompanyStatisticsByCountryIdQuery, Dictionary<string, int>>
{
    private readonly ICompanyRepository _companyRepo = companyRepo ??
        throw new ArgumentNullException(nameof(companyRepo));

    public async Task<Dictionary<string, int>> Handle(GetCompanyStatisticsByCountryIdQuery request, CancellationToken cancellationToken)
    {
        return await _companyRepo.GetCompanyStatisticsByCountryIdAsync(request.CountryId);
    }
}
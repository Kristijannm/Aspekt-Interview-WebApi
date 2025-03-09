using MediatR;

namespace CompanyContacts.Application.Features.Companies.GetCompanyStatisticsByCountryId;

public sealed record GetCompanyStatisticsByCountryIdQuery(int CountryId) : IRequest<Dictionary<string, int>>;
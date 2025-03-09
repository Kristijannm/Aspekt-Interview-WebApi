using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Interfaces;
using MediatR;

namespace CompanyContacts.Application.Features.Countries.GetAllCountries;

public sealed class GetAllCountriesQueryHandler(ICountryRepository countryRepo) : IRequestHandler<GetAllCountriesQuery, IEnumerable<Country>>
{
    private readonly ICountryRepository _countryRepo = countryRepo;

    public async Task<IEnumerable<Country>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        return await _countryRepo.GetAllAsync();
    }
}

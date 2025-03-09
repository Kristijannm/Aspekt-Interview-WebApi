using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Interfaces;
using MediatR;

namespace CompanyContacts.Application.Features.Countries.GetCountry;

public sealed class GetCountryQueryHandler(ICountryRepository countryRepo) : IRequestHandler<GetCountryQuery, Country?>
{
    private readonly ICountryRepository _countryRepo = countryRepo;

    public async Task<Country?> Handle(GetCountryQuery request, CancellationToken cancellationToken)
    {
        return await _countryRepo.GetByIdAsync(request.Id);
    }
}
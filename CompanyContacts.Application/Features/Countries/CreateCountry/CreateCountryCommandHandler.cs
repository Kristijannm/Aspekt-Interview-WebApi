using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Interfaces;
using MediatR;

namespace CompanyContacts.Application.Features.Countries.CreateCountry;

public sealed class CreateCountryCommandHandler(ICountryRepository countryRepo) : IRequestHandler<CreateCountryCommand, Country>
{
    private readonly ICountryRepository _countryRepo = countryRepo;

    public async Task<Country> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = new Country { Name = request.Name };
        await _countryRepo.AddAsync(country);

        return country;
    }
}

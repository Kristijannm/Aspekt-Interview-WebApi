using CompanyContacts.Application.Exceptions;
using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Interfaces;
using MediatR;

namespace CompanyContacts.Application.Features.Countries.UpdateCountry;

public sealed class UpdateCountryCommandHandler(ICountryRepository countryRepo) : IRequestHandler<UpdateCountryCommand, Country>
{
    private readonly ICountryRepository _countryRepo = countryRepo ?? 
        throw new ArgumentNullException(nameof(countryRepo));

    public async Task<Country> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await _countryRepo.GetByIdAsync(request.Id) ?? 
            throw new NotFoundException("Country not found");
        
        country.Name = request.Name;
        await _countryRepo.UpdateAsync(country);

        return country;
    }
}

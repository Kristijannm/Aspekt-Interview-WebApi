using CompanyContacts.Application.Exceptions;
using CompanyContacts.Infrastructure.Interfaces;
using MediatR;

namespace CompanyContacts.Application.Features.Countries.DeleteCountry;

public sealed class DeleteCountryCommandHandler(ICountryRepository countryRepo) : IRequestHandler<DeleteCountryCommand>
{
    private readonly ICountryRepository _countryRepo = countryRepo;

    public async Task Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        var country = await _countryRepo.GetByIdAsync(request.Id) ?? 
            throw new NotFoundException("Country not found");
        
        await _countryRepo.DeleteAsync(country);
    }
}
using MediatR;

namespace CompanyContacts.Application.Features.Countries.DeleteCountry;

public sealed record DeleteCountryCommand(int Id) : IRequest;
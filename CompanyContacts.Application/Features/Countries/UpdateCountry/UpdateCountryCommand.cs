using CompanyContacts.Domain.Models;
using MediatR;

namespace CompanyContacts.Application.Features.Countries.UpdateCountry;

public sealed record UpdateCountryCommand(int Id, string Name) : IRequest<Country>;

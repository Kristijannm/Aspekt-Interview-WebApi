using CompanyContacts.Domain.Models;
using MediatR;

namespace CompanyContacts.Application.Features.Countries.CreateCountry;

public sealed record CreateCountryCommand(string Name) : IRequest<Country>;
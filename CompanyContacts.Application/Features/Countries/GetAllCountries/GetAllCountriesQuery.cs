using CompanyContacts.Domain.Models;
using MediatR;

namespace CompanyContacts.Application.Features.Countries.GetAllCountries;

public sealed record GetAllCountriesQuery : IRequest<IEnumerable<Country>>;

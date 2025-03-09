using CompanyContacts.Domain.Models;
using MediatR;

namespace CompanyContacts.Application.Features.Countries.GetCountry;

public sealed record GetCountryQuery(int Id) : IRequest<Country?>;

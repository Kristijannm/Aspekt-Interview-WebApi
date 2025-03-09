using CompanyContacts.Domain.Models;
using MediatR;

namespace CompanyContacts.Application.Features.Companies.GetCompany;

public sealed record GetCompanyQuery(int Id) : IRequest<Company?>;
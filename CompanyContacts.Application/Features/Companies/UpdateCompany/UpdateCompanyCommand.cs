using CompanyContacts.Domain.Models;
using MediatR;

namespace CompanyContacts.Application.Features.Companies.UpdateCompany;

public sealed record UpdateCompanyCommand(int Id, string Name) : IRequest<Company>;
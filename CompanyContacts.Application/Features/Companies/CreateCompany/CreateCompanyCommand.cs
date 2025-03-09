using MediatR;

namespace CompanyContacts.Application.Features.Companies.CreateCompany;

public sealed record CreateCompanyCommand(string Name) : IRequest<int>;
using MediatR;

namespace CompanyContacts.Application.Features.Companies.DeleteCompany;

public sealed record DeleteCompanyCommand(int Id) : IRequest;
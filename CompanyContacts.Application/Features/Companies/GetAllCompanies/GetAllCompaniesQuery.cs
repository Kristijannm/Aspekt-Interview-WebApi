using CompanyContacts.Domain.Models;
using CompanyContacts.Shared;
using MediatR;

namespace CompanyContacts.Application.Features.Companies.GetAllCompanies;

public sealed record GetAllCompaniesQuery(int PageNumber, int PageSize) : IRequest<PagedResult<Company>>;
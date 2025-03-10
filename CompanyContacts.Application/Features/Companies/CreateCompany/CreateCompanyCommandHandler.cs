﻿using CompanyContacts.Domain.Models;
using CompanyContacts.Infrastructure.Interfaces;
using MediatR;

namespace CompanyContacts.Application.Features.Companies.CreateCompany;

public sealed class CreateCompanyCommandHandler(ICompanyRepository companyRepo) : IRequestHandler<CreateCompanyCommand, int>
{
    private readonly ICompanyRepository _companyRepo = companyRepo;

    public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            throw new ArgumentException("Company name cannot be empty", nameof(request.Name));
        }

        var company = new Company
        {
            Name = request.Name
        };

        await _companyRepo.AddAsync(company);
        return company.Id;
    }
}
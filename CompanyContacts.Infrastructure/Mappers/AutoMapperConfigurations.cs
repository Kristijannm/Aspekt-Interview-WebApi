using AutoMapper;
using CompanyContacts.Domain.Models;
using CompanyContacts.Shared.DTOs;
using CompanyContacts.Shared.Helpers;

namespace CompanyContacts.Infrastructure.Mappers;

public static class AutoMapperConfiguration
{
    public static MapperConfiguration GetConfiguration()
    {
        return new MapperConfiguration(x =>
        {
            x.CreateMap<RegisterUserDto, User>()
                .AfterMap((src, dest) => dest.Password = HashingHelper.HashPassword(src.Password));
        });
    }
}
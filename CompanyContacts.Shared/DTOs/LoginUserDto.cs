﻿namespace CompanyContacts.Shared.DTOs;

public sealed class LoginUserDto
{
    public required string Username { get; set; }
    public required string Password { get; set; } 
}
﻿using Data.Enums;
using Microsoft.AspNetCore.Identity;

namespace Data.Models;

public class User : IdentityUser
{
    public int? OrganizationId { get; set; }
    public Organization? Organization { get; set; }

    public UserType? UserType { get; set; } = Enums.UserType.Company;
}
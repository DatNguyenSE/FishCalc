using System;
using Microsoft.AspNetCore.Identity;

namespace FishCalc.Web.Data;

public class AppUser : IdentityUser
{
    public required string FullName { get; set; }
    public string? ImageUrl { get; set; }

}

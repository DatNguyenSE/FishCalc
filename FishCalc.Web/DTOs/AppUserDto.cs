using System;

namespace FishCalc.Web.DTOs
{
    public class AppUserDto
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }
}

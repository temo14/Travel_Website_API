global using System.ComponentModel.DataAnnotations;

namespace Altex_Task.Models.Profile
{
    public record LoginModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}

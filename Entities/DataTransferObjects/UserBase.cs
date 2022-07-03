using Entities.Helper;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class UserBase: IEntity
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(60, ErrorMessage = "LastName can't be longer than 60 characters")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(60, ErrorMessage = "Email can't be longer than 60 characters")]
        public string Email { get; set; } = string.Empty;
        public string? Image { get; set; }
    }
}

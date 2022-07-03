using Entities.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class UserCreationDto: UserBase, IEntity
    {
        [Required(ErrorMessage = "Password is required")]
        [StringLength(60, ErrorMessage = "Password can't be longer than 60 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }
    }
}

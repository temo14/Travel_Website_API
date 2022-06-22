using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class AppartmentCreationDto
    {
        [Required(ErrorMessage = "City created is required")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Adress created is required")]
        public string? Address { get; set; }
        public int NumOfBeds { get; set; }
        public string? PhotoLocation { get; set; }
        public string? DistanceFromCenter { get; set; }
        public string? Description { get; set; }
        public Guid OwnerId { get; set; }

    }
}

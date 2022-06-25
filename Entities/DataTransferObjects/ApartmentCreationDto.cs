using Entities.Helper;
using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class ApartmentCreationDto
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
        public bool Pool { get; set; } = false;
        public bool Gym { get; set; } = false;
        public bool Wifi { get; set; } = false;
        public bool Parking { get; set; } = false;
    }
}

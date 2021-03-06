using Entities.Helper;
using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class ApartmentBase : Amenities
    {
        public string City { get; set; } = string.Empty;
        public string? Address { get; set; }
        public int NumOfBeds { get; set; } = 1;
        public string? Image { get; set; }
        public int DistanceFromCenter { get; set; }
        public string? Description { get; set; }
    }
}

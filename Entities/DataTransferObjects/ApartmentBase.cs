using Entities.Helper;
using Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class ApartmentBase : Amenities
    {
        public string? City { get; set; }
        public string? Address { get; set; }
        public int NumOfBeds { get; set; }
        public string? Image { get; set; }
        public int DistanceFromCenter { get; set; }
        public string? Description { get; set; }
    }
}

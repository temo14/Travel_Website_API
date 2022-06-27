using Entities.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Apartments
    {
        public Guid Id { get; set; }

        public string? City { get; set; }
        public string? Address { get; set; }
        public int NumOfBeds { get; set; }
        public string? Image { get; set; }
        public int DistanceFromCenter { get; set; }
        public string? Description { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public Guid OwnerId { get; set; }
        public bool Pool { get; set; } = false;
        public bool Gym { get; set; } = false;
        public bool Wifi { get; set; } = false;
        public bool Parking { get; set; } = false;

    }
}

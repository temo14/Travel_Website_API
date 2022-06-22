using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Appartments
    {
        [Column("AppartmentId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "City created is required")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Adress created is required")]
        public string? Address { get; set; }
        public int NumOfBeds { get; set; }
        public string? PhotoLocation { get; set; }
        public string? DistanceFromCenter { get; set; }
        public string? Description { get; set; }

        [ForeignKey(nameof(Owner))]
        public Guid OwnerId { get; set; }
        public User? Owner { get; set; }

    }
}

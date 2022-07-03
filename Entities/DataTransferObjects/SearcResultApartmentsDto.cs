using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class SearcResultApartmentsDto
    {
        public Guid? Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string? Address { get; set; }
        public int NumOfBeds { get; set; }
        public string? Image { get; set; }
        public int? DistanceFromCenter { get; set; }
        public string? Description { get; set; }
        public Guid? OwnerId { get; set; }
        public bool? Avaliable { get; set; }

    }
}

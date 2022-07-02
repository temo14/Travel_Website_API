using Entities.DataTransferObjects;
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
    public class Apartments : ApartmentBase
    {
        public Guid? Id { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public Guid? OwnerId { get; set; }

    }
}

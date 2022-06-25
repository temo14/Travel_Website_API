using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class BookingGuestAddDto
    {
        public Guid GuestId { get; set; }
        public Guid HostId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}

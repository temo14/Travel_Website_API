using Entities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities.Models
{
    public class BookingGuests
    {
        public Guid Id { get; set; }
        public Guid GuestId { get; set; } = Guid.NewGuid();
        public Guid HostId { get; set; } = Guid.NewGuid();
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Status status { get; set; }
    }
}

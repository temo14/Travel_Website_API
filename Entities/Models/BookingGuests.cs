using Entities.DataTransferObjects;
using Entities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entities.Models
{
    public class BookingGuests : BookingGuestAddDto
    {
        public Guid Id { get; set; }
        public Guid? GuestId { get; set; }
        public Status status { get; set; }
    }
}

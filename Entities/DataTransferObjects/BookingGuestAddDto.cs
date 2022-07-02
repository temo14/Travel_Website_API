using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class BookingGuestAddDto : DateTimes
    {
        public Guid HostId { get; set; }
    }
}

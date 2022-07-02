using Entities.Helper;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ReturnBookingsDto : DateTimes
    {
        public Apartments? Apartment { get; set; }
        public Status Status { get; set; }
        public string? Image { get; set; }
       
    }
}

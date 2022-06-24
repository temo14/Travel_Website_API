using Entities.Helper;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ReturnBookingsDto
    {
        public Appartments? appartment { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public Status status { get; set; }
        public string? PhotoUrl { get; set; }
       
    }
}

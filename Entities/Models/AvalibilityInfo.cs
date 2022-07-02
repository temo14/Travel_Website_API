using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class AvaliabilityInfo : DateTimes
    {
        public User? Guest { get; set; }
        public string? Description { get; set; }
    }
}

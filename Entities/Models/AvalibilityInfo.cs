using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class AvaliabilityInfo
    {
        public User? Guest { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}

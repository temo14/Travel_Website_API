﻿using Entities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ApartmentDetails
    {
        public Apartments? Apartment { get; set; }

        public IQueryable<AvaliabilityInfo> Avalibilities { get; set; }
    }
}

﻿using Entities.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class SearchParameters :QueryStringParameters
    {
        public string? City { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string? OrderBy { get; set; }
        public int Bedsfilter { get; set; }
    }
}

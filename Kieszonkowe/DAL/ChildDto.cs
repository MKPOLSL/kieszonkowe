﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kieszonkowe.DAL
{
    public class ChildDto
    {
        public string Name { get; set; }
        public string Education { get; set; }
        public int PlannedAmount { get; set; }
        public int RealAmount { get; set; }
        public string Region { get; set; } 
    }
}
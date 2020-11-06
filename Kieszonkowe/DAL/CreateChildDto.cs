using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kieszonkowe.DAL
{
    public class CreateChildDto
    {
        public string Education { get; set; }
        public string Region { get; set; }
        public int PlannedAmount { get; set; }
        public int RealAmount { get; set; }
    }
}

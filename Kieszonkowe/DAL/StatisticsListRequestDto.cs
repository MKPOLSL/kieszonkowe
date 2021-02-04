using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kieszonkowe.DAL
{
    public class StatisticsListRequestDto
    {
        public Guid educationId { get; set; }
        public bool isCity { get; set; }
    }
}

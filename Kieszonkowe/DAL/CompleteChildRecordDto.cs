using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kieszonkowe.DAL
{
    public class CompleteChildRecordDto
    {
        public Guid ChildId { get; set; }
        public int ActualAmount { get; set; }
    }
}

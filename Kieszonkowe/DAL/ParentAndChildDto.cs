using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kieszonkowe.DAL
{
    public class ParentAndChildDto
    {
        public Guid ParentId { get; set; }
        public ChildDto Child { get; set; }
    }
}

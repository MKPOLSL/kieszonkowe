using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kieszonkowe.DAL
{
    public class ParentChangeDataDto
    {
        public Guid Id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string birthDate { get; set; }
    }
}

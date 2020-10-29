using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kieszonkowe.Entities
{
    public class Parent : User
    {
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<ChildRecord> Children { get; set; }
        public bool IsActive { get; set; }
    }
}

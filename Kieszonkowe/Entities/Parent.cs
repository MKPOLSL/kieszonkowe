using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kieszonkowe.Entities
{
    public class Parent : User
    {
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public string BirthDate { get; set; }
        public ICollection<ChildRecord> Children { get; set; }
        public bool IsActive { get; set; }
        public bool IsBanned { get; set; }
    }
}

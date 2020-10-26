using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kieszonkowe.Entities
{
    public class Region
    {
        [Key]
        public Guid Id { get; set; }
        public string RegionName { get; set; }
    }
}

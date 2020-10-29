using System;
using System.ComponentModel.DataAnnotations;

namespace Kieszonkowe.Entities
{
    public class Region
    {
        [Key]
        public Guid Id { get; set; }
        public string RegionName { get; set; }
    }
}

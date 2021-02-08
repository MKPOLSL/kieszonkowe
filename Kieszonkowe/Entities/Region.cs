using System;
using System.ComponentModel.DataAnnotations;

namespace Kieszonkowe.Entities
{
    public class Region : IdentityEntity
    {
        public string RegionName { get; set; }
        public bool IsCity { get; set; }
    }
}

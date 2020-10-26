using System;
using System.ComponentModel.DataAnnotations;

namespace Kieszonkowe.Entities
{
    public class ChildRecord
    {
        [Key]
        public Guid Id { get; set; }
        public Education Education { get; set; }
        public Region Region { get; set; }
        public int? PlannedAmount { get; set; }
        public int? ActualAmount { get; set; }
    }
}

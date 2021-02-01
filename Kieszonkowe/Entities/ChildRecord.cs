using System;
using System.ComponentModel.DataAnnotations;

namespace Kieszonkowe.Entities
{
    public class ChildRecord
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Education Education { get; set; }
        public Region Region { get; set; }
        public int? PlannedAmount { get; set; }
        public int? ActualAmount { get; set; }
        public bool IsHidden { get; set; }
    }
}
    
using System;

namespace Kieszonkowe.Entities
{
    public class ChildRecord : IdentityEntity
    {
        public string Name { get; set; }
        public Education Education { get; set; }
        public Region Region { get; set; }
        public int? PlannedAmount { get; set; }
        public int? ActualAmount { get; set; }
        public Guid ParentId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
    
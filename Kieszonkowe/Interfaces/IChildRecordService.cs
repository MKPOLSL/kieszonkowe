using Kieszonkowe.DAL;
using Kieszonkowe.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kieszonkowe.Interfaces
{
    public interface IChildRecordService
    {
        Task<ChildRecord> CreateChildRecord(Guid id, ChildDto childRecord);
        List<ChildRecord> GetChildren(Guid ParentID);
        ChildRecord GetChild(Guid childID);
        Task<ChildRecord> HideChild(Guid childID);
        Task<ChildRecord> CompleteChildRecord(Guid childId, int actualAmount);
    }
}

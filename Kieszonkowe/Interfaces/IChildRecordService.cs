using Kieszonkowe.DAL;
using Kieszonkowe.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kieszonkowe.Interfaces
{
    public interface IChildRecordService
    {
        Task<ChildRecord> CreateChildRecord(ChildDto childRecord);
        List<ChildRecord> GetChildren(Guid ParentID);
        Task addChildToParent(Guid id, ChildRecord result);
    }
}

using Kieszonkowe.Entities;
using System.Threading.Tasks;

namespace Kieszonkowe.Interfaces
{
    public interface IChildRecordService
    {
        Task<ChildRecord> CreateChildRecord(ChildRecord childRecord);
    }
}

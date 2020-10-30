using Kieszonkowe.Entities;
using Kieszonkowe.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Kieszonkowe.Services
{
    public class ChildRecordService : IChildRecordService
    {
        private readonly PocketMoneyContext pocketMoneyContext;
        private readonly DbSet<ChildRecord> childSet;

        public ChildRecordService(PocketMoneyContext pocketMoneyContext)
        {
            this.pocketMoneyContext = pocketMoneyContext;
            childSet = pocketMoneyContext.Set<ChildRecord>();
        }

        public async Task<ChildRecord> CreateChildRecord(ChildRecord childRecord)
        {
            childSet.Add(childRecord);
            await pocketMoneyContext.SaveChangesAsync();
            return childRecord;
        }
    }
}

using Kieszonkowe.Entities;
using Kieszonkowe.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kieszonkowe.Services
{
    public class ChildRecordService : IChildRecordService
    {
        private readonly PocketMoneyContext pocketMoneyContext;
        private readonly DbSet<ChildRecord> childSet;
        private readonly DbSet<Parent> parentSet;

        public ChildRecordService(PocketMoneyContext pocketMoneyContext)
        {
            this.pocketMoneyContext = pocketMoneyContext;
            childSet = pocketMoneyContext.Set<ChildRecord>();
            parentSet = pocketMoneyContext.Set<Parent>();
        }

        public async Task<ChildRecord> CreateChildRecord(ChildRecord childRecord)
        {
            childSet.Add(childRecord);
            await pocketMoneyContext.SaveChangesAsync();
            return childRecord;
        }

        public List<ChildRecord> GetChildren(Guid parentID)
        {
            pocketMoneyContext.SaveChanges();
            return parentSet.Find(parentID).Children.ToList();
        }
    }
}

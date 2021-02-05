using Kieszonkowe.DAL;
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

        public async Task<ChildRecord> CreateChildRecord(Guid id, ChildDto childRecord)
        {
            var region = pocketMoneyContext.Regions
                .Where(x => x.RegionName == childRecord.Region)
                .FirstOrDefault();
            var education = pocketMoneyContext.EducationDegrees
                .Where(x => x.EducationDegree == childRecord.Education)
                .FirstOrDefault();
            ChildRecord child = new ChildRecord()
            {
                Region = region,
                Education = education,
                Name = childRecord.Name,
                PlannedAmount = childRecord.PlannedAmount,  
                ParentId = id
            };
            var createdChild = await childSet.AddAsync(child);
            await pocketMoneyContext.SaveChangesAsync();
            return child;
        }

        public ChildRecord GetChild(Guid childID)
        {
            return childSet
                .Include(p => p.Region)
                .Include(p => p.Education)
                .Where(c => c.Id == childID)
                .FirstOrDefault();
        }

        public List<ChildRecord> GetChildren(Guid parentID)
        {
            var parent = parentSet
                .Include(p => p.Children)
                .ThenInclude(p => p.Region)
                .Include(p => p.Children)
                .ThenInclude(p => p.Education)
                .Where(p => p.Id == parentID)
                .FirstOrDefault();
            if (parent == null)
                return null;
            return parent.Children.Where(c => c.IsHidden == false).ToList();
        }

        public async Task<ChildRecord> HideChild(Guid childID)
        {
            var child = childSet
                .Include(p => p.Region)
                .Include(p => p.Education)
                .Where(c => c.Id == childID)
                .FirstOrDefault();
            child.IsHidden = true;
            await pocketMoneyContext.SaveChangesAsync();
            return child;
        }

        public async Task<ChildRecord> CompleteChildRecord(Guid childId, int actualAmount)
        {
            var child = childSet
                .Where(c => c.Id == childId)
                .FirstOrDefault();
            child.ActualAmount = actualAmount;
            await pocketMoneyContext.SaveChangesAsync();
            return child;
        }

        public async Task DeleteChildRecord(Guid childId)
        {
            var child = childSet
                .Where(c => c.Id == childId)
                .FirstOrDefault();
            childSet.Remove(child);
            await pocketMoneyContext.SaveChangesAsync();
        }
    }
}

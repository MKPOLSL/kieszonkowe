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

        public async Task<ChildRecord> CreateChildRecord(ChildDto childRecord)
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
                ActualAmount = childRecord.RealAmount
            };
            var createdChild = await childSet.AddAsync(child);
            return createdChild.Entity;
        }

        public async Task addChildToParent(Guid id, ChildRecord result)
        {
            var parent = parentSet
                .Include(p => p.Children)
                .ThenInclude(p => p.Region)
                .Include(p => p.Children)
                .ThenInclude(p => p.Education)
                .Where(p => p.Id == id)
                .FirstOrDefault();
            parent.Children.Add(result);
            await pocketMoneyContext.SaveChangesAsync();
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
            return parent.Children.ToList();
        }
    }
}

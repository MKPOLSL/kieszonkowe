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
    public class StatisticsService : IStatisticsService
    {
        private readonly PocketMoneyContext pocketMoneyContext;
        private readonly DbSet<ChildRecord> childSet;
        private readonly DbSet<Education> educationSet;
        private readonly DbSet<Region> regionSet;

        public StatisticsService(PocketMoneyContext pocketMoneyContext)
        {
            this.pocketMoneyContext = pocketMoneyContext;
            childSet = pocketMoneyContext.Set<ChildRecord>();
            educationSet = pocketMoneyContext.Set<Education>();
            regionSet = pocketMoneyContext.Set<Region>();
        }

        public double? MeanAmount(List<int?> list)
        {
            return list.Sum() / list.Count();
        }

        public double? MedianAmount(List<int?> list)    
        {
            var count = list.Count();
            var orderedList = list.OrderBy(l => l.Value);
            return (orderedList.ElementAt(count / 2).Value + orderedList.ElementAt((count - 1) / 2).Value) / 2;
        }

        public int? ModeAmount(List<int?> list)
        {
            int? modeValue = list
                .GroupBy(x => x)
                .OrderByDescending(x => x.Count()).ThenBy(x => x.Key)
                .Select(x => x.Key)
                .FirstOrDefault();
            return modeValue;
        }

        public double? StandardDeviationAmount(List<int?> list)
        {
            var mean = MeanAmount(list);
            return Math.Sqrt(list.Average(v => Math.Pow((double)(v - mean), 2)));
        }
        public List<int?> GetActualAmountListForRegionAndEducation(Guid educationId, Guid regionId)
        {
            return childSet
                .Include(e => e.Region)
                .Include(e => e.Education)
                .ToList()
                .Where(e => e.Education.Id.Equals(educationId) && e.Region.Id.Equals(regionId))
                .Select(s => s.ActualAmount)
                .ToList();
        }

        public List<int?> GetPlannedAmountListForRegionAndEducation(Guid educationId, Guid regionId)
        {
            return childSet
                .Include(e => e.Region)
                .Include(e => e.Education)
                .ToList()
                .Where(e => e.Education.Id.Equals(educationId) && e.Region.Id.Equals(regionId))
                .Select(s => s.PlannedAmount)
                .ToList();
        }

        public List<List<int?>> GetPlannedAmountListForEducation(Guid educationId)
        {
            return childSet
                .Include(e => e.Region)
                .Include(e => e.Education)
                .ToList()
                .Where(e => e.Education.Id.Equals(educationId))
                .GroupBy(e => e.Region)
                .Select(s => s.Select(s => s.PlannedAmount).ToList())
                .ToList();

        }

        public List<List<int?>> GetActualAmountListForEducation(Guid educationId)
        {
            return childSet
                .Include(e => e.Region)
                .Include(e => e.Education)
                .ToList()
                .Where(e => e.Education.Id.Equals(educationId))
                .GroupBy(e => e.Region)
                .Select(s => s.Select(s => s.ActualAmount).ToList())
                .ToList();

        }
        public StatisticsDto calculateStatisticsForPlannedAmount(Guid educationId, Guid regionId)
        {
            var list = GetPlannedAmountListForRegionAndEducation(educationId, regionId);
            if (list == null || !list.Any())
                return null;
            StatisticsDto statistics = new StatisticsDto()
            {
                MeanAmount = MeanAmount(list),
                MedianAmount = MedianAmount(list),
                ModeAmount = ModeAmount(list),
                StandardDeviationAmount = StandardDeviationAmount(list)
            };
            return statistics;
        }

        public StatisticsDto calculateStatisticsForActualAmount(Guid educationId, Guid regionId)
        {
            var list = GetActualAmountListForRegionAndEducation(educationId, regionId);
            if (list == null || !list.Any())
                return null;
            StatisticsDto statistics = new StatisticsDto()
            {
                MeanAmount = MeanAmount(list),
                MedianAmount = MedianAmount(list),
                ModeAmount = ModeAmount(list),
                StandardDeviationAmount = StandardDeviationAmount(list)
            };
            return statistics;
        }

        public async Task<List<Education>> GetEducations()
        {
            return await educationSet.ToListAsync();
        }

        public async Task<List<Region>> GetRegions()
        {
            return await regionSet.ToListAsync();
        }
    }
}

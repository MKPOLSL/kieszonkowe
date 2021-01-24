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
            return orderedList.ElementAt(count / 2).Value + orderedList.ElementAt((count - 1) / 2).Value / 2;
        }

        public int? ModeAmount(List<int?> list)
        {
            int? modeValue = list
                .GroupBy(x => x)
                .OrderByDescending(x => x.Count()).ThenBy(x => x.Key)
                .Select(x => (int?)x.Key)
                .FirstOrDefault();
            return modeValue;
        }

        public double? StandardDeviationAmount(List<int?> list)
        {
            var mean = MeanAmount(list);
            return Math.Sqrt(list.Average(v => Math.Pow((double)(v - mean), 2)));
        }
        public async Task<List<int?>> GetActualAmountListForRegionAndEducation(Guid educationId, Guid regionId)
        {
            return await childSet
                .Where(x => x.Region.Id == regionId && x.Education.Id == educationId)
                .Include(e => e.Region)
                .Select(s => s.ActualAmount)
                .ToListAsync();
        }
        public async Task<List<int?>> GetPlannedAmountListForRegionAndEducation(Guid educationId, Guid regionId)
        {
            return await childSet
                .Where(x => x.Region.Id == regionId && x.Education.Id == educationId)
                .Include(e => e.Region)
                .Select(s => s.PlannedAmount)
                .ToListAsync(); 
        }

        public StatisticsDto calculateStatistics(List<int?> list)
        {
            StatisticsDto statistics = new StatisticsDto();
            statistics.meanAmount = MeanAmount(list);
            statistics.medianAmount = MedianAmount(list);
            statistics.modeAmount = ModeAmount(list);
            statistics.standardDeviationAmount = StandardDeviationAmount(list);
            return statistics;
        }

        public async Task<StatisticsDto> calculateStatisticsForPlannedAmount(Guid educationId, Guid regionId)
        {
            var list = await GetActualAmountListForRegionAndEducation(educationId, regionId);
            StatisticsDto statistics = new StatisticsDto()
            {
                meanAmount = MeanAmount(list),
                medianAmount = MedianAmount(list),
                modeAmount = ModeAmount(list),
                standardDeviationAmount = StandardDeviationAmount(list)
            };
            return statistics;
        }

        public async Task<StatisticsDto> calculateStatisticsForActualAmount(Guid educationId, Guid regionId)
        {
            var list = await GetActualAmountListForRegionAndEducation(educationId, regionId);
            StatisticsDto statistics = new StatisticsDto()
            {
                meanAmount = MeanAmount(list),
                medianAmount = MedianAmount(list),
                modeAmount = ModeAmount(list),
                standardDeviationAmount = StandardDeviationAmount(list)
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

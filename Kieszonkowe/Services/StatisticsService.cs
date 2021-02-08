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
        private readonly DbSet<Parent> parentSet;
        private readonly DbSet<Education> educationSet;
        private readonly DbSet<Region> regionSet;
        private readonly int DaysIn4Years = 1460;

        public StatisticsService(PocketMoneyContext pocketMoneyContext)
        {
            this.pocketMoneyContext = pocketMoneyContext;
            childSet = pocketMoneyContext.Set<ChildRecord>();
            educationSet = pocketMoneyContext.Set<Education>();
            regionSet = pocketMoneyContext.Set<Region>();
            parentSet = pocketMoneyContext.Set<Parent>();
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
                .Where(e => e.Education.Id.Equals(educationId) && e.Region.Id.Equals(regionId) 
                            && (e.DateAdded - DateTime.Now).TotalDays < DaysIn4Years)
                .Select(s => s.ActualAmount).Where(e => e != null)
                .ToList();
        }

        public List<int?> GetPlannedAmountListForRegionAndEducation(Guid educationId, Guid regionId)
        {
            return childSet
                .Include(e => e.Region)
                .Include(e => e.Education)
                .ToList()
                .Where(e => e.Education.Id.Equals(educationId) && e.Region.Id.Equals(regionId) 
                            && (e.DateAdded - DateTime.Now).TotalDays < DaysIn4Years)
                .Select(s => s.PlannedAmount).Where(e => e != null)
                .ToList();
        }

        public Dictionary<Region, List<int?>> GetPlannedAmountListForEducation(Guid educationId, bool isCity)
        {
            return childSet
               .Include(e => e.Region)
               .Include(e => e.Education)
               .ToList()
               .Where(e => e.Education.Id.Equals(educationId) && e.Region.IsCity == isCity 
                           && (e.DateAdded-DateTime.Now).TotalDays < DaysIn4Years)
               .GroupBy(e => e.Region)
               .ToDictionary(g => g.Key, g => g.Select(e => e.PlannedAmount).Where(e => e != null).ToList());
        }

        public Dictionary<Region, List<int?>> GetActualAmountListForEducation(Guid educationId, bool isCity)
        {
            return childSet
               .Include(e => e.Region)
               .Include(e => e.Education)
               .ToList()
               .Where(e => e.Education.Id.Equals(educationId) && e.Region.IsCity == isCity 
                           && (e.DateAdded - DateTime.Now).TotalDays < DaysIn4Years)
               .GroupBy(e => e.Region)
               .ToDictionary(g => g.Key, g => g.Select(e => e.ActualAmount).Where(e => e != null).ToList());

        }
        public StatisticsDto calculateStatisticsForPlannedAmount(Guid educationId, Guid regionId)
        {
            var list = GetPlannedAmountListForRegionAndEducation(educationId, regionId);
            if (list == null || !list.Any())
                return null;
            StatisticsDto statistics = new StatisticsDto()
            {
                RegionName = pocketMoneyContext.Regions.Where(r => r.Id == regionId).FirstOrDefault().RegionName,
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
                RegionName = pocketMoneyContext.Regions.Where(r => r.Id == regionId).FirstOrDefault().RegionName,
                MeanAmount = MeanAmount(list),
                MedianAmount = MedianAmount(list),
                ModeAmount = ModeAmount(list),
                StandardDeviationAmount = StandardDeviationAmount(list)
            };
            return statistics;
        }


        public List<StatisticsDto> calculateStatisticsForPlannedAmount(Guid educationId, bool isCity)
        {
            var list = GetPlannedAmountListForEducation(educationId, isCity);
            if (list == null || !list.Any())
                return null;
            List<StatisticsDto> statistics = new List<StatisticsDto>();
            foreach (var element in list)
            {
                statistics.Add(new StatisticsDto()
                {
                    RegionName = element.Key.RegionName,
                    MeanAmount = MeanAmount(element.Value),
                    MedianAmount = MedianAmount(element.Value),
                    ModeAmount = ModeAmount(element.Value),
                    StandardDeviationAmount = StandardDeviationAmount(element.Value)
                });
            }
            return statistics;
        }

        public List<StatisticsDto> calculateStatisticsForActualAmount(Guid educationId, bool isCity)
        {
            var list = GetActualAmountListForEducation(educationId, isCity);
            if (list == null || !list.Any())
                return null;
            List<StatisticsDto> statistics = new List<StatisticsDto>();
            foreach (var element in list)
            {
                statistics.Add(new StatisticsDto()
                {
                    RegionName = element.Key.RegionName,
                    MeanAmount = MeanAmount(element.Value),
                    MedianAmount = MedianAmount(element.Value),
                    ModeAmount = ModeAmount(element.Value),
                    StandardDeviationAmount = StandardDeviationAmount(element.Value)
                });
            }
            return statistics;
        }

        public async Task<List<Education>> GetEducations(Guid parentId)
        {
            return await childSet
                .Include(c => c.Education)
                .Where(c => c.ParentId == parentId)
                .Select(c => c.Education).ToListAsync();
        }
        public async Task<List<Education>> GetEducationsActual(Guid parentId)
        {
            return await childSet
                .Include(c => c.Education)
                .Where(c => c.ParentId == parentId && c.ActualAmount != null)
                .Select(c => c.Education).ToListAsync();
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

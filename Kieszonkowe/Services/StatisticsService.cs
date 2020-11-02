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
        public int Pies { get; set; }

        public StatisticsService(PocketMoneyContext pocketMoneyContext)
        {
            this.pocketMoneyContext = pocketMoneyContext;
            childSet = pocketMoneyContext.Set<ChildRecord>();
        }

        public double? AllStatistics()
        {
            throw new NotImplementedException();
        }

        public async Task<double?> MeanAmount(Guid educationId, Guid regionId)
        {
            var list = await GetActualAmountListForRegionAndEducation(educationId, regionId);
            return list.Sum() / list.Count();
        }

        public double? MedianAmount(Guid educationId, Guid regionId)
        {
            throw new NotImplementedException();
        }

        public async Task<int?> ModeAmount(Guid educationId, Guid regionId)
        {
            var list = await GetActualAmountListForRegionAndEducation(educationId, regionId);
            int? modeValue = list
                .GroupBy(x => x)
                .OrderByDescending(x => x.Count()).ThenBy(x => x.Key)
                .Select(x => (int?)x.Key)
                .FirstOrDefault();
            return modeValue;
        }

        public double? StandartDeviationAmount(Guid educationId, Guid regionId)
        {
            throw new NotImplementedException();
        }

        private async Task<List<int?>> GetActualAmountListForRegionAndEducation(Guid educationId, Guid regionId)
        {
            return await childSet
                .Where(x => x.Region.Id == regionId && x.Education.Id == educationId)
                .Include(e => e.Region)
                .Select(s => s.ActualAmount)
                .ToListAsync();
        }
        private async Task<List<int?>> GetPlannedAmountListForRegionAndEducation(Guid educationId, Guid regionId)
        {
            return await childSet
                .Where(x => x.Region.Id == regionId && x.Education.Id == educationId)
                .Include(e => e.Region)
                .Select(s => s.PlannedAmount)
                .ToListAsync(); ;
        }

        double? IStatisticsService.MeanAmount(Guid educationId, Guid regionId)
        {
            throw new NotImplementedException();
        }
    }
}

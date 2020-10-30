using Kieszonkowe.Entities;
using Kieszonkowe.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Kieszonkowe.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly PocketMoneyContext pocketMoneyContext;
        private readonly DbSet<ChildRecord> childSet;

        public StatisticsService(PocketMoneyContext pocketMoneyContext)
        {
            this.pocketMoneyContext = pocketMoneyContext;
            childSet = pocketMoneyContext.Set<ChildRecord>();
        }

        public double AllStatistics()
        {
            throw new NotImplementedException();
        }

        public double? MeanAmount(Guid educationId, Guid regionId)
        {
            IQueryable<int?> set = pocketMoneyContext
                .Set<ChildRecord>()
                .Where(x => x.Region.Id == regionId && x.Education.Id == educationId)
                .Include(e => e.Region)
                .Select(s => s.ActualAmount);
            var list = set.ToArray();
            double? average = list.Average();
            return average;
        }

        public double MedianAmount(Guid educationId, Guid regionId)
        {
            throw new NotImplementedException();
        }

        public double ModeAmount(Guid educationId, Guid regionId)
        {
            throw new NotImplementedException();
        }

        public double StandartDeviationAmount(Guid educationId, Guid regionId)
        {
            throw new NotImplementedException();
        }
    }
}

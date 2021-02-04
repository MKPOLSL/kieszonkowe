using Kieszonkowe.DAL;
using Kieszonkowe.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kieszonkowe.Interfaces
{
    public interface IStatisticsService
    {
        StatisticsDto calculateStatisticsForPlannedAmount(Guid educationId, Guid regionId);
        StatisticsDto calculateStatisticsForActualAmount(Guid educationId, Guid regionId);
        Task<List<Education>> GetEducations();
        Task<List<Region>> GetRegions();
    }
}

using Kieszonkowe.DAL;
using Kieszonkowe.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kieszonkowe.Interfaces
{
    public interface IStatisticsService
    {
        StatisticsDto CalculateStatisticsForPlannedAmount(Guid educationId, Guid regionId);
        List<StatisticsDto> CalculateStatisticsForPlannedAmount(Guid educationId, bool isCity);
        StatisticsDto CalculateStatisticsForActualAmount(Guid educationId, Guid regionId);
        List<StatisticsDto> CalculateStatisticsForActualAmount(Guid educationId, bool isCity);

        Task<List<Education>> GetEducations();
        Task<List<Education>> GetEducations(Guid parentId);
        Task<List<Education>> GetEducationsActual(Guid parentId);
        Task<List<Region>> GetRegions();
        Task<StatisticsDto> RandomStatistics();
    }
}

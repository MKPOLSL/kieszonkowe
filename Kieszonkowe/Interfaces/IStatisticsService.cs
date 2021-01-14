using Kieszonkowe.DAL;
using Kieszonkowe.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kieszonkowe.Interfaces
{
    public interface IStatisticsService
    {
        StatisticsDto calculateStatistics(List<int?> list);
        Task<List<int?>> GetActualAmountListForRegionAndEducation(Guid educationId, Guid regionId);
        Task<List<int?>> GetPlannedAmountListForRegionAndEducation(Guid educationId, Guid regionId);
    }
}

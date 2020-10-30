using Kieszonkowe.Entities;
using System;
using System.Threading.Tasks;

namespace Kieszonkowe.Interfaces
{
    public interface IStatisticsService
    {
        double? MeanAmount(Guid educationId, Guid regionId);
        double? MedianAmount(Guid educationId, Guid regionId);
        double? StandartDeviationAmount(Guid educationId, Guid regionId);
        Task<int?> ModeAmount(Guid educationId, Guid regionId);
        double? AllStatistics();
    }
}

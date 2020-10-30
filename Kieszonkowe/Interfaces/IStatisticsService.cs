using Kieszonkowe.Entities;
using System;

namespace Kieszonkowe.Interfaces
{
    public interface IStatisticsService
    {
        double? MeanAmount(Guid educationId, Guid regionId);
        double? MedianAmount(Guid educationId, Guid regionId);
        double? StandartDeviationAmount(Guid educationId, Guid regionId);
        double? ModeAmount(Guid educationId, Guid regionId);
        double? AllStatistics();
    }
}

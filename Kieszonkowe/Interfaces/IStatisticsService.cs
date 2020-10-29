using Kieszonkowe.Entities;

namespace Kieszonkowe.Interfaces
{
    public interface IStatisticsService
    {
        double MeanAmount(Education education, Region region);
        double MedianAmount(Education education, Region region);
        double StandartDeviationAmount(Education education, Region region);
        double ModeAmount(Education education, Region region);
        double AllStatistics();
    }
}

using Kieszonkowe.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kieszonkowe.Interfaces
{
    public interface IStatisticsService
    {
        double MeanAmount(Education education, Region region);
        double MedianAmount(Education education, Region region);
        double StandartDeviationAmount(Education education, Region region);
        double ModeAmount(Education education, Region region);
    }
}

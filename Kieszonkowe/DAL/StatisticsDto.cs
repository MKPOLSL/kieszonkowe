namespace Kieszonkowe.DAL
{
    public class StatisticsDto
    { 
        public string RegionName { get; set; }
        public double? MeanAmount { get; set; }
        public double? StandardDeviationAmount { get; set; }
        public double? ModeAmount { get; set; }
        public double? MedianAmount { get; set; }
    }
}

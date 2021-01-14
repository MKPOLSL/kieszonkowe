using Kieszonkowe.DAL;
using Kieszonkowe.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kieszonkowe.Controllers
{
    [ApiController]
    [Route("pocketmoney")]
    public class PocketMoneyController : ControllerBase
    {
        private readonly ILogger<PocketMoneyController> _logger;
        private readonly IStatisticsService statisticsService;

        public PocketMoneyController(ILogger<PocketMoneyController> logger, IStatisticsService statisticsService)
        {
            _logger = logger;
            this.statisticsService = statisticsService;
        }

        [HttpGet]
        [EnableCors("AllowAll")]
        public IEnumerable<string> Get()
        {
            return new List<string>
            {
                new string("Kieszonkowe"),
                new string("Statystyki"),
                new string("I Inne Dane"),
            }.ToArray();
        }

        [HttpPost]
        [Route("statistics")]
        public async Task<IActionResult> CalculateStatisticsForPlannedAmount(Guid educationId, Guid regionId)
        {
            var list = await statisticsService.GetPlannedAmountListForRegionAndEducation(educationId, regionId);
            var statistics = statisticsService.calculateStatistics(list);
            return Ok(statistics);
        }

        [HttpPost]
        [Route("statisticsActual")]
        public async Task<IActionResult> CalculateStatisticsForActualAmount(Guid educationId, Guid regionId)
        {
            var list = await statisticsService.GetActualAmountListForRegionAndEducation(educationId, regionId);
            var statistics = statisticsService.calculateStatistics(list);
            return Ok(statistics);
        }
    }
}
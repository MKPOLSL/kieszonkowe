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
        public IActionResult CalculateStatisticsForPlannedAmount([FromBody] RegionAndEducationDto ids)
        {
            var statistics = statisticsService.calculateStatisticsForPlannedAmount(ids.EducationID, ids.RegionID);
            return Ok(statistics);
        }

        [HttpPost]
        [Route("statisticsActual")]
        public async Task<IActionResult> CalculateStatisticsForActualAmount(Guid educationId, Guid regionId)
        {
            var statistics = await statisticsService.calculateStatisticsForActualAmount(educationId, regionId);
            return Ok(statistics);
        }

        [HttpGet]
        [Route("educations")]
        public async Task<IActionResult> GetEducationDegrees()
        {
            var educations = await statisticsService.GetEducations();
            return Ok(educations);
        }

        [HttpGet]
        [Route("regions")]
        public async Task<IActionResult> GetRegions()
        {
            var regions = await statisticsService.GetRegions();
            return Ok(regions);
        }
    }
}
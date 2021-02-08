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
    [Route("statistics")]
    public class StatisticsController : ControllerBase
    {
        private readonly ILogger<StatisticsController> _logger;
        private readonly IStatisticsService statisticsService;

        public StatisticsController(ILogger<StatisticsController> logger, IStatisticsService statisticsService)
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
            if (statistics != null)
                return Ok(statistics);
            return BadRequest();
        }

        [HttpGet]  
        [Route("statisticsEducation")]
        public IActionResult CalculateStatisticsForPlannedAmount([FromQuery] StatisticsListRequestDto request)
        {
            var statistics = statisticsService.calculateStatisticsForPlannedAmount(request.educationId, request.isCity);
            if (statistics != null)
                return Ok(statistics);
            return BadRequest();
        }

        [HttpPost]
        [Route("statisticsActual")]
        public IActionResult CalculateStatisticsForActualAmount([FromBody] RegionAndEducationDto ids)
        {
            var statistics = statisticsService.calculateStatisticsForActualAmount(ids.EducationID, ids.RegionID);
            if(statistics != null) 
                return Ok(statistics);
            return BadRequest();
        }

        [HttpGet]
        [Route("statisticsEducationActual")]
        public IActionResult CalculateStatisticsForActualAmount([FromQuery] StatisticsListRequestDto request)
        {
            var statistics = statisticsService.calculateStatisticsForActualAmount(request.educationId, request.isCity);
            if (statistics != null)
                return Ok(statistics);
            return BadRequest();
        }

        [HttpGet]
        [Route("educations")]
        public async Task<IActionResult> GetEducationDegrees()
        {
            var educations = await statisticsService.GetEducations();
            return Ok(educations);
        }

        [HttpGet]
        [Route("educationsPlanned")]
        public async Task<IActionResult> GetEducationDegreesPlanned([FromQuery] Guid parentId)
        {
            var educations = await statisticsService.GetEducations(parentId);
            return Ok(educations);
        }

        [HttpGet]
        [Route("educationsActual")]
        public async Task<IActionResult> GetEducationDegreesActual([FromQuery] Guid parentId)
        {
            var educations = await statisticsService.GetEducationsActual(parentId);
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
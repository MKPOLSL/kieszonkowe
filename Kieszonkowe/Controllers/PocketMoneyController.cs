using Kieszonkowe.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

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
    }
}
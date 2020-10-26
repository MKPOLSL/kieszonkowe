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

        public PocketMoneyController(ILogger<PocketMoneyController> logger)
        {
            _logger = logger;
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
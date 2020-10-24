﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kieszonkowe.Controllers
{
    [ApiController]
    [Route("kieszonkowe")]
    public class PocketMoneyController : ControllerBase
    {
        private readonly ILogger<PocketMoneyController> _logger;

        public PocketMoneyController(ILogger<PocketMoneyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new List<string>
            {
                new string("Kieszonkowe"),
                new string("MK"),
                new string("ŁK")
            }.ToArray();
        }
    }
}
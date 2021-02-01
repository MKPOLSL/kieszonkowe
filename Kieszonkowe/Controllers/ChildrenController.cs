using Kieszonkowe.DAL;
using Kieszonkowe.Entities;
using Kieszonkowe.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kieszonkowe.Controllers
{
    [ApiController]
    [Route("children")]
    public class ChildrenController : Controller
    {
        private readonly IChildRecordService childRecordService;
        
        public ChildrenController(IChildRecordService childRecordService)
        {
            this.childRecordService = childRecordService;
        }

        //WIP
        [HttpPost]
        [Route("addChild")]
        public async Task<IActionResult> CreateChildRecord([FromQuery] Guid id, [FromBody] ChildDto child)
        {
            //var result = await childRecordService.CreateChildRecord(child);
            //return Ok(result);
            return Ok();
        }

        [HttpGet]
        [Route("childs")]
        public IActionResult GetChildren([FromQuery] Guid id)
        {
            
            var result = childRecordService.GetChildren(id);
            return Ok(result);
        }

    }
}

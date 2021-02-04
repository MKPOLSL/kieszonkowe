using Kieszonkowe.DAL;
using Kieszonkowe.Entities;
using Kieszonkowe.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
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

        [HttpPost]
        [Route("addChild")]
        public async Task<IActionResult> CreateChildRecord([FromBody] ParentAndChildDto parentAndChild)
        {
            var result = await childRecordService.CreateChildRecord(parentAndChild.ParentId, parentAndChild.Child);
            if (result != null)
                return Ok();
            else return BadRequest();
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

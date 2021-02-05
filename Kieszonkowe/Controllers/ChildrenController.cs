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
        [Route("children")]
        public IActionResult GetChildren([FromQuery] Guid parentId)
        {
            var result = childRecordService.GetChildren(parentId);
            return Ok(result);
        }

        [HttpGet]
        [Route("child")]
        public IActionResult GetChild([FromQuery] Guid childId)
        {
            var result = childRecordService.GetChild(childId);
            return Ok(result);
        }

        [HttpGet]
        [Route("hide")]
        public async Task<IActionResult> HideChild([FromQuery] Guid childId)
        {
            var result = await childRecordService.HideChild(childId);
            if (result != null)
                return Ok();
            return BadRequest();
        }

        [HttpPost]
        [Route("complete")]
        public async Task<IActionResult> CompleteChildRecord([FromBody] CompleteChildRecordDto request)
        {
            var result = await childRecordService.CompleteChildRecord(request.ChildId, request.ActualAmount);
            if (result != null)
                return Ok();
            return BadRequest();
        }
    }
}

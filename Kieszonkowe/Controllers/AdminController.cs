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
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        [HttpPost]
        [Route("authenticate")]
        public IActionResult AuthenticateAdmin([FromBody] UserLoginDto admin)
        {
            var authenticatedAdmin = adminService.AuthenticateAdmin(admin);
            if (authenticatedAdmin == null)
                return Forbid();
            return Ok(authenticatedAdmin);
        }

        [HttpGet]
        [Route("panel/regions")]
        public async Task<IActionResult> GetRegions()
        {
            var regions = await adminService.GetRegions();
            if (regions == null)
                return BadRequest();
            return Ok(regions);
        }

        [HttpGet]
        [Route("panel/children")]
        public async Task<IActionResult> GetChildRecords()
        {
            var regions = await adminService.GetChildRecords();
            if (regions == null)
                return BadRequest();
            return Ok(regions);
        }
        [HttpGet]
        [Route("panel/educations")]
        public async Task<IActionResult> GetEducations()
        {
            var regions = await adminService.GetEducations();
            if (regions == null)
                return BadRequest();
            return Ok(regions);
        }
        [HttpGet]
        [Route("panel/administrators")]
        public async Task<IActionResult> GetAdministrators()
        {
            var regions = await adminService.GetAdministrators();
            if (regions == null)
                return BadRequest();
            return Ok(regions);
        }
        [HttpGet]
        [Route("panel/parents")]
        public async Task<IActionResult> GetParents()
        {
            var regions = await adminService.GetParents();
            if (regions == null)
                return BadRequest();
            return Ok(regions);
        }

        [HttpPost]
        [Route("panel/children/add")]
        public async Task<IActionResult> AddChildRecord([FromBody] ChildDto child)
        { 
            var childRecord = await adminService.AddChildRecord(child);
            if (childRecord == null)
                return BadRequest();
            return Ok(childRecord);
        }

        [HttpPost]
        [Route("panel/educations/add")]
        public async Task<IActionResult> AddEducation([FromBody] Education education)
        {
            var addedEducation = await adminService.AddEducation(education);
            if (addedEducation == null)
                return BadRequest();
            return Ok(addedEducation);
        }

        [HttpPost]
        [Route("panel/regions/add")]
        public async Task<IActionResult> AddRegion([FromBody] RegionDto region)
        {
            var addedRegion = await adminService.AddRegion(region);
            if (addedRegion == null)
                return BadRequest();
            return Ok(addedRegion);
        }

        [HttpPost]
        [Route("panel/regions/add")]
        public async Task<IActionResult> AddParent([FromBody] ParentDto parent)
        {
            var addedParent = await adminService.AddParent(parent);
            if (addedParent == null)
                return BadRequest();
            return Ok(addedParent);
        }

        [HttpPost]
        [Route("panel/regions/add")]
        public async Task<IActionResult> AddAdministrator([FromBody] Administrator admin)
        {
            var addedAdministrator = await adminService.AddAdministrator(admin);
            if (addedAdministrator == null)
                return BadRequest();
            return Ok(addedAdministrator);
        }
    }
}

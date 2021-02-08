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
        public async Task<IActionResult> AddRegion([FromBody] Region region)
        {
            var addedRegion = await adminService.AddRegion(region);
            if (addedRegion == null)
                return BadRequest();
            return Ok(addedRegion);
        }

        [HttpPost]
        [Route("panel/parents/add")]
        public async Task<IActionResult> AddParent([FromBody] Parent parent)
        {
            var addedParent = await adminService.AddParent(parent);
            if (addedParent == null)
                return BadRequest();
            return Ok(addedParent);
        }

        [HttpPost]
        [Route("panel/administrators/add")]
        public async Task<IActionResult> AddAdministrator([FromBody] Administrator admin)
        {
            var addedAdministrator = await adminService.AddAdministrator(admin);
            if (addedAdministrator == null)
                return BadRequest();
            return Ok(addedAdministrator);
        }

        [HttpPost]
        [Route("panel/children/delete")]
        public async Task<IActionResult> DeleteChildRecord([FromBody] Guid childId)
        {
            bool deletedChildRecord = await adminService.DeleteChildRecord(childId);
            if (deletedChildRecord == false)
                return BadRequest();
            return Ok(deletedChildRecord);
        }

        [HttpPost]
        [Route("panel/parents/delete")]
        public async Task<IActionResult> DeleteParent([FromBody] Guid parentId)
        {
            bool deletedParent = await adminService.DeleteParent(parentId);
            if (deletedParent == false)
                return BadRequest();
            return Ok(deletedParent);
        }

        [HttpPost]
        [Route("panel/administrators/delete")]
        public async Task<IActionResult> DeleteAdministrator([FromBody] Guid adminId)
        {
            bool deletedAdministrator = await adminService.DeleteAdministrator(adminId);
            if (deletedAdministrator == false)
                return BadRequest();
            return Ok(deletedAdministrator);
        }

        [HttpPost]
        [Route("panel/regions/delete")]
        public async Task<IActionResult> DeleteRegion([FromBody] Guid regionId)
        {
            bool deletedRegion = await adminService.DeleteRegion(regionId);
            if (deletedRegion == false)
                return BadRequest();
            return Ok(deletedRegion);
        }

        [HttpPost]
        [Route("panel/educations/delete")]
        public async Task<IActionResult> DeleteEducation([FromBody] Guid educationId)
        {
            bool deletedEducation = await adminService.DeleteEducation(educationId);
            if (deletedEducation == false)
                return BadRequest();
            return Ok(deletedEducation);
        }

        [HttpPost]
        [Route("panel/children/update")]
        public async Task<IActionResult> UpdateChildRecord([FromBody] ChildDto child)
        {
            var updatedChildRecord = await adminService.UpdateChildRecord(child);
            if (updatedChildRecord == null)
                return BadRequest();
            return Ok(updatedChildRecord);
        }

        [HttpPost]
        [Route("panel/parents/update")]
        public async Task<IActionResult> UpdateParent([FromBody] Parent parent)
        {
            var updatedParent = await adminService.UpdateParent(parent);
            if (updatedParent == null)
                return BadRequest();
            return Ok(updatedParent);
        }

        [HttpPost]
        [Route("panel/administrators/update")]
        public async Task<IActionResult> UpdateAdministrator([FromBody] Administrator admin)
        {
            var updatedAdministrator = await adminService.UpdateAdministrator(admin);
            if (updatedAdministrator == null)
                return BadRequest();
            return Ok(updatedAdministrator);
        }

        [HttpPost]
        [Route("panel/regions/update")]
        public async Task<IActionResult> UpdateRegion([FromBody] Region region)
        {
            var updatedRegion = await adminService.UpdateRegion(region);
            if (updatedRegion == null)
                return BadRequest();
            return Ok(updatedRegion);
        }

        [HttpPost]
        [Route("panel/educations/update")]
        public async Task<IActionResult> UpdateEducation([FromBody] Education education)
        {
            var updatedEducation = await adminService.UpdateEducation(education);
            if (updatedEducation == null)
                return BadRequest();
            return Ok(updatedEducation);
        }
    }
}

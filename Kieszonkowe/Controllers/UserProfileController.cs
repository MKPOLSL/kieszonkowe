using Kieszonkowe.Entities;
using Kieszonkowe.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kieszonkowe.Controllers
{
    [ApiController]
    [Route("profile")]
    public class UserProfileController : ControllerBase
    {
        private readonly IChildRecordService ChildRecordService;

        public UserProfileController(IChildRecordService childRecordService)
        {
            ChildRecordService = childRecordService;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            ChildRecord childRecord = new ChildRecord
            {
                PlannedAmount = 20,
                ActualAmount = 20000
            };
            var result = await ChildRecordService.CreateChildRecord(childRecord);
            return "pies";
        }

        [HttpPost]
        public async Task<IActionResult> CreateChildRecord(ChildRecord childRecord)
        {
            var result = await ChildRecordService.CreateChildRecord(childRecord);
            return Ok(result);
        }
    }
}

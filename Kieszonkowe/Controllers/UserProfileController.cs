using Kieszonkowe.DAL;
using Kieszonkowe.Entities;
using Kieszonkowe.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Kieszonkowe.Controllers
{
    [ApiController]
    [Route("profile")]//localhost:4800/profile/register
    public class UserProfileController : ControllerBase
    {
        private readonly IChildRecordService childRecordService;
        private readonly IUserService userService;

        public UserProfileController(IChildRecordService childRecordService, IUserService userService)
        {
            this.childRecordService = childRecordService;
            this.userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateParent(Parent parent)
        {
            var result = await userService.CreateUser(parent);
            if (result == null)
                return BadRequest();
            return Ok(result);
        }

        [HttpPost]
        [Route("authenticate")]
        public IActionResult AuthenticateUser([FromBody] UserLoginDto userLogin)
        {
            var authenticatedUser = userService.AuthenticateUser(userLogin);
            if (authenticatedUser == null)
                return Forbid();
            return Ok(authenticatedUser);
        }

        [HttpPost]
        [Route("updateUserData")]
        public async Task<IActionResult> UpdateParentData([FromBody] ParentChangeDataDto parentData)
        {
            var parent = await userService.UpdateParentData(parentData);
            if (parent == null)
                return Forbid();
            return Ok(parent);
        }

        [HttpPost]
        [Route("updateUserPassword")]
        public async Task<IActionResult> UpdateParentPassword([FromBody] ParentChangePasswordDto parentPassword)
        {
            var parent = await userService.UpdateParentPassword(parentPassword);
            if (parent == null)
                return Forbid();
            return Ok(parent);
        }
    }
}




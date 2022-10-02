using Gamelance.Models.Applications;
using Gamelance.Models.Users;
using Gamelance.Services.AppsService;
using Gamelance.Services.PagesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamelance.Controllers.Admins
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")]
    public class UserPagesAdminController : ControllerBase
    {
        private readonly IUserPagesService _userPages;
        private readonly IChangeUserNameAppsService _appsService;

        public UserPagesAdminController(IUserPagesService userPages, IChangeUserNameAppsService appsService)
        {
            _userPages = userPages;
            _appsService = appsService;
        }

        [HttpGet("AllApplications")]
        public async Task<List<ChangeUserNameApps>> GetAllChangeNameApplications()
        {
            return await _appsService.GetApplicationsAsync();
        }

        [HttpGet("Application")]
        public async Task<ChangeUserNameApps> GetChangeNameApplication(Guid id)
        {
            return await _appsService.GetApplicationAsync(id);
        }

        [HttpPost("ApproveApplication/{id}")]
        public async Task<IActionResult> Approve(Guid id)
        {
            var task = _appsService.ApproveApplicationAsync(id);

            if (task.IsCompletedSuccessfully)
            {
                return new OkObjectResult(await _appsService.GetApplicationsAsync() + $"Application {id} is applied");
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost("RejectApplication/{id}")]
        public async Task<IActionResult> Regect(Guid id)
        {
            var task = _appsService.RejectApplicationAsync(id);

            if (task.IsCompletedSuccessfully)
            {
                return new OkObjectResult(await _appsService.GetApplicationsAsync() + $"Application {id} is rejected");
            }
            else
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPut("BanUser/{id}")]
        public IActionResult BanUserPage(Guid id)
        {
            _userPages.BanUserPageAsync(id);

            return Ok($"User {id} was banned");
        }

        [HttpPut("UnBanUser/{id}")]
        public IActionResult UnBanUserPage(Guid id)
        {
            _userPages.UnBanUserPage(id);

            return Ok($"User {id} was banned");
        }

        [HttpDelete("RemoveDeletedPages")]
        public IActionResult RemoveDeletedPagesFromDatabase(List<UserPage> pages)
        {
            try
            {
                _userPages.RemoveDeletedPagesFromDatabase(pages);

                return Ok("Successfule deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

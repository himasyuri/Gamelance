using Gamelance.Models.Dto;
using Gamelance.Models.SpecialResponses;
using Gamelance.Models.Users;
using Gamelance.Services;
using Gamelance.Services.PagesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gamelance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPagesController : ControllerBase
    {
        private readonly IUserPagesService _userPages;
        private Guid UserId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public UserPagesController(IUserPagesService user)
        {
            _userPages = user;
        }

        // GET: api/<UserPagesController>
        [HttpGet("AllUserPages")]
        public async Task<ActionResult<List<UserPage>>> GetAllUserPages()
        {
            var pages = await _userPages.GetUserPagesAsync();

            return Ok(pages);
        }

        [HttpGet("AllNotDeletedUserPages")]
        public async Task<ActionResult<List<UserPage>>> GetAllNotDeletedUserPages()
        {
            var pages = await _userPages.GetUserPagesNotDeletedAsync();

            return Ok(pages);
        }

        // GET api/<UserPagesController>/5
        [HttpGet("UserPage/id")]
        public async Task<ActionResult<UserPageResponse>> GetUserPage(Guid id)
        {
            var page = await _userPages.GetUserPageAsync(id);

            if (page.OrganizationPage == null)
            {
                UserPageResponse response = new UserPageResponse()
                {
                    Id = page.PageId,
                    Name = page.Name,
                    Reputation = page.Reputation,
                    IsFindingStatus = page.IsFindingStatus,
                    IsOnline = page.IsOnline,
                    IsBanned = page.IsBanned,
                    IsDeleted = page.IsDeleted,
                    About = page.About,
                    LastOnline = page.LastOnline,
                    OrgPageId = page.OrgPageId,
                    OrganizationName = "",
                    UserPosts = page.UserPosts,
                    Reviews = page.Reviews,
                };

                return Ok(response);
            }
            else
            {
                UserPageResponse response = new UserPageResponse()
                {
                    Id = page.PageId,
                    Name = page.Name,
                    Reputation = page.Reputation,
                    IsFindingStatus = page.IsFindingStatus,
                    IsOnline = page.IsOnline,
                    IsBanned = page.IsBanned,
                    IsDeleted = page.IsDeleted,
                    About = page.About,
                    LastOnline = page.LastOnline,
                    OrgPageId = page.OrgPageId,
                    OrganizationName = page.OrganizationPage.OrganizationName,
                    UserPosts = page.UserPosts,
                    Reviews = page.Reviews,
                };

                return Ok(response);
            }
        }

        [HttpPost("AddStatus"), Authorize]
        public async Task<ActionResult<UserPage>> AddStatus([FromBody] string status)
        {
            var page = await _userPages.AddStatusAsync(status, UserId);

            if (page.OrganizationPage == null)
            {
                UserPageResponse response = new UserPageResponse()
                {
                    Id = page.PageId,
                    Name = page.Name,
                    Reputation = page.Reputation,
                    IsFindingStatus = page.IsFindingStatus,
                    IsOnline = page.IsOnline,
                    IsBanned = page.IsBanned,
                    IsDeleted = page.IsDeleted,
                    About = page.About,
                    LastOnline = page.LastOnline,
                    OrgPageId = page.OrgPageId,
                    OrganizationName = "",
                    UserPosts = page.UserPosts,
                    Reviews = page.Reviews,
                };

                return Ok(response);
            }
            else
            {
                UserPageResponse response = new UserPageResponse()
                {
                    Id = page.PageId,
                    Name = page.Name,
                    Reputation = page.Reputation,
                    IsFindingStatus = page.IsFindingStatus,
                    IsOnline = page.IsOnline,
                    IsBanned = page.IsBanned,
                    IsDeleted = page.IsDeleted,
                    About = page.About,
                    LastOnline = page.LastOnline,
                    OrgPageId = page.OrgPageId,
                    OrganizationName = page.OrganizationPage.OrganizationName,
                    UserPosts = page.UserPosts,
                    Reviews = page.Reviews,
                };

                return Ok(response);
            }
        }

        [HttpPost("AddAbout"), Authorize]
        public async Task<ActionResult<UserPageResponse>> AddAbout([FromBody] string about)
        {
            var page = await _userPages.AddAboutAsync(about, UserId);

            if (page.OrganizationPage == null)
            {
                UserPageResponse response = new UserPageResponse()
                {
                    Id = page.PageId,
                    Name = page.Name,
                    Reputation = page.Reputation,
                    IsFindingStatus = page.IsFindingStatus,
                    IsOnline = page.IsOnline,
                    IsBanned = page.IsBanned,
                    IsDeleted = page.IsDeleted,
                    About = page.About,
                    LastOnline = page.LastOnline,
                    OrgPageId = page.OrgPageId,
                    OrganizationName = "",
                    UserPosts = page.UserPosts,
                    Reviews = page.Reviews,
                };

                return Ok(response);
            }
            else
            {
                UserPageResponse response = new UserPageResponse()
                {
                    Id = page.PageId,
                    Name = page.Name,
                    Reputation = page.Reputation,
                    IsFindingStatus = page.IsFindingStatus,
                    IsOnline = page.IsOnline,
                    IsBanned = page.IsBanned,
                    IsDeleted = page.IsDeleted,
                    About = page.About,
                    LastOnline = page.LastOnline,
                    OrgPageId = page.OrgPageId,
                    OrganizationName = page.OrganizationPage.OrganizationName,
                    UserPosts = page.UserPosts,
                    Reviews = page.Reviews,
                };

                return Ok(response);
            }
        }

        [HttpPost("AddPhoto"), Authorize]
        public async Task<ActionResult<UserPageResponse>> AddPhoto([FromBody] IFormFile photo)
        {
            var page = await _userPages.AddPhotoAsync(photo, UserId);
            if (page.OrganizationPage == null)
            {
                UserPageResponse response = new UserPageResponse()
                {
                    Id = page.PageId,
                    Name = page.Name,
                    Reputation = page.Reputation,
                    IsFindingStatus = page.IsFindingStatus,
                    IsOnline = page.IsOnline,
                    IsBanned = page.IsBanned,
                    IsDeleted = page.IsDeleted,
                    About = page.About,
                    LastOnline = page.LastOnline,
                    OrgPageId = page.OrgPageId,
                    OrganizationName = "",
                    UserPosts = page.UserPosts,
                    Reviews = page.Reviews,
                };

                return Ok(response);
            }
            else
            {
                UserPageResponse response = new UserPageResponse()
                {
                    Id = page.PageId,
                    Name = page.Name,
                    Reputation = page.Reputation,
                    IsFindingStatus = page.IsFindingStatus,
                    IsOnline = page.IsOnline,
                    IsBanned = page.IsBanned,
                    IsDeleted = page.IsDeleted,
                    About = page.About,
                    LastOnline = page.LastOnline,
                    OrgPageId = page.OrgPageId,
                    OrganizationName = page.OrganizationPage.OrganizationName,
                    UserPosts = page.UserPosts,
                    Reviews = page.Reviews,
                };

                return Ok(response);
            }
        }

        // POST api/<UserPagesController>
        [HttpPost("NewPage"), Authorize]
        public async Task<ActionResult<UserPageResponse>> CreateUserPage([FromBody] UserPageRegistration userPage)
        {
            var page = await _userPages.CreateUserPageAsync(userPage, UserId);

            if (page.OrganizationPage == null)
            {
                UserPageResponse response = new UserPageResponse()
                {
                    Id = page.PageId,
                    Name = page.Name,
                    Reputation = page.Reputation,
                    IsFindingStatus = page.IsFindingStatus,
                    IsOnline = page.IsOnline,
                    IsBanned = page.IsBanned,
                    IsDeleted = page.IsDeleted,
                    About = page.About,
                    LastOnline = page.LastOnline,
                    OrgPageId = page.OrgPageId,
                    OrganizationName = "",
                    UserPosts = page.UserPosts,
                    Reviews = page.Reviews,
                };

                return Ok(response);
            }
            else
            {
                UserPageResponse response = new UserPageResponse()
                {
                    Id = page.PageId,
                    Name = page.Name,
                    Reputation = page.Reputation,
                    IsFindingStatus = page.IsFindingStatus,
                    IsOnline = page.IsOnline,
                    IsBanned = page.IsBanned,
                    IsDeleted = page.IsDeleted,
                    About = page.About,
                    LastOnline = page.LastOnline,
                    OrgPageId = page.OrgPageId,
                    OrganizationName = page.OrganizationPage.OrganizationName,
                    UserPosts = page.UserPosts,
                    Reviews = page.Reviews,
                };

                return Ok(response);
            }
        }

        // PUT api/<UserPagesController>/5
        [HttpPut("ChangeName/{id}"), Authorize]
        public async Task<ActionResult<UserPageResponse>> ChangeUserName([FromBody] string name)
        {
            var page = await _userPages.EditName(name, UserId);

            if (page.OrganizationPage == null)
            {
                UserPageResponse response = new UserPageResponse()
                {
                    Id = page.PageId,
                    Name = page.Name,
                    Reputation = page.Reputation,
                    IsFindingStatus = page.IsFindingStatus,
                    IsOnline = page.IsOnline,
                    IsBanned = page.IsBanned,
                    IsDeleted = page.IsDeleted,
                    About = page.About,
                    LastOnline = page.LastOnline,
                    OrgPageId = page.OrgPageId,
                    OrganizationName = "",
                    UserPosts = page.UserPosts,
                    Reviews = page.Reviews,
                };

                return Ok(response);
            }
            else
            {
                UserPageResponse response = new UserPageResponse()
                {
                    Id = page.PageId,
                    Name = page.Name,
                    Reputation = page.Reputation,
                    IsFindingStatus = page.IsFindingStatus,
                    IsOnline = page.IsOnline,
                    IsBanned = page.IsBanned,
                    IsDeleted = page.IsDeleted,
                    About = page.About,
                    LastOnline = page.LastOnline,
                    OrgPageId = page.OrgPageId,
                    OrganizationName = page.OrganizationPage.OrganizationName,
                    UserPosts = page.UserPosts,
                    Reviews = page.Reviews,
                };

                return Ok(response);
            }
        }

        // PUT api/<UserPagesController>/5
        [HttpPut("ChangeAbout/{id}"), Authorize]
        public async Task<ActionResult<UserPageResponse>> ChangeAbout([FromBody] string about)
        {
            var page = await _userPages.EditAbout(about, UserId);

            if (page.OrganizationPage == null)
            {
                UserPageResponse response = new UserPageResponse()
                {
                    Id = page.PageId,
                    Name = page.Name,
                    Reputation = page.Reputation,
                    IsFindingStatus = page.IsFindingStatus,
                    IsOnline = page.IsOnline,
                    IsBanned = page.IsBanned,
                    IsDeleted = page.IsDeleted,
                    About = page.About,
                    LastOnline = page.LastOnline,
                    OrgPageId = page.OrgPageId,
                    OrganizationName = "",
                    UserPosts = page.UserPosts,
                    Reviews = page.Reviews,
                };

                return Ok(response);
            }
            else
            {
                UserPageResponse response = new UserPageResponse()
                {
                    Id = page.PageId,
                    Name = page.Name,
                    Reputation = page.Reputation,
                    IsFindingStatus = page.IsFindingStatus,
                    IsOnline = page.IsOnline,
                    IsBanned = page.IsBanned,
                    IsDeleted = page.IsDeleted,
                    About = page.About,
                    LastOnline = page.LastOnline,
                    OrgPageId = page.OrgPageId,
                    OrganizationName = page.OrganizationPage.OrganizationName,
                    UserPosts = page.UserPosts,
                    Reviews = page.Reviews,
                };

                return Ok(response);
            }
        }

        // PUT api/<UserPagesController>/5
        [HttpPut("ChangeStatus/{id}"), Authorize]
        public async Task<ActionResult<UserPageResponse>> ChangeStatus([FromBody] string status)
        {
            var page = await _userPages.EditStatus(status, UserId);

            if (page.OrganizationPage == null)
            {
                UserPageResponse response = new UserPageResponse()
                {
                    Id = page.PageId,
                    Name = page.Name,
                    Reputation = page.Reputation,
                    IsFindingStatus = page.IsFindingStatus,
                    IsOnline = page.IsOnline,
                    IsBanned = page.IsBanned,
                    IsDeleted = page.IsDeleted,
                    About = page.About,
                    LastOnline = page.LastOnline,
                    OrgPageId = page.OrgPageId,
                    OrganizationName = "",
                    UserPosts = page.UserPosts,
                    Reviews = page.Reviews,
                };

                return Ok(response);
            }
            else
            {
                UserPageResponse response = new UserPageResponse()
                {
                    Id = page.PageId,
                    Name = page.Name,
                    Reputation = page.Reputation,
                    IsFindingStatus = page.IsFindingStatus,
                    IsOnline = page.IsOnline,
                    IsBanned = page.IsBanned,
                    IsDeleted = page.IsDeleted,
                    About = page.About,
                    LastOnline = page.LastOnline,
                    OrgPageId = page.OrgPageId,
                    OrganizationName = page.OrganizationPage.OrganizationName,
                    UserPosts = page.UserPosts,
                    Reviews = page.Reviews,
                };

                return Ok(response);
            }
        }

        // DELETE api/<UserPagesController>/5
        [HttpDelete("DeleteUserPage/{id}"), Authorize]
        public void DeleteUserPage(Guid userId)
        {
             _userPages.DeleteUserPageAsync(userId);
        }

        [HttpDelete("DeleteUserPhoto/{id}"), Authorize]
        public async Task<ActionResult<UserPage>> DeleteUserPhoto(Guid userId, Guid photoId)
        {
            var page = await _userPages.DeletePhotoAsync(photoId, userId);

            return page;
        }
    }
}

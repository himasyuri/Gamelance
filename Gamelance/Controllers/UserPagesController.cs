using Gamelance.Models.Dto;
using Gamelance.Models.SpecialResponses;
using Gamelance.Models.Users;
using Gamelance.Services;
using Gamelance.Services.PagesServices;
using Microsoft.AspNetCore.Mvc;

namespace Gamelance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPagesController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IUserPagesService _userPages;

        public UserPagesController(IImageService image, IUserPagesService user)
        {
            _imageService = image;
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
            //var image = _imageService.GetImage(page.Avatar.Path);

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
                    // ImageData= image
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
                    // ImageData= image
                };

                return Ok(response);
            }
        }

        [HttpPost("AddStatus")]
        public async Task<ActionResult<UserPage>> AddStatus([FromBody] string status, Guid userId)
        {
            var page = await _userPages.AddStatusAsync(status, userId);
            var image = _imageService.GetImage(page.Avatar.Path);

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
                    // ImageData= image
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
                    // ImageData= image
                };

                return Ok(response);
            }
        }

        [HttpPost("AddAbout")]
        public async Task<ActionResult<UserPageResponse>> AddAbout([FromBody] string about, Guid userId)
        {
            var page = await _userPages.AddAboutAsync(about, userId);
            var image = _imageService.GetImage(page.Avatar.Path);

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
                    // ImageData= image
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
                    // ImageData= image
                };

                return Ok(response);
            }
        }

        [HttpPost("AddPhoto")]
        public async Task<ActionResult<UserPageResponse>> AddPhoto([FromBody] IFormFile photo, Guid userId)
        {
            var page = await _userPages.AddPhotoAsync(photo, userId);
            var image = _imageService.GetImage(page.Avatar.Path);
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
                    // ImageData= image
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
                    // ImageData= image
                };

                return Ok(response);
            }
        }

        // POST api/<UserPagesController>
        [HttpPost("NewPage")]
        public async Task<ActionResult<UserPageResponse>> CreateUserPage([FromBody] UserPageRegistration userPage, Guid userId)
        {
            var page = await _userPages.CreateUserPageAsync(userPage, userId);
            //var image = _imageService.GetImage(page.Avatar.Path);

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
                   // ImageData= image
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
                   // ImageData= image
                };

                return Ok(response);
            }
        }

        // PUT api/<UserPagesController>/5
        [HttpPut("ChangeName/{id}")]
        public async Task<ActionResult<UserPageResponse>> ChangeUserName(Guid id, [FromBody] string name)
        {
            var page = await _userPages.EditName(name, id);

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
                    // ImageData= image
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
                    // ImageData= image
                };

                return Ok(response);
            }
        }

        // PUT api/<UserPagesController>/5
        [HttpPut("ChangeAbout/{id}")]
        public async Task<ActionResult<UserPageResponse>> ChangeAbout(Guid id, [FromBody] string about)
        {
            var page = await _userPages.EditAbout(about, id);

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
                    // ImageData= image
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
                    // ImageData= image
                };

                return Ok(response);
            }
        }

        // PUT api/<UserPagesController>/5
        [HttpPut("ChangeStatus/{id}")]
        public async Task<ActionResult<UserPageResponse>> ChangeStatus(Guid id, [FromBody] string status)
        {
            var page = await _userPages.EditStatus(status, id);

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
                    // ImageData= image
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
                    // ImageData= image
                };

                return Ok(response);
            }
        }

        // DELETE api/<UserPagesController>/5
        [HttpDelete("DeleteUserPage/{id}")]
        public void DeleteUserPage(Guid userId)
        {
             _userPages.DeleteUserPageAsync(userId);
        }

        [HttpDelete("DeleteUserPhoto/{id}")]
        public async Task<ActionResult<UserPage>> DeleteUserPhoto(Guid userId, Guid photoId)
        {
            var page = await _userPages.DeletePhotoAsync(photoId, userId);

            return page;
        }
    }
}

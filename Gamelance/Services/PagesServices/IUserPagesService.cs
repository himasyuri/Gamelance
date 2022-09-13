using Gamelance.Models.Dto;
using Gamelance.Models.Users;

namespace Gamelance.Services.PagesServices
{
    public interface IUserPagesService
    {
        ValueTask<UserPage> GetUserPageAsync(Guid id);

        ValueTask<List<UserPage>> GetUserPagesNotDeletedAsync();

        ValueTask<List<UserPage>> GetUserPagesAsync();

        ValueTask<List<UserPage>> GetUserDeletedPagesAsync();

        ValueTask<List<UserPage>> GetBannedUsersAsync();

        ValueTask<UserPage> CreateUserPageAsync(UserPageRegistration model, Guid userId);

        ValueTask<UserPage> EditName(string name, Guid pageId);

        ValueTask<UserPage> AddPhotoAsync(IFormFile file, Guid userId);

        ValueTask<UserPage> EditPhoto(IFormFile file, Guid pageId);

        ValueTask<UserPage> DeletePhotoAsync(Guid photoId, Guid userId);

        ValueTask<UserPage> AddStatusAsync(string status, Guid userId);

        ValueTask<UserPage> EditStatus(string status, Guid pageId);

        ValueTask<UserPage> AddAboutAsync(string about, Guid userId);

        ValueTask<UserPage> EditAbout(string about, Guid pageId);

        void DeleteUserPageAsync(Guid userId);

        void BanUserPageAsync(Guid pageId);

        void UnBanUserPage(Guid pageId);

        void RemoveDeletedPagesFromDatabase(List<UserPage> userPages);
    }
}

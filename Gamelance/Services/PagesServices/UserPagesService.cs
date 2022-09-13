using Gamelance.Data;
using Gamelance.Models.Dto;
using Gamelance.Models.Users;
using Gamelance.Models.Photos;
using Gamelance.Models.Applications;

namespace Gamelance.Services.PagesServices
{
    public class UserPagesService : IUserPagesService
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserPagesService(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async ValueTask<UserPage> AddAboutAsync(string about, Guid userId)
        {
            UserPage? userPage = await _context.UserPages.FindAsync(userId);

            if (userPage == null)
            {
                throw new Exception("Page not found");
            }

            userPage.About = about;

            return userPage;
        }

        public async ValueTask<UserPage> AddPhotoAsync(IFormFile file, Guid userId)
        {
            UserPage? userPage = await _context.UserPages.FindAsync(userId);

            if (userPage == null)
            {
                throw new Exception("Page not found");
            }

            var photo = await SavePhoto(file);
            userPage.Avatar = photo;

            await _context.Photos.AddAsync(photo);
            await _context.SaveChangesAsync();
            
            return userPage;
        }

        public async ValueTask<UserPage> AddStatusAsync(string status, Guid userId)
        {
            UserPage? userPage = await _context.UserPages.FindAsync(userId);

            if (userPage == null)
            {
                throw new Exception("Page not found");
            }

            userPage.Status = status;

            await _context.SaveChangesAsync();

            return userPage;
        }

        public async ValueTask<UserPage> CreateUserPageAsync(UserPageRegistration model, Guid userId)
        {
            UserPage userPage = new UserPage
            {
                PageId = userId, 
                Name = model.Name,
                About = model.About,
            };

            //if (model.Photo != null)
            //{
            //    var photo = await SavePhoto(model.Photo);
            //    userPage.Avatar = photo;

            //    await _context.Photos.AddAsync(photo);
            //    await _context.UserPages.AddAsync(userPage);
            //    await _context.SaveChangesAsync();

            //    return userPage;
            //}

            await _context.AddAsync(userPage);
            await _context.SaveChangesAsync();

            return userPage;
        }

        public async void BanUserPageAsync(Guid pageId)
        {
            UserPage? userPage = await _context.UserPages.FindAsync(pageId);

            if (userPage == null)
            {
                throw new Exception("Not found");
            }

            userPage.IsBanned = true;

            await _context.SaveChangesAsync();
        }

        public async void DeleteUserPageAsync(Guid userId)
        {
            UserPage? userPage = await _context.UserPages.FindAsync(userId);

            if (userPage == null)
            {
                throw new Exception("Page not found");
            }

            userPage.IsDeleted = true;

            await _context.SaveChangesAsync();
        }

        public async ValueTask<UserPage> EditAbout(string about, Guid pageId)
        {
            UserPage? userPage = await _context.UserPages.FindAsync(pageId);

            if (userPage == null)
            {
                throw new Exception("Page not found");
            }

            if (about == null)
            {
                return userPage;
            }

            userPage.About = about;

            await _context.SaveChangesAsync();

            return userPage;
        }

        public async ValueTask<UserPage> EditName(string name, Guid pageId)
        {
            UserPage? userPage = await _context.UserPages.FindAsync(pageId);

            if (userPage == null)
            {
                throw new Exception("Page not found");
            }

            ChangeUserNameApps application = new ChangeUserNameApps
            {
                NewName = name,
                UserId = pageId,
            };

            await _context.ChangeUserNameAppsStore.AddAsync(application);
            await _context.SaveChangesAsync();

            return userPage;
        }

        public ValueTask<UserPage> EditPhoto(IFormFile file, Guid pageId)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<UserPage> EditStatus(string status, Guid pageId)
        {
            UserPage? userPage = await _context.UserPages.FindAsync(pageId);

            if (userPage == null)
            {
                throw new Exception("Page not found");
            }

            if (status == null)
            {
                return userPage;
            }

            userPage.About = status;

            await _context.SaveChangesAsync();

            return userPage;
        }

        public async ValueTask<UserPage> GetUserPageAsync(Guid id)
        {
            UserPage? userPage = await _context.UserPages.FindAsync(id);

            if (userPage == null)
            {
                throw new Exception("Not found");
            }

            if (userPage.Avatar != null)
            {

            }

            return userPage;
        }

        public async ValueTask<List<UserPage>> GetUserPagesAsync()
        {
            List<UserPage> userPages = await _context.UserPages.ToListAsync();

            return userPages;
        }

        public async ValueTask<List<UserPage>> GetUserDeletedPagesAsync()
        {
            List<UserPage> userPages = await _context.UserPages.Where(u => u.IsDeleted == true)
                                                               .ToListAsync();

            return userPages;
        }

        public async ValueTask<List<UserPage>> GetBannedUsersAsync()
        {
            List<UserPage> userPages = await _context.UserPages.Where(u => u.IsBanned == true)
                                                               .ToListAsync();

            return userPages;
        }

        public void RemoveDeletedPagesFromDatabase(List<UserPage> userPages)
        {
            _context.UserPages.RemoveRange(userPages);
            _context.SaveChanges();
        }

        public async ValueTask<UserPage> DeletePhotoAsync(Guid photoId, Guid userId)
        {
            UserPage? userPage = await _context.UserPages.FindAsync(userId);
            if (userPage == null)
            {
                throw new Exception("Page not found");
            }

            Photo? photo = await _context.Photos.FindAsync(photoId);

            if (photo == null)
            {
                throw new Exception("Not found");
            }

            photo.IsDeleted = true;
            userPage.Avatar.IsDeleted = true;

            await _context.SaveChangesAsync();

            return userPage;
        }

        public async ValueTask<List<UserPage>> GetUserPagesNotDeletedAsync()
        {
            List<UserPage> userPages = await _context.UserPages.Where(u => u.IsDeleted == false)
                                                               .ToListAsync();

            return userPages;
        }

        public void UnBanUserPage(Guid pageId)
        {
            UserPage? userPage =  _context.UserPages.Find(pageId);

            if (userPage == null)
            {
                throw new Exception("Not found");
            }

            userPage.IsBanned = false;

            _context.SaveChanges();
        }

        private async Task<Photo> SavePhoto(IFormFile file)
        {
            string path = "/Images/" + file.FileName;

            if (file.FileName == null)
            {
                Photo defPhoto = new Photo
                {
                    FileName = "default",
                    Path = "default"
                };

                return defPhoto;
            }

            using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            Photo photo = new Photo
            {
                FileName = file.FileName,
                Path = _webHostEnvironment.WebRootPath + path
            };

            return photo;
        }
    }
}

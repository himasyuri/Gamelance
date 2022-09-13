using Gamelance.Data;
using Gamelance.Models.Applications;
using Gamelance.Models.Users;

namespace Gamelance.Services.AppsService
{
    public class ChangeUserNameAppsService : IChangeUserNameAppsService
    {
        private readonly DataContext _context;

        public ChangeUserNameAppsService(DataContext context)
        {
            _context = context;
        }

        public Task AddComment(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task ApproveApplicationAsync(Guid id)
        {
            ChangeUserNameApps? application = await _context.ChangeUserNameAppsStore.FindAsync(id);

            if (application == null)
            {
                throw new Exception("Application not found");
            }

            application.IsApprove = true;
            application.IsSeen = true;

            UserPage? userPage = await _context.UserPages.FindAsync(application.UserId);

            if (userPage == null)
            {
                throw new Exception("Page not found");
            }

            userPage.Name = application.NewName;

            await _context.SaveChangesAsync();

            return;
        }

        public void DeleteApplicationAsync(Guid id)
        {
            ChangeUserNameApps? application = _context.ChangeUserNameAppsStore.Find(id);

            if (application == null)
            {
                throw new Exception("Application not found");
            }

            _context.ChangeUserNameAppsStore.Remove(application);
            _context.SaveChanges();
        }

        public async Task<ChangeUserNameApps> GetApplicationAsync(Guid id)
        {
            ChangeUserNameApps? application = await _context.ChangeUserNameAppsStore.FindAsync(id);

            if (application == null)
            {
                throw new Exception("Application not found");
            }

            return application;
        }

        public async Task<List<ChangeUserNameApps>> GetApplicationsAsync()
        {
            List<ChangeUserNameApps> lst = await _context.ChangeUserNameAppsStore.ToListAsync();
            
            return lst;
        }

        public async Task RejectApplicationAsync(Guid id)
        {
            ChangeUserNameApps? application = await _context.ChangeUserNameAppsStore.FindAsync(id);

            if (application == null)
            {
                throw new Exception("Application not found");
            }

            application.IsSeen = true;

            await _context.SaveChangesAsync();

            return;
        }
    }
}

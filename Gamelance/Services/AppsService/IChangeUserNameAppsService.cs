using Gamelance.Models.Applications;

namespace Gamelance.Services.AppsService
{
    public interface IChangeUserNameAppsService
    {
        Task<List<ChangeUserNameApps>> GetApplicationsAsync();

        Task<ChangeUserNameApps> GetApplicationAsync(Guid id);

        Task ApproveApplicationAsync(Guid id);

        Task RejectApplicationAsync(Guid id);

        Task AddComment(Guid id);

        void DeleteApplicationAsync(Guid id);
    }
}

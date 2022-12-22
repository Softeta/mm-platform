using Microsoft.Graph;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BackOffice.Users.CacheRefresher.BackOfficeUsersServices
{
    public interface IMsGraphServiceClient
    {
        Task<List<User>> GetUsersForGroupsAsync(string[] groupNames, CancellationToken cancellationToken = default);
        Task<Dictionary<string, ProfilePhoto>> GetProfilePicturesAsync(string[] userIds, CancellationToken cancellationToken = default);
    }
}

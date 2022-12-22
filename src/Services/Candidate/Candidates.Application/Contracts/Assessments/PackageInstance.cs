using Candidates.Infrastructure.Clients.Talogy.Models.Responses;

namespace Candidates.Application.Contracts.Assessments
{
    public class PackageInstance
    {
        public string PackageInstanceId { get; set; } = null!;
        public string LogonUrl { get; set; } = null!;

        public static PackageInstance From(CreatedPackageInstance createdPackageInstance, string packageInstanceId)
        {
            return new PackageInstance
            {
                PackageInstanceId = packageInstanceId,
                LogonUrl = createdPackageInstance.LogonUrl
            };
        }
    }
}

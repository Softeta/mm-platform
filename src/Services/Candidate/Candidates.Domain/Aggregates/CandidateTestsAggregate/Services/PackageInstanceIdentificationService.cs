namespace Candidates.Domain.Aggregates.CandidateTestsAggregate.Services
{
    public static class PackageInstanceIdentificationService
    {
        public static string BuildPackageInstanceId(this Guid candidateId, string packageType, string assessmentCode)
        {
            if (string.IsNullOrWhiteSpace(packageType))
            {
                throw new ArgumentException($"{nameof(packageType)} can't be empty");
            }

            if (string.IsNullOrWhiteSpace(assessmentCode))
            {
                throw new ArgumentException($"{nameof(assessmentCode)} can't be empty");
            }

            return $"{packageType}-{candidateId}-{assessmentCode}";
        }
    }
}

using Candidates.Domain.Aggregates.CandidateAggregate.Entities;

namespace Candidates.Domain.Aggregates.CandidateAggregate.Services
{
    public static class CandidateHobbiesCalibrationService
    {
        public static void Calibrate(this List<CandidateHobby> current, IEnumerable<CandidateHobby> request, Guid candidateId)
        {
            var requestHobbyIds = new HashSet<Guid>(request.Select(s => s.HobbyId));
            var currentHobbyIds = new HashSet<Guid>(current.Select(s => s.HobbyId));

            var equals = requestHobbyIds.SetEquals(currentHobbyIds);

            if (!equals)
            {
                current.RemoveAll(s => !requestHobbyIds.Contains(s.HobbyId));

                var hobbiesToAdd = request.Where(s => !currentHobbyIds.Contains(s.HobbyId));

                foreach (var hobby in hobbiesToAdd)
                {
                    current.Add(new CandidateHobby(hobby.HobbyId, candidateId, hobby.Code));
                }
            }
        }
    }
}

using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using System;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateAggregate
{
    public class CandidateShortListedJobTests
    {
        [Theory, AutoMoqData]
        public void Constructor_ShouldInitCandidateShortListedJob(Guid candidateId, Guid jobId)
        {
            // Act
            var candidateShortListedJob = new CandidateShortListedJob(candidateId, jobId);

            // Assert
            Assert.NotEqual(Guid.Empty, candidateShortListedJob.Id);
            Assert.Equal(candidateId, candidateShortListedJob.CandidateId);
            Assert.Equal(jobId, candidateShortListedJob.JobId);
        }
    }
}

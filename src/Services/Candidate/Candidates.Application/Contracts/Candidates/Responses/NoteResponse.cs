using Candidates.Domain.Aggregates.CandidateAggregate.ValueObjects;
using Common = Contracts.Candidate.Notes.Responses;

namespace Candidates.Application.Contracts.Candidates.Responses
{
    public class NoteResponse : Common.NoteResponse
    {
        public static NoteResponse? FromDomain(Note? note)
        {
            if (note is null) return null;

            return new NoteResponse
            {
                Value = note.Value,
                EndDate = note.EndDate
            };
        }
    }
}

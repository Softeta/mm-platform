namespace Contracts.Company.Responses.ContactPersons
{
    public record GetContactPersonsResponse(
        int Count,
        IEnumerable<GetContactPersonBriefResponse> ContactPersons);
}

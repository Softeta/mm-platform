namespace Candidates.Infrastructure.Clients.MicrosoftGraph
{
    public interface IMsGraphServiceClient
    {
        Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}

namespace Companies.Infrastructure.Clients
{
    public interface IMsGraphServiceClient
    {
        Task UpdateUserAttributesAsync(Guid objectId, Guid companyId, Guid contactId, bool isAdmin, CancellationToken cancellationToken = default);
        Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}

using MediatR;

namespace Persistence.Customization.Queries
{
    public record GetFileQuery(Guid? CacheId, string TablePartitionKey) 
        : IRequest<(string? FileUrl, string? FileName)>;
}

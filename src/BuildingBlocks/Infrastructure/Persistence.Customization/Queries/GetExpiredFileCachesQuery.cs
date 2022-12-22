using MediatR;
using Persistence.Customization.TableStorage.Models;

namespace Persistence.Customization.Queries
{
    public record GetExpiredFileCachesQuery(string TablePartitionKey) : IRequest<List<FileCacheEntity>>;
}

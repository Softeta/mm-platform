using Domain.Seedwork.Enums;
using MediatR;

namespace Persistence.Customization.Queries
{
    public record GetImageQuery(Guid? CacheId, string TablePartitionKey) 
        : IRequest<Dictionary<ImageType, string?>?>;
}

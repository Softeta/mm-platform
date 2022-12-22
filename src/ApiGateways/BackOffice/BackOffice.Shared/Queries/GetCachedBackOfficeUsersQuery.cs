using BackOffice.Shared.Entities;
using MediatR;

namespace BackOffice.Shared.Queries
{
    public record GetCachedBackOfficeUsersQuery(List<Guid>? BackOfficeUserIds = null) : IRequest<ICollection<BackOfficeUserEntity>>;
}

using Domain.Seedwork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Customization.Context
{
    public abstract class BaseContext<TContext> : DbContext, IUnitOfWork where TContext : DbContext
    {
        private readonly IMediator _mediator;

        protected BaseContext(DbContextOptions<TContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TContext).Assembly);
        }

        public async Task<bool> SaveEntitiesAsync<T>(CancellationToken cancellationToken = default)
            where T : AggregateRoot
        {
            var entities = ChangeTracker
                .Entries<T>()
                .Select(entry => entry.Entity)
                .ToList();

            await SaveChangesAsync(cancellationToken);
            await EmitAndClearEventsAsync(entities);

            return true;
        }

        private async Task EmitAndClearEventsAsync(IEnumerable<AggregateRoot> entities)
        {
            foreach (var entity in entities)
            {
                var events = entity.DomainEvents
                    .Where(x => !x.Published);

                foreach (var notification in events)
                {
                    notification.MarkAsPublished();
                    await _mediator.Publish(notification);
                }
            }
        }
    }
}

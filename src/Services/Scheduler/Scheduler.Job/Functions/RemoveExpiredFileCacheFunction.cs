using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Scheduler.Job.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduler.Job.Functions
{
    public class RemoveExpiredFileCacheFunction
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RemoveExpiredFileCacheFunction> _logger;

        public RemoveExpiredFileCacheFunction(
            IMediator mediator,
            ILogger<RemoveExpiredFileCacheFunction> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [FunctionName(nameof(RemoveExpiredFileCacheFunction))]
        public async Task Run(
            [TimerTrigger("0 0 4 * * *")] TimerInfo myTimer,
            CancellationToken cancellationToken)
        {
            var notification = new RemoveExpiredFileCacheScheduledEvent(DateTimeOffset.UtcNow);
            await _mediator.Publish(notification);

            _logger.LogInformation($"{nameof(RemoveExpiredFileCacheFunction)} function executed.");
        }
    }
}

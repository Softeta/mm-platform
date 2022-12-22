using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Scheduler.Job.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduler.Job.Functions
{
    public class SyncRegistryCenterCompaniesFunction
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SyncRegistryCenterCompaniesFunction> _logger;

        public SyncRegistryCenterCompaniesFunction(
            IMediator mediator,
            ILogger<SyncRegistryCenterCompaniesFunction> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [FunctionName(nameof(SyncRegistryCenterCompaniesFunction))]
        public async Task Run(
            [TimerTrigger("0 0 3 * * 0")] TimerInfo myTimer,
            CancellationToken cancellationToken)
        {
            var notification = new SyncRegistryCenterCompaniesScheduledEvent(DateTimeOffset.UtcNow);
            await _mediator.Publish(notification);

            _logger.LogInformation($"{nameof(SyncRegistryCenterCompaniesFunction)} function executed.");
        }
    }
}

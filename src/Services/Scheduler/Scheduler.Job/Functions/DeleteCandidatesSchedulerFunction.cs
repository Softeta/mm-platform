using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Scheduler.Job.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduler.Job.Functions
{
    public class DeleteCandidatesSchedulerFunction
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DeleteCandidatesSchedulerFunction> _logger;

        public DeleteCandidatesSchedulerFunction(
            IMediator mediator,
            ILogger<DeleteCandidatesSchedulerFunction> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [FunctionName(nameof(DeleteCandidatesSchedulerFunction))]
        public async Task Run(
            [TimerTrigger("0 0 3 * * *")] TimerInfo myTimer,
            CancellationToken cancellationToken)
        {
            var notification = new DeleteCandidatesScheduledEvent(DateTimeOffset.UtcNow);
            await _mediator.Publish(notification);

            _logger.LogInformation($"{nameof(DeleteCandidatesSchedulerFunction)} function executed.");
        }
    }
}

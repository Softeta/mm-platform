using API.Customization.Extensions;
using Azure.Data.Tables;
using EmailService.Shared.Enums;
using EmailService.WebHook.Events;
using EmailService.WebHook.IntegrationEventHandlers.Publishers.Payloads;
using EmailService.WebHook.Models;
using EventBus.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Persistence.Customization.TableStorage;
using Persistence.Customization.TableStorage.Clients;
using Persistence.Customization.TableStorage.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EmailService.WebHook.Functions
{
    public class EmailMessageWebHookFunction
    {
        private const string FunctionName = "web-hook";
        private readonly TableClient _emailMessagesClient;
        private readonly IMediator _mediator;
        private readonly ILogger<EmailMessageWebHookFunction> _logger;

        public EmailMessageWebHookFunction(
            IPrivateTableServiceClient client,
            IMediator mediator, 
            ILogger<EmailMessageWebHookFunction> logger)
        {
            _emailMessagesClient = client.GetTableClient(EmailMessageTableStorageConstants.TableName);
            _mediator = mediator;
            _logger = logger;
        }

        [FunctionName(FunctionName)]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiRequestBody("application/json", typeof(WebHookRequest), Required = true)]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NoContent)]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest request)
        {
            try
            {
                var webHook = await request.ParseRequestObjectAsync<WebHookRequest>();

                var emailEntity = await _emailMessagesClient
                    .GetEntityAsync<EmailMessageTrackerEntity>(
                        EmailMessageTableStorageConstants.PartitionKey,
                        webHook.MessageId);

                if (Enum.TryParse(webHook.Event, out EmailStatus status))
                {
                    emailEntity.Value.Status = status.ToString();

                    var updateEntity = await _emailMessagesClient
                        .UpdateEntityAsync<EmailMessageTrackerEntity>(
                        emailEntity,
                        emailEntity.Value.ETag);

                    PublishEvent(status, emailEntity.Value, webHook);
                }

                return new NoContentResult();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Error occurred in {FunctionName}. Exception: {Exception}", FunctionName, ex.Message);
                throw;
            } 
        }

        private void PublishEvent(EmailStatus status, EmailMessageTrackerEntity emailEntity, WebHookRequest webHook)
        {
            switch (status)
            {
                case EmailStatus.delivered:
                    // await PublishEventAsync(emailEntity, webHook); TODO #2361
                    break;
                case EmailStatus.soft_bounce:
                case EmailStatus.hard_bounce:
                case EmailStatus.invalid_email:
                    _logger.LogCritical("Email message was not delivered. MessaggeId: {MessageId} Status: {Status}", webHook.MessageId, status);
                    break;
            }
        }

        private async Task PublishEventAsync(EmailMessageTrackerEntity emailEntity, WebHookRequest webHook)
        {
            var eventName = GetEventNameByMessageSubject(emailEntity);

            var notification = new EmailServiceWebHookedEvent(
                new EmailServiceWebHookPayload
                {
                    EntityId = emailEntity.EntityId,
                    TargetId = emailEntity.TargetId,
                    Email = emailEntity.Email,
                    Status = webHook.Event,
                    Date = webHook.Date
                }, DateTimeOffset.UtcNow, eventName);

            await _mediator.Publish(notification);
        }

        private static string GetEventNameByMessageSubject(EmailMessageTrackerEntity entity)
        {
            switch (entity.FilterName)
            {
                case Topics.JobShareChanged.Filters.AskedForJobApproval:
                    return Topics.EmailServiceWebHooked.Filters.AskedForJobApprovalWebHook;
                case Topics.CandidateChanged.Filters.CandidateRegistered:
                    return Topics.EmailServiceWebHooked.Filters.CandidateVerificationWebHook;
                case Topics.CandidateChanged.Filters.CandidateApproved:
                    return Topics.EmailServiceWebHooked.Filters.CandidateApprovedWebHook;
                case Topics.CandidateChanged.Filters.CandidateRejected:
                    return Topics.EmailServiceWebHooked.Filters.CandidateRejectedWebHook;
                default:
                    throw new Exception($"Filter was not found. FilterName: {entity.FilterName}. EntityId: {entity.EntityId}. TargetId: {entity.TargetId}");
            }
        }
    }
}

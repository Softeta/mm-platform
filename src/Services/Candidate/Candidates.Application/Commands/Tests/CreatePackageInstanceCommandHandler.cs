using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Clients.Talogy;
using Candidates.Infrastructure.Clients.Talogy.Constants;
using Candidates.Infrastructure.Clients.Talogy.Models.Requests;
using Candidates.Infrastructure.Clients.Talogy.Models.Responses;
using Candidates.Infrastructure.Settings;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using MediatR;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using LocalEndpoint = Candidates.Infrastructure.Constants.Endpoints;

namespace Candidates.Application.Commands.Tests
{
    public class CreatePackageInstanceCommandHandler : IRequestHandler<CreatePackageInstanceCommand, CreatedPackageInstance>
    {
        private readonly ITalogyApiClient _talogyApiClient;
        private readonly TalogySettings _talogySettings;
        private readonly SelfServiceSettings _selfServiceSettings;

        public CreatePackageInstanceCommandHandler(
            ITalogyApiClient talogyApiClient,
            IOptions<TalogySettings> talogySettings,
            IOptions<SelfServiceSettings> selfServiceSettings)
        {
            _talogyApiClient = talogyApiClient;
            _talogySettings = talogySettings.Value;
            _selfServiceSettings = selfServiceSettings.Value;
        }

        public async Task<CreatedPackageInstance> Handle(CreatePackageInstanceCommand request, CancellationToken cancellationToken)
        {
            Validate(request.candidate);

            var candidate = request.candidate;

            var selfServiceWebsite = _selfServiceSettings.Website;
            var notificationEndpoint = string.Format(LocalEndpoint.CandidateTests.Notifications, candidate.Id, candidate.ExternalId);

            var payload = new CreatePackageInstance
            {
                ParticipantId = candidate.Id.ToString(),
                FirstName = candidate.FirstName!,
                LastName = candidate.LastName!,
                EmailAddress = candidate.Email!.Address,
                LocaleCode = GetSupportedLocale(candidate.SystemLanguage),
                PackageTypeId = request.PackageTypeId,
                CompletionUrl = selfServiceWebsite.AbsolutePath(selfServiceWebsite.TestsPath),
                NotificationUrl = $"{_selfServiceSettings.ApiUrl}/{notificationEndpoint}"
            };

            var endpoint = $"{Endpoints.PackageInstances}/{request.PackageInstanceId}";
            var result = await _talogyApiClient.PostAsync<CreatePackageInstance, CreatedPackageInstance>(payload, endpoint);

            if (string.IsNullOrWhiteSpace(result?.LogonUrl))
            {
                throw new HttpRequestException("LogonUrl was not returned");
            }

            return result;
        }

        private string GetSupportedLocale(SystemLanguage? systemLanguage)
        {
            var language = (systemLanguage ?? SystemLanguage.EN).ToString();
            if (_talogySettings.SupportedLocales.TryGetValue(language, out var locale))
            {
                return locale;
            }

            return _talogySettings.DefaultLocale;
        }

        private static void Validate(Candidate candidate)
        {
            if (string.IsNullOrWhiteSpace(candidate.FirstName))
            {
                throw new BadRequestException($"Candidate {nameof(candidate.FirstName)} not filled", ErrorCodes.Candidate.FirstNameRequired);
            }

            if (string.IsNullOrWhiteSpace(candidate.LastName))
            {
                throw new BadRequestException($"Candidate {nameof(candidate.LastName)} not filled", ErrorCodes.Candidate.LastNameRequired);
            }

            if (string.IsNullOrWhiteSpace(candidate.Email?.Address))
            {
                throw new BadRequestException($"Candidate {nameof(candidate.FirstName)} not filled", ErrorCodes.Candidate.EmailNotFound);
            }
        }
    }
}

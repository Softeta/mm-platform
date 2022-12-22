using Companies.Domain.Aggregates.CompanyAggregate;
using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Companies.Infrastructure.Persistence.Repositories;
using Contracts.Shared.Requests;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;
using MediatR;
using Persistence.Customization.FileStorage.Clients.Public;
using Persistence.Customization.Queries;
using Persistence.Customization.TableStorage;

namespace Companies.Application.Commands.ContactPersons
{
    internal class UpdateContactPersonCommandHandler : IRequestHandler<UpdateContactPersonCommand, ContactPerson>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMediator _mediator;
        private readonly IPublicFileDeleteClient _publicFileDeleteClient;

        public UpdateContactPersonCommandHandler(
            ICompanyRepository companyRepository,
            IMediator mediator,
            IPublicFileDeleteClient publicFileDeleteClient)
        {
            _companyRepository = companyRepository;
            _mediator = mediator;
            _publicFileDeleteClient = publicFileDeleteClient;
        }

        public async Task<ContactPerson> Handle(UpdateContactPersonCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetAsync(request.CompanyId);

            var contactPerson = company.ContactPersons
                .FirstOrDefault(x => x.Id == request.ContactId);

            if (contactPerson is null)
            {
                throw new NotFoundException(
                    $"Contact person not found. ContactId: {request.ContactId}. CompanyId: {request.CompanyId}",
                    ErrorCodes.NotFound.ContactPersonNotFound);
            }

            var picture = request.ContactPerson.Picture;
            var deleteContactPersonPictureTask = DeleteOldImageAsync(picture.HasChanged, contactPerson.Picture, cancellationToken);
            var newPicturesTask = GetImagesAsync(picture);
            await Task.WhenAll(deleteContactPersonPictureTask, newPicturesTask);

            var updatedContactPerson = company.UpdateContactPerson(
                request.ContactId,
                request.ContactPerson.Role,
                request.ContactPerson.FirstName,
                request.ContactPerson.LastName,
                request.ContactPerson.Phone?.CountryCode,
                request.ContactPerson.Phone?.Number,
                request.ContactPerson.Position?.Id,
                request.ContactPerson.Position?.Code,
                request.ContactPerson.Position?.AliasTo?.Id,
                request.ContactPerson.Position?.AliasTo?.Code,
                newPicturesTask.Result,
                picture.HasChanged,
                picture.CacheId);

            _companyRepository.Update(company);
            await _companyRepository.UnitOfWork.SaveEntitiesAsync<Company>(cancellationToken);

            return updatedContactPerson;
        }

        private async Task DeleteOldImageAsync(bool hasChanged, Image? oldImage, CancellationToken token)
        {
            if (oldImage is not null && hasChanged)
            {
                var files = new[]
                {
                    new Uri(oldImage.OriginalUri),
                    new Uri(oldImage.ThumbnailUri)
                };

                await _publicFileDeleteClient.BatchDeleteAsync(files, token);
            }
        }

        private async Task<Dictionary<ImageType, string?>?> GetImagesAsync(UpdateFileCacheRequest image)
        {
            if (image.HasChanged)
            {
                var command = new GetImageQuery(image.CacheId,
                    FileCacheTableStorage.Company.FilePartitionKey);
                return await _mediator.Send(command);
            }
            return null;
        }
    }
}

using Companies.Domain.Aggregates.CompanyAggregate.Entities;
using Companies.Infrastructure.Persistence;
using Contracts.Company.Responses.ContactPersons;
using Contracts.Shared;
using Contracts.Shared.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Companies.Application.Queries
{
    public class GetContactPersonsQueryHandler : IRequestHandler<GetContactPersonsQuery, GetContactPersonsResponse>
    {
        private readonly ICompanyContext _companyContext;

        public GetContactPersonsQueryHandler(ICompanyContext companyContext)
        {
            _companyContext = companyContext;
        }

        public async Task<GetContactPersonsResponse> Handle(GetContactPersonsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<ContactPerson, bool>> filterByContactPersons = contact =>
                request.ContactPersons == null || request.ContactPersons.Contains(contact.Id);

            var contactPersonsQuery = _companyContext.ContactPersons
                .AsNoTracking()
                .Where(filterByContactPersons)
                .AsQueryable();

            var count = await contactPersonsQuery.CountAsync(cancellationToken);

            var contactPersons = await contactPersonsQuery
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new GetContactPersonBriefResponse
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Position = x.Position != null ? new Position
                    {
                        Id = x.Position.Id,
                        Code = x.Position.Code,
                        AliasTo = x.Position.AliasTo != null ? new Tag
                        {
                            Id = x.Position.AliasTo.Id,
                            Code = x.Position.AliasTo.Code
                        } : null
                    } : null,
                    Phone = x.Phone != null ? new PhoneFullResponse
                    {
                        CountryCode = x.Phone.CountryCode,
                        PhoneNumber = x.Phone.PhoneNumber,
                        Number = x.Phone.Number
                    } : null,
                    Email = x.Email.Address,
                    Picture = x.Picture != null ? new ImageResponse { 
                        Uri = x.Picture.ThumbnailUri 
                    } : null,
                    SystemLanguage = x.SystemLanguage,
                    ExternalId = x.ExternalId
                })
                .ToListAsync(cancellationToken);

            return new GetContactPersonsResponse(count, contactPersons);
        }
    }
}

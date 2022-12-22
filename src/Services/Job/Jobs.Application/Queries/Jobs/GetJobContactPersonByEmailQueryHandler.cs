using Domain.Seedwork.Exceptions;
using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Jobs.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Application.Queries.Jobs
{
    public class GetJobContactPersonByEmailQueryHandler : IRequestHandler<GetJobContactPersonByEmailQuery, JobContactPerson>
    {
        private readonly IJobContext _context;

        public GetJobContactPersonByEmailQueryHandler(IJobContext context)
        {
            _context = context;
        }

        public async Task<JobContactPerson> Handle(GetJobContactPersonByEmailQuery request, CancellationToken cancellationToken)
        {
            var contactPerson = await _context.Jobs
                .Include(j => j.Company.ContactPersons)
                .Where(x => x.Id == request.JobId)
                .AsNoTracking()
                .SelectMany(x => x.Company.ContactPersons
                    .Where(x => x.Email.ToLower() == request.EmailAddress.ToLower()))
                .FirstOrDefaultAsync();

            if (contactPerson is null)
            {
                throw new NotFoundException($"Contact person of job was not found with {request.EmailAddress} email.",
                    ErrorCodes.NotFound.JobContactPersonByEmailNotFound, new string[] { request.EmailAddress });
            }

            return contactPerson;
        }
    }
}

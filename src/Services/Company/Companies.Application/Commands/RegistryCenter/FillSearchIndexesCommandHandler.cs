using Companies.Infrastructure.Persistence;
using Companies.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Companies.Application.Commands.RegistryCenter
{
    public class FillSearchIndexesCommandHandler : INotificationHandler<FillSearchIndexesCommand>
    {
        private readonly CompanyContext _context;
        private readonly IRcCompanyRepository _rcCompanyRepository;
        private readonly ILogger<FillSearchIndexesCommandHandler> _logger;

        public FillSearchIndexesCommandHandler(
            CompanyContext context,
            IRcCompanyRepository rcCompanyRepository,
            ILogger<FillSearchIndexesCommandHandler> logger)
        {
            _context = context;
            _rcCompanyRepository = rcCompanyRepository;
            _logger = logger;
        }

        public async Task Handle(FillSearchIndexesCommand request, CancellationToken cancellationToken)
        {
            int pageSize = 200;
            int? nextPage = 1;

            while (true)
            {
                nextPage = await SyncOnePageAsync(pageSize, nextPage.Value, cancellationToken);

                if (nextPage is null) return;
            }
        }

        private async Task<int?> SyncOnePageAsync(int pageSize, int pageNumber, CancellationToken cancellationToken)
        {
            try
            {
                var companies = await _context.RegistryCenterCompanies
                    .Include(x => x.SearchIndexes)
                    .OrderBy(x => x.RegistrationNumber)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync(cancellationToken);

                if (companies is null || companies.Count == 0) return null;

                foreach (var company in companies)
                {
                    company.BuildIndexes();
                }

                await _rcCompanyRepository.AddOrUpdateRange(companies);
                await _rcCompanyRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fill search indexes");
            }

            return pageNumber + 1;
        }
    }
}

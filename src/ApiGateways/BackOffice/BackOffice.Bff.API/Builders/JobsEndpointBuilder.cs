using API.Customization.Constants;
using Domain.Seedwork.Enums;
using System.Collections.ObjectModel;
using System.Text;

namespace BackOffice.Bff.API.Builders
{
    public class JobsEndpointBuilder
    {
        private string _endpoint = null!;
        protected Guid UserId { get; private set; }
        protected Collection<Guid>? Companies { get; private set; }
        protected Collection<Guid>? Positions { get; private set; }
        protected DateTimeOffset? DeadLineDate { get; private set; }
        protected Collection<WorkType>? WorkTypes { get; private set; }
        protected Collection<JobStage>? JobStages { get; private set; }
        protected Collection<string>? Locations { get; private set; }
        protected Guid? Owner { get; private set; }
        protected int? PageNumber { get; private set; }
        protected int? PageSize { get; private set; }
        protected string? Search { get; private set; }
        protected JobOrderBy? OrderBy { get; private set; }

        public JobsEndpointBuilder ForEndpoint(string endpoint)
        {
            _endpoint = endpoint;
            return this;
        }

        public JobsEndpointBuilder WithUser(Guid userId)
        {
            UserId = userId;
            return this;
        }

        public JobsEndpointBuilder WithCompanies(Collection<Guid>? companies)
        {
            Companies = companies;
            return this;
        }

        public JobsEndpointBuilder WithPositions(Collection<Guid>? positions)
        {
            Positions = positions;
            return this;
        }

        public JobsEndpointBuilder WithDeadLineDate(DateTimeOffset? deadLineDate)
        {
            DeadLineDate = deadLineDate;
            return this;
        }

        public JobsEndpointBuilder WithWorkTypes(Collection<WorkType>? workTypes)
        {
            WorkTypes = workTypes;
            return this;
        }

        public JobsEndpointBuilder WithJobStages(Collection<JobStage>? jobStages)
        {
            JobStages = jobStages;
            return this;
        }

        public JobsEndpointBuilder WithLocations(Collection<string>? locations)
        {
            Locations = locations;
            return this;
        }

        public JobsEndpointBuilder WithOwner(Guid? owner)
        {
            Owner = owner;
            return this;
        }

        public JobsEndpointBuilder WithPageNumber(int? pageNumber)
        {
            PageNumber = pageNumber;
            return this;
        }

        public JobsEndpointBuilder WithPageSize(int? pageSize)
        {
            PageSize = pageSize;
            return this;
        }

        public JobsEndpointBuilder WithSearch(string? search)
        {
            Search = search;
            return this;
        }

        public JobsEndpointBuilder WithOrderBy(JobOrderBy? orderBy)
        {
            OrderBy = orderBy;
            return this;
        }

        public string Build()
        {
            var queryParams = new StringBuilder();

            queryParams.Append(_endpoint);
            queryParams.Append('?').Append(nameof(UserId)).Append('=').Append(UserId);

            queryParams.Append(FilterCollection(Companies, nameof(Companies)));
            queryParams.Append(FilterCollection(Positions, nameof(Positions)));
            queryParams.Append(FilterCollection(WorkTypes, nameof(WorkTypes)));
            queryParams.Append(FilterCollection(JobStages, nameof(JobStages)));
            queryParams.Append(FilterCollection(Locations, nameof(Locations)));

            if (DeadLineDate.HasValue)
            {
                queryParams.Append('&')
                    .Append(nameof(DeadLineDate))
                    .Append('=')
                    .Append(DeadLineDate.Value.ToUniversalTime().ToString(ContractsConstants.DateFormatZulu));
            }

            if (Owner is not null)
            {
                queryParams.Append('&').Append(nameof(Owner)).Append('=').Append(Owner);
            }

            if (PageNumber is not null)
            {
                queryParams.Append('&').Append(nameof(PageNumber)).Append('=').Append(PageNumber);
            }

            if (PageSize is not null)
            {
                queryParams.Append('&').Append(nameof(PageSize)).Append('=').Append(PageSize);
            }

            if (!string.IsNullOrWhiteSpace(Search) && Search.Length > 2)
            {
                queryParams.Append('&').Append(nameof(Search)).Append('=').Append(Search);
            }

            if (OrderBy is not null)
            {
                queryParams.Append('&').Append(nameof(OrderBy)).Append('=').Append(OrderBy);
            }

            return queryParams.ToString();
        }

        private static string FilterCollection<T>(Collection<T>? collection, string collectionName)
        {
            var queryParams = new StringBuilder();

            if (collection is null || collection.Count == 0)
            {
                return string.Empty;
            }

            foreach (var item in collection)
            {
                queryParams.Append('&').Append(collectionName).Append('=').Append(item);
            }

            return queryParams.ToString();
        }
    }
}

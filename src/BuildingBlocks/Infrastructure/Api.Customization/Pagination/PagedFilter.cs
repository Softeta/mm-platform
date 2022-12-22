using API.Customization.Constants;

namespace API.Customization.Pagination
{
    public abstract class PagedFilter
    {
        private int _pageNumber = PaginationConstants.DefaultPageNumber;
        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value < 1 ? 1 : value;
        }

        private int _pageSize = PaginationConstants.DefaultPageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value is > PaginationConstants.MaxPageSize or < PaginationConstants.MinPageSize 
                ? 
                PaginationConstants.MaxPageSize 
                : 
                value;
        }
    }
}

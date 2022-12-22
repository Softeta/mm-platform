using API.Customization.Pagination;

namespace Companies.API.Models
{
    public class ContactPersonFilters : PagedFilter
    {
        public IEnumerable<Guid>? ContactPersons { get; set; }
    }
}

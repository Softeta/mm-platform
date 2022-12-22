using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Customization.Controllers.Attributes
{
    public class ProducesResponseNoContentAttribute : ProducesResponseTypeAttribute
    {
        public ProducesResponseNoContentAttribute() : base(StatusCodes.Status204NoContent)
        {
        }
    }
}

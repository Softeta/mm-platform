using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Customization.Controllers.Attributes
{
    public class ProducesResponseOkAttribute : ProducesResponseTypeAttribute
    {
        public ProducesResponseOkAttribute() : base(StatusCodes.Status200OK)
        {
        }
    }
}

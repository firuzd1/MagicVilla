
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers.v2
{
    [Route("api/v{version:apiVersion}/VillaNumberAPI")]
    [ApiController]
    [ApiVersion("2.0")]
    public class VillaNumberAPIv2Controller
    {

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Value1", "Value2" };
        }
    }
}

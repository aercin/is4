using System.Threading.Tasks;
using System.Web.Http;

namespace legacy_api.Controllers
{
    [Authorize]
    public class ProtectedResourceController : ApiController
    {
        [Route("api/protected-values")]
        [HttpGet()]
        public async Task<IHttpActionResult> SampleQuery()
        {
            return Ok(await Task.FromResult(new string[] { "value1", "value2" }));
        }
    }
}

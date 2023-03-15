using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace api.Controllers
{
    [Authorize]
    public class SecurityController : ApiController
    {
        [Route("api/permission-check")]
        [HttpGet()]
        public async Task<IHttpActionResult> PermissionCheck(string permission)
        {
            bool hasPermission = false;

            ClaimsPrincipal principal = HttpContext.Current.User as ClaimsPrincipal;
            if (null != principal)
            {
                hasPermission = principal.Claims.ToList().Any(x => x.Value.ToLower() == permission.ToLower());
            }

            return Ok(await Task.FromResult(new
            {
                IsSuccess = hasPermission
            }));
        }
    }
}

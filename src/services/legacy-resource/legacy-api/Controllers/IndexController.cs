using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace legacy_api.Controllers
{
    public class IndexController : ApiController
    {
        public async Task<IHttpActionResult> Get()
        {
            return Ok(await Task.FromResult($"Api is ready at {DateTime.Now.ToString("dd.MM.yyyy HH:mm")}"));
        }
    }
}

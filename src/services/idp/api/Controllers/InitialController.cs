using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class InitialController : ControllerBase
    {
        [Route("/")]
        [HttpGet]
        public IActionResult EntryPoint()
        {
            var discoveryDocumentAddress = $"{Request.Scheme}://{Request.Host.Value}/.well-known/openid-configuration";
            var swaggerAddress = $"{Request.Scheme}://{Request.Host.Value}/swagger";
            var html = $"<div>Idp Service is started at {DateTime.Now.ToString("dd.MM.yyyy HH:mm")} </div>"
                      +$"<div></br><a href='{discoveryDocumentAddress}'><b>Click For Discovery Document <b> </a> </div>"
                      +$"<div></br><a href='{swaggerAddress}'><b>Click For Swagger Document <b> </a> </div>";
            return new ContentResult
            {
                Content = html,
                ContentType = "text/html"
            };
        }
    }
}

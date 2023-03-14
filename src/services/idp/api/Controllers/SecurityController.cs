using application.Features.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SecurityController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [Route("permission-check")]
        [HttpGet]
        public async Task<IActionResult> PermissionCheck([FromQuery] CheckPermission.Command request)
        {
            return Ok(await this._mediator.Send(request));
        }
    }
}

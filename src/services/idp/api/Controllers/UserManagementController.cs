using application.Features.Commands;
using application.Features.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserManagementController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [Route("add/permission")]
        [HttpPost]
        public async Task<IActionResult> AddPermission(AddPermission.Command request)
        {
            return Ok(await this._mediator.Send(request));
        }

        [Route("user/preregistration")]
        [HttpPost]
        public async Task<IActionResult> UserPreregistration(UserPreRegistration.Command request)
        {
            return Ok(await this._mediator.Send(request));
        }

        [Route("user/registration")]
        [HttpPost]
        public async Task<IActionResult> UserRegistration(UserRegistration.Command request)
        {
            return Ok(await this._mediator.Send(request));
        }

        [Route("add/permission/to/user")]
        [HttpPost]
        public async Task<IActionResult> AddUserPermission(AddUserPermission.Command request)
        {
            return Ok(await this._mediator.Send(request));
        }

        [Route("password/prerenew")]
        [HttpPost]
        public async Task<IActionResult> PasswordPreRenew(PasswordPreRenew.Command request)
        {
            return Ok(await this._mediator.Send(request));
        }

        [Route("password/renew")]
        [HttpPost]
        public async Task<IActionResult> PasswordRenew(PasswordRenew.Command request)
        {
            return Ok(await this._mediator.Send(request));
        }


        [Route("users")]
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]GetUsers.Query request)
        {
            return Ok(await this._mediator.Send(request));
        }
    }
}

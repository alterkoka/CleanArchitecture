using Application.Common.Interfaces;
using Application.DTOs.Account;
using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Commands.DeleteUser;
using Application.Features.Users.Commands.UpdateUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly IIdentityService _identityService;
        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        /// <summary>
        /// Authenticates user in system
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _identityService.AuthenticateAsync(request, GenerateIPAddress()));
        }
        /// <summary>
        /// Registers user in system
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">User created</response>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> RegisterAsync(CreateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Updates user in system
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Update(UpdateUserCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Deletes User in system
        /// </summary>
        /// <param name="id" example="00000000-0000-0000-0000-000000000000">The User id (Guid)</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteUserCommand { Id = id });

            return NoContent();
        }

        private string GenerateIPAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}

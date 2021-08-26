using AccessAllAgents.Logging;
using AccessAllAgents.MicroService.Common.Constants;
using AccessAllAgents.MicroService.Common.Containers;
using AccessAllAgents.MicroService.Common.Exceptions;
using AccessAllAgents.MicroService.Template;
using AccessAllAgents.MicroService.Users.Services.Interfaces;
using AccessAllAgents.MicroService.Users.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AccessAllAgents.MicroService.Users.Controllers
{
    [ApiController]
    [Route("api/v1/authentication")]
    [Produces("application/json")]
    public class AuthenticationController : MicroServiceController
    {
        private static readonly ILog Log = LogManager.GetLogger<AuthenticationController>();

        private readonly IUserAuthenticationService _userAuthenticationService;

        public AuthenticationController(IUserAuthenticationService userAuthenticationService, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _userAuthenticationService = userAuthenticationService;
        }

        [HttpPost]
        [Route("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateViewModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Password))
                {
                    return BadRequest(new {message = "Token is invalid and password is missing from the request"});
                }

                UserInformation userInformation = await _userAuthenticationService.AuthenticateAsync(model.EmailAddress, model.Password);
                if (userInformation == null)
                {
                    throw new ControllerException(ErrorCodes.UserNotAuthenticated, "Username or password is incorrect");
                }

                return Ok(userInformation);
            }
            catch (Exception ex)
            {
                Log.Error("Unable to authenticate user", ex);
                throw;
            }
        }

        [HttpPost]
        [Route("refreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenViewModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.AccessToken) || string.IsNullOrEmpty(model.RefreshToken))
                {
                    return Unauthorized(new { message = "Token is invalid" });
                }

                UserInformation userInformation = await _userAuthenticationService.RefreshAsync(model.AccessToken, model.RefreshToken);
                if (userInformation == null)
                {
                    throw new ControllerException(ErrorCodes.UserNotAuthenticated, "Token is not valid.");
                }

                return Ok(userInformation);
            }
            catch (Exception ex)
            {
                Log.Error("Unable to authenticate user", ex);
                throw;
            }
        }
    }
}
using AccessAllAgents.MicroService.Common.Constants;
using AccessAllAgents.MicroService.Common.Containers;
using AccessAllAgents.MicroService.Common.Exceptions;
using AccessAllAgents.MicroService.Common.Extensions;
using AccessAllAgents.MicroService.Template.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Security.Claims;

namespace AccessAllAgents.MicroService.Template
{
    public abstract class MicroServiceController : ControllerBase
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;

        protected MicroServiceController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserInformation GetUserInformation()
        {
            // var claims = _httpContextAccessor.HttpContext.User.Claims;
            if (User.Identity is ClaimsIdentity claimsIdentity)
            {
                string emailAddress = null;
                string loginType = null;
                Guid? userId = null;
                bool isAccountSuspended = false;
                bool isProfileComplete = false;
                bool isAccountBeingReset = false;

                foreach (var claim in claimsIdentity.Claims)
                {
                    switch (claim.Type)
                    {
                        case MicroServiceClaimTypes.UserId:
                            userId = Guid.Parse(claim.Value);
                            break;
                        case MicroServiceClaimTypes.LoginType:
                            loginType = claim.Value;
                            break;
                        case MicroServiceClaimTypes.EmailAddress:
                            emailAddress = claim.Value;
                            break;
                        case MicroServiceClaimTypes.IsAccountSuspended:
                            isAccountSuspended = bool.Parse(claim.Value);
                            break;
                        case MicroServiceClaimTypes.IsProfileComplete:
                            isProfileComplete = bool.Parse(claim.Value);
                            break;
                        case MicroServiceClaimTypes.IsAccountBeingReset:
                            isAccountBeingReset = bool.Parse(claim.Value);
                            break;
                    }
                }

                if (!userId.HasValue)
                {
                    throw new ControllerException((int) HttpStatusCode.Unauthorized, "You are not authorized to perform this action.");
                }

                return new UserInformation
                {
                    UserId = userId.Value,
                    LoginType = loginType,
                    EmailAddress = emailAddress,
                    IsAccountLocked = isAccountSuspended,
                    IsProfileComplete = isProfileComplete,
                    IsAccountBeingReset = isAccountBeingReset,
                };
            }

            throw new ControllerException((int)HttpStatusCode.Unauthorized, "You are not authorized to perform this action.");
        }

        public GenericResponseViewModel FailureResponse(ErrorCodes errorCode)
        {
            return new GenericResponseViewModel
            {
                ErrorCode = (int)errorCode,
                IsSuccessful = false,
                FailureReason = errorCode.ToFailureReason()
            };
        }

        public GenericResponseViewModel FailureResponse(ErrorCodes errorCode, string errorMessage)
        {
            return new GenericResponseViewModel
            {
                ErrorCode = (int)errorCode,
                IsSuccessful = false,
                FailureReason = errorMessage
            };
        }

        protected string GetAbsoluteUri()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            string port = request.Host.Port.HasValue ? request.Host.Port.Value.ToString() : "";
            return request.Scheme + "://" + request.Host.Host + ":" + port;
        }
    }
}
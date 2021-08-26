using AccessAllAgents.Logging;
using AccessAllAgents.MicroService.Common.Constants;
using AccessAllAgents.MicroService.Common.Containers;
using AccessAllAgents.MicroService.Template;
using AccessAllAgents.MicroService.Users.Services.Containers;
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
    [Route("api/v1/user")]
    [Produces("application/json")]
    public class UserController : MicroServiceController
    {
        private static readonly ILog Log = LogManager.GetLogger<UserController>();

        private readonly IUserService _userService;

        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        [Authorize(Policy = LoginTypes.Everyone)]
        public async Task<LoginUserResponseViewModel> LoginUser()
        {
            UserInformation userInformation = GetUserInformation();

            try
            {
                LoginContainer loginContainer = await _userService.LoginUser(userInformation.UserId);
                return new LoginUserResponseViewModel
                {
                    IsSuccessful = true,
                    UserId = loginContainer.UserId,
                    IsProfileComplete = loginContainer.IsProfileComplete,
                    IsAccountLocked = loginContainer.IsAccountLocked,
                    IsAccountSuspended = loginContainer.IsAccountSuspended,
                    IsAccountBeingReset = loginContainer.IsAccountBeingReset,
                    LoginType = loginContainer.LoginType,
                    IsFirstTime = loginContainer.IsFirstTime,
                    IsPhoneNumberSet = loginContainer.IsPhoneNumberSet,
                    IsFullNameSet = loginContainer.IsFullNameSet,
                    UserProfile = new GetUserProfileResponseViewModel
                    {
                        AgencyName = loginContainer.Profile.AgencyName,
                        FullName = loginContainer.Profile.FullName,
                        PhoneNumber = loginContainer.Profile.PhoneNumber,
                        PostCode = loginContainer.Profile.PostCode,
                        Address = loginContainer.Profile.Address,
                        City = loginContainer.Profile.City,
                        Country = loginContainer.Profile.Country,
                        LogoUrl = loginContainer.Profile.LogoUrl,
                        LoginType = loginContainer.Profile.LoginType,
                        IsSuccessful = true
                    }
                };
            }
            catch (Exception ex)
            {
                if (userInformation != null) Log.Error("Exception when trying to login user. " + userInformation.UserId, ex);
                throw;
            }
        }

        [HttpPost]
        [Route("changePassword")]
        [Authorize(Policy = LoginTypes.Everyone)]
        public async Task<ChangePasswordResponseViewModel> ChangePassword(ChangePasswordRequestViewModel request)
        {
            UserInformation userInformation = GetUserInformation();

            try
            {
                string emailAddress = await _userService.TryChangePassword(userInformation.UserId, request.OldPassword, request.NewPassword);
                return new ChangePasswordResponseViewModel
                {
                    ErrorCode = (int)ErrorCodes.None,
                    IsSuccessful = true,
                    FailureReason = "",
                    EmailAddress = emailAddress
                };
            }
            catch (Exception ex)
            {
                Log.Error($"Unknown exception occured when trying to complete reset password for {userInformation.UserId}", ex);
                throw;
            }
        }
    }
}
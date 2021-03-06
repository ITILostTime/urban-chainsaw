using System.Collections.Generic;
using System.Linq;
using LostTimeDB;
using LostTimeWeb.WebApp.Authentication;
using LostTimeWeb.WebApp.Models.ManagerAccountViewModel;
using LostTimeWeb.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LostTimeWeb.WebApp.Controllers
{
    [Route( "api/[controller]" )]
    [Authorize( ActiveAuthenticationSchemes = JwtBearerAuthentication.AuthenticationScheme )]
    public class UserProfileController : Controller
    {
        readonly UserProfileService _userProfileService;
    
        public UserProfileController(UserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserByIdForDisplay(int id)
        {
            Result<UserAccount> result  = _userProfileService.Display(id);
            return this.CreateResult<UserAccount, UserForDisplayViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToUserForDisplayViewModel();
            } );
        }

        [HttpPut( "{id}" )]
        //[Authorize(Policy = "Permission")]
        public IActionResult Edit( [FromBody] UserEditViewModel model )
        {
            Console.WriteLine(model);
            Result<UserAccount> result = _userProfileService.Edit(
                model.UserID, 
                model.UserPseudonym,
                model.UserEmail, 
                model.UserControlPassword);
            return this.CreateResult<UserAccount, UserViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToUserViewModel();
            } );
        }
        [HttpPut("editpassword")]
        //[Authorize(Policy = "Permission")]
        public IActionResult EditPassword( [FromBody] EditPasswordViewModel model )
        {
            Result<UserAccount> result = _userProfileService.EditPassword(
                model.UserEmail, 
                model.UserOldPassword,
                model.UserNewPassword);
            return this.CreateResult<UserAccount, UserViewModel>( result, o =>
            {
                o.ToViewModel = s => s.ToUserViewModel();
            } );
        }

        [HttpPost("delete")]
        //[Authorize(Policy = "Permission")]
        public IActionResult Delete( [FromBody] UserDeleteViewModel model  )
        {
            Result<int> result = _userProfileService.Delete( 
                model.UserID, 
                model.UserEmail, 
                model.UserConfirmPassword );
            return this.CreateResult( result );
        }
    }
}
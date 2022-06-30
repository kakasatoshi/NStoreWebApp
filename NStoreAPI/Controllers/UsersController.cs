using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Auth;
using Service.Common;
using Service.Interfaces;
using Service.Models;
using Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Service.Auth.JwtAuthManager;

namespace NStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _iUser;
        private readonly IJwtAuthManager _jwtAuthManager;

        public UsersController(IUser iUser, IJwtAuthManager jwtAuthManager)
        {
            _iUser = iUser;
            _jwtAuthManager = jwtAuthManager;
        }

        [HttpPost("Login")]
        public UserModel.Output.UserInfo Login(UserModel.Input.LoginInfo input)
        {
            UserModel.Output.UserInfo userinfo = new();
            var user = _iUser.Login(input.UserName, input.Password);
            if (user != null)
            {
                Utilities.PropertyCopier<User, UserModel.Output.UserInfo>.Copy(user, userinfo);
                var userInfo = new UserInfo
                {
                    Id = user.Id,
                    Name = user.Name,
                    Username = user.Identification,
                    UserAgent = HttpContext.Request.Headers["User-Agent"].ToString()
                };
                userinfo.AccessToken = _jwtAuthManager.CreateToken(userInfo);
            }
            return userinfo;
        }

        [Authorize]
        [HttpPost("Logout")]
        public bool Logout(string input)
        {
            try
            {
                _jwtAuthManager.RemoveTokenByUserName(input);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NStore.Common;
using NStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NStore.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            LoginModel model = new();
            return View(model);
        }

        [HttpPost]
        public IActionResult Login(string username, string password, bool remember = false, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            if (username == null) username = "";
            if (password == null) password = "";

            var input = new UserModel.Input.LoginInfo { UserName = username, Password = password };
            var user = Utilities.SendDataRequest<UserModel.Output.UserInfo>(ConstantValues.User.Login, input);
            HttpContext.Session.Remove("NhanVien");
            if (user != null)
            {
                if (user.Id > 0)
                {
                    bool logined = LoginUser(user, remember);
                    if (logined)
                        HttpContext.Session.Set<UserModel.Output.UserInfo>("NhanVien", user);
                    if (!string.IsNullOrEmpty(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                    TempData["LoginNoteti"] = user.Noteti;
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            //var user = HttpContext.Session.Get<UserModel.Output.UserInfo>("NhanVien");
            //var input = new UserModel.Input.LoginInfo { UserName = user.Identification, Password = user.Password };
            var input = @User.Claims.FirstOrDefault(x => x.Type == "HOTEN").Value;
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            Utilities.SendDataRequest<bool>(ConstantValues.User.Logout, input);
            return RedirectToAction("Login", "Account");
        }

        private bool LoginUser(UserModel.Output.UserInfo user, bool remember)
        {
            try
            {
                var userClaims = new List<Claim>() {
                    new Claim("NHANVIENID", user.Id.ToString()),
                    new Claim("USERNAME", user.Identification),
                    new Claim("QUYENHAN", user.Role.ToUpper()),
                    new Claim("HOTEN", user.Name),
                    new Claim("CMND", user.Identification)
                };
                var claimsIdentity = new ClaimsIdentity(userClaims,
                                            CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimsIdentity);
                var properties = new AuthenticationProperties { IsPersistent = remember };
                HttpContext.SignInAsync(principal, properties);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
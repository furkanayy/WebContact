using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Presentation.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model.Concrete;
using Microsoft.AspNetCore.Authorization;
using Entity.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Principal;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Route("Admin")]
    public class AdminController : BaseController
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(UserLoginDto model)
        {
            if (!ModelState.IsValid) return View();

            try
            {
                if (UnitOfWork.UserManager.LoginControl(model))
                {
                    var User = UnitOfWork.UserManager.Get(p => p.UserName == model.UserName);
                    var userClaims = new List<Claim>()
                {
                    new Claim("Id", User.Id.ToString()),
                    new Claim("Username", User.UserName ?? ""),
                    new Claim("FirstName", User.FirstName ?? ""),
                    new Claim("LastName", User.LastName ?? ""),
                    new Claim("Phonenumber", User.PhoneNumber?.ToString() ?? ""),
                    new Claim("Email", User.Email?.ToString() ?? ""),
                    new Claim("Photo", User.Photo ?? ""),
                    new Claim("IsApproved", User.IsApproved.ToString()),
                    new Claim("RoleId", User.RoleId.ToString()),
                    new Claim("RoleName", User.Role?.RoleName ?? ""),
                    new Claim(ClaimTypes.Role, User.Role?.RoleName ?? "")
                };
                    var identity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);
                    var authProperties = new AuthenticationProperties()
                    {
                        ExpiresUtc = DateTime.UtcNow.AddDays(14),
                        AllowRefresh = false,
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

                    return RedirectToAction("Home");
                }
                else
                    return View();
            }

            catch (Exception)
            {

                return View();
            }
        }


        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UserRegisterDto model)
        {
            try
            {
                UnitOfWork.UserManager.AddUser(model);
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View();
            }
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin, Admin, User")]
        [Route("Home")]
        public IActionResult Home()
        {
            return View();
        }

        [HttpGet]
        [Route("Profile")]
        public IActionResult Profile()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            var model = UnitOfWork.UserManager.GetProfile(p => p.Id == int.Parse(userId));
            return View(model);

        }

        [HttpPost]
        [Route("Profile")]
        public IActionResult Profile(ProfileDto model)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            model.Id = int.Parse(userId);
            UnitOfWork.UserManager.UpdateUser(model);
            return View();
        }

        [HttpGet]
        [Route("ContactsMenu")]
        public IActionResult ContactsMenu()
        {
            return View();

        }

        //[HttpPost]
        //[Route("ContactsMenu")]
        //public IActionResult ContactsMenu()
        //{
        //    return View();

        //}

    }
}

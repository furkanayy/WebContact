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
                    new Claim("Role", User.Role?.RoleName ?? ""),
                    new Claim(ClaimTypes.Role, User.Role?.RoleName ?? "")
                };
                var grandmaIdentity = new ClaimsIdentity(userClaims, "Login");
                var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
                var authProperties = new AuthenticationProperties()
                {
                    ExpiresUtc = DateTime.UtcNow.AddYears(10),
                    AllowRefresh = false,
                };
                await HttpContext.SignInAsync(userPrincipal, authProperties);
                if (User.RoleId == 1)
                {
                    return RedirectToAction("Home");
                }
                else if (User.RoleId == 2)
                {
                    return RedirectToAction("Home");
                }
                else
                    return RedirectToAction("Home");
            }
            else
                return View("Login");
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
        [Route("Home")]
        public IActionResult Home()
        {
            return View();
        }

    }
}

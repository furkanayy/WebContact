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
        public IActionResult Login(UserLoginDto model)
        {
            if (UnitOfWork.UserManager.LoginControl(model))
                return View("Home");
            else
                return View();
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

    }
}

using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PhoneGuideUI.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        UserManager userManager = new UserManager(new EfUserDal());
        public ActionResult Index()
        {
            var uservalues = userManager.GetList();
            _logger.LogInformation("Index page says hello");
            return View(uservalues);
        }
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddUser(User u)
        {
            
                userManager.UserAdd(u);
                _logger.LogInformation("User added");
            return RedirectToAction("Login","Login");
        }
    }
}
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
using DataAccess.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        ContactManager contactManager = new ContactManager(new EfContactDal());
        RoleManager roleManager = new RoleManager(new EfRoleDal());
        Context c = new Context();
        
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
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var uservalues = userManager.GetList();
            _logger.LogInformation("Admin Index page says hello");
            return View(uservalues);
        }
        public ActionResult DeleteRole(int id)
        {
            ViewBag.Success = TempData["Success"] as bool?;
            return View();
        }
        [HttpPost, ActionName("DeleteRole")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var uservalue = userManager.GetById(id);
            userManager.UserDelete(uservalue);
            _logger.LogInformation("Delete confirmed");
            TempData["Deleted1"] = true;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditRole(int id)
        {
            var uservalue = userManager.GetById(id);
            ViewBag.Success = TempData["Success"] as bool?;
            List<SelectListItem> valuecategory = (from x in roleManager.GetList()
                                                  select new SelectListItem
                                                  {
                                                      
                                                      Text = x.RoleName,
                                                      Value = x.RoleId.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            return View(uservalue);
        }
        [HttpPost]
        public ActionResult EditRole(User u)
        {

            userManager.UserUpdate(u);
            _logger.LogInformation("User updated");
            TempData["Edited1"] = true;
            return RedirectToAction("Index");
        }
        public ActionResult ListUserContact(int id)
        {
            var contactvalues = contactManager.GetListByUser(id);
            _logger.LogInformation("User contacts");
            return View(contactvalues);
        }
    }
}
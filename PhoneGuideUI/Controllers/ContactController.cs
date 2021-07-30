using System.Diagnostics.Eventing.Reader;
using System.Linq;
using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PhoneGuideUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;
        }
        // GET: Contact
        ContactManager cm = new ContactManager(new EfContactDal());
        Context c = new Context();
        [Authorize(Roles = "Admin,DelAddUpd,AddUpd,DelAdd,DelUpd")]
        
        public ActionResult Index()
        {
           string mail= HttpContext.Session.GetString("UserSession");
           var useridinfo = c.Users.Where(x => x.UserMail == mail).Select(y => y.UserId).FirstOrDefault();
           var values = cm.GetListByUser(useridinfo);
           _logger.LogInformation("Index page says hello");
            return View(values);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,DelAddUpd,AddUpd,DelAdd")]
        public ActionResult AddContact()
        {
            ViewBag.Success = TempData["Success"] as bool?;
            return View();
        }
        [HttpPost]
        public ActionResult AddContact(Contact con)
        {
            string mail = HttpContext.Session.GetString("UserSession");
            var useridinfo = c.Users.Where(x => x.UserMail == mail).Select(y => y.UserId).FirstOrDefault();
            con.UserId = useridinfo;
            cm.ContactAdd(con);
            _logger.LogInformation("Add Contact");
            TempData["Added"] = true;
            return RedirectToAction("Index");
            }
        [Authorize(Roles = "Admin,DelAddUpd,DelAdd,DelUpd")]
        public ActionResult DeleteContact(int id)
        {
            ViewBag.Success = TempData["Success"] as bool?;
            return View();
        }
        [HttpPost, ActionName("DeleteContact")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            var contactvalue = cm.GetById(id);
            cm.ContactDelete(contactvalue);
            _logger.LogInformation("Delete confirmed");
            TempData["Deleted"] = true;
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "Admin,DelAddUpd,AddUpd,DelUpd")]
        public ActionResult EditContact(int id)
        {
            var contactvalue = cm.GetById(id);
            ViewBag.Success = TempData["Success"] as bool?;
            return View(contactvalue);
        }
        [HttpPost]
        public ActionResult EditContact(Contact con)
        {
            string mail = HttpContext.Session.GetString("UserSession");
            string role = HttpContext.Session.GetString("AdminSession");
            if (role == null)
            {
                var useridinfo = c.Users.Where(x => x.UserMail == mail).Select(y => y.UserId).FirstOrDefault();
                con.UserId = useridinfo;
                cm.ContactUpdate(con);
                _logger.LogInformation("Contact updated");
                TempData["Edited"] = true;
                return RedirectToAction("Index");
            }
            else {
                cm.ContactUpdate(con);
                _logger.LogInformation("Contact updated");
                TempData["Edited2"] = true;
                return RedirectToAction("Index", "User");
            }
        }

    }
}
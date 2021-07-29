using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Concrete;
using DataAccess.Concrete;
using DataAccess.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PhoneGuideUI.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }
        UserLoginManager userLoginManager = new UserLoginManager(new EfUserDal());
        Context c = new Context();
        
        [HttpGet]
        public ActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(User u)
        {
            var infos = c.Users.FirstOrDefault(x => x.UserMail == u.UserMail && x.UserPassword == u.UserPassword);
            if (infos != null)
            {
                switch (infos.RoleId)
                {
                    case 0:
                        var claims0 = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,u.UserMail),
                    new Claim(ClaimTypes.Role,"DelAddUpd") };
                        var useridentity0 = new ClaimsIdentity(claims0, "Login");
                        ClaimsPrincipal claimsPrincipal0 = new ClaimsPrincipal(useridentity0);
                        await HttpContext.SignInAsync(claimsPrincipal0);
                        HttpContext.Session.SetString("UserSession", u.UserMail);
                        _logger.LogInformation("DelAddUpd login successful");
                        return RedirectToAction("Index", "Contact");
                    case 1:
                        var claims1 = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,u.UserMail),
                    new Claim(ClaimTypes.Role,"AddUpd") };
                        var useridentity1 = new ClaimsIdentity(claims1, "Login");
                        ClaimsPrincipal claimsPrincipal1 = new ClaimsPrincipal(useridentity1);
                        await HttpContext.SignInAsync(claimsPrincipal1);
                        HttpContext.Session.SetString("UserSession", u.UserMail);
                        _logger.LogInformation("AddUpd login successful");
                        return RedirectToAction("Index", "Contact");
                    case 2:
                        var claims2 = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,u.UserMail),
                    new Claim(ClaimTypes.Role,"DelAdd") };
                        var useridentity2 = new ClaimsIdentity(claims2, "Login");
                        ClaimsPrincipal claimsPrincipal2 = new ClaimsPrincipal(useridentity2);
                        await HttpContext.SignInAsync(claimsPrincipal2);
                        HttpContext.Session.SetString("UserSession", u.UserMail);
                        _logger.LogInformation("DelAdd login successful");
                        return RedirectToAction("Index", "Contact");
                    case 3:
                        var claims3 = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,u.UserMail),
                    new Claim(ClaimTypes.Role,"DelUpd") };
                        var useridentity3 = new ClaimsIdentity(claims3, "Login");
                        ClaimsPrincipal claimsPrincipal3 = new ClaimsPrincipal(useridentity3);
                        await HttpContext.SignInAsync(claimsPrincipal3);
                        HttpContext.Session.SetString("UserSession", u.UserMail);
                        _logger.LogInformation("DelUpd login successful");
                        return RedirectToAction("Index", "Contact");
                    case 4:
                        var claims4 = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,u.UserMail),
                    new Claim(ClaimTypes.Role,"Unauthorized") };
                        var useridentity4 = new ClaimsIdentity(claims4, "Login");
                        ClaimsPrincipal claimsPrincipal4 = new ClaimsPrincipal(useridentity4);
                        await HttpContext.SignInAsync(claimsPrincipal4);
                        HttpContext.Session.SetString("UserSession", u.UserMail);
                        _logger.LogInformation("Unauthorized login successful");
                        return RedirectToAction("Index", "Contact");
                    case 99:
                        var claims99 = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,u.UserMail),
                    new Claim(ClaimTypes.Role,"Admin") };
                        var useridentity99 = new ClaimsIdentity(claims99, "Login");
                        ClaimsPrincipal claimsPrincipal99 = new ClaimsPrincipal(useridentity99);
                        await HttpContext.SignInAsync(claimsPrincipal99);
                        HttpContext.Session.SetString("UserSession", u.UserMail);
                        HttpContext.Session.SetString("AdminSession", u.RoleId.ToString());
                        _logger.LogInformation("Admin login successful");
                        TempData["Admin"] = true;
                        return RedirectToAction("Index", "Contact");
                }
                
            }
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _logger.LogInformation("Log Out");
            return RedirectToAction("Login");
        }
    }
}
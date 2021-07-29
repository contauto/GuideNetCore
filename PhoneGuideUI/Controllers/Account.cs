using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneGuideUI.Controllers
{
    public class Account : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

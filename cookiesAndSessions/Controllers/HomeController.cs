using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using cookiesAndSessions.Models;
using Microsoft.AspNetCore.Http;

namespace cookiesAndSessions.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (Request.Cookies["Colour"] == null)
            {
                ViewData["myColour"] = "red";
            }
            else
            {
                ViewData["myColour"] = Request.Cookies["Colour"].ToString();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormCollection form)
        {
            string newColour = form["pickedColour"].ToString();
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(10);
            Response.Cookies.Append("Colour", newColour, option);
            ViewData["myColour"] = newColour;
            return View();
        }

        public IActionResult TestCookie()
        {
            if (Request.Cookies["Colour"] == null)
            {
                ViewData["myColour"] = "red";
            }
            else
            {
                ViewData["myColour"] = Request.Cookies["Colour"].ToString();
            }
            return View();
        }

        public IActionResult DeleteCookies()
        {
            foreach (var cookie in HttpContext.Request.Cookies)
            {
                Response.Cookies.Delete("Colour");
            }
            return Redirect("/Home/Index");
        }

        public IActionResult SessionDemo()
        {
            if (HttpContext.Session.GetString("Name") == null)
            {
                ViewData["myName"] = "Not Set";
            }
            else
            {
                ViewData["myName"] = HttpContext.Session.GetString("Name"); ;
            }
            return View();
        }

        [HttpPost]
        public IActionResult SessionDemo(IFormCollection form)
        {
            string newName = form["myName"].ToString();
            HttpContext.Session.SetString("Name", newName);
            ViewData["myName"] = newName;
            return View();
        }

        public IActionResult TestSession()
        {
            if (HttpContext.Session.GetString("Name") == null)
            {
                ViewData["myName"] = "Not Set";
            }
            else
            {
                ViewData["myName"] = HttpContext.Session.GetString("Name"); ;
            }
            return View();
        }

        public IActionResult DeleteSession()
        {
            HttpContext.Session.Clear();
            return Redirect("/Home/SessionDemo");
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

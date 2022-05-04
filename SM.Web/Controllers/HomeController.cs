using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SM.Entity;
using SM.Repositories.IRepository;
using SM.Web.Data;
using SM.Web.Models;
using System;
using System.Diagnostics;

namespace SM.Web.Controllers
{
    [ResponseCache(CacheProfileName = "Default0")]
    public class HomeController : Controller
    {
        private readonly SchoolManagementContext _schoolManagementContext;
       
        public HomeController(SchoolManagementContext schoolManagementContext)
        {
            _schoolManagementContext = schoolManagementContext;
        }

        /// <summary>
        /// Main Dashboard when user is not logged in.
        /// </summary>
        [AllowAnonymous]
        #region Dashboard
        //[ResponseCache(CacheProfileName = "default")]
        //[ResponseCache(CacheProfileName = "PrivateCache")]
        public IActionResult Dashboard()
        {
            if (!Convert.ToBoolean(HttpContext.Session.GetString("Userlogeddin")))
            {
                return RedirectToAction("Login", "Auth");
            }
            else
            {
                return View();
            }
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

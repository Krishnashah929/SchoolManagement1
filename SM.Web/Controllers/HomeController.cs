using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SM.Web.Models;
using System;
using System.Diagnostics;

/// <summary>
/// Home controller for users.
/// </summary>
namespace SM.Web.Controllers
{
    /// <summary>
    /// Calling cache from startup.cs
    /// </summary>
    [ResponseCache(CacheProfileName = "Default0")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Main Dashboard when user is not logged in.
        /// </summary>
        [Authorize(Roles = "Admin")]
        #region Dashboard
        public IActionResult Dashboard()
        {
            try
            {
                if (User.Identity.IsAuthenticated == true)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Login", "Auth");
                }
            }
            catch (Exception)
            {
                return View("Error");
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

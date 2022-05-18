using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SM.Common;
using SM.Entity;
using SM.Models;
using SM.Repositories.IRepository;
using SM.Services.Users;
using SM.Web.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SM.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly SchoolManagementContext _schoolManagementContext;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;
        private IUserServices _userService;

        private bool errorflag;

        public object HttpCacheability { get; private set; }

        [Obsolete]
        public AuthController(SchoolManagementContext schoolManagementContext, IHostingEnvironment hostingEnvironment, 
            IUserRepository userRepository, IUserServices userService)
        {
            _schoolManagementContext = schoolManagementContext;
            _hostingEnvironment = hostingEnvironment;
            _userService = userService;
            //_userRepository = userRepository;
        }

        /// <summary>
        /// This action method is for getting login modal.
        /// </summary>
        #region Login
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            try
            {
                //if user is already logged in then they can't go back to login page
                if (User.Identity.IsAuthenticated == true)
                {
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        #endregion

        /// <summary>
        /// Login for user from this post login method.
        /// loginModel is a viewmodel and used for login form.
        /// object of LoginModel is objLoginModel.
        /// </summary>
        #region Login(POST)
        [HttpPost]
        public IActionResult Login(LoginModel objloginModel, User getUser)
        {
            try
            {
                ModelState.Remove("FirstName");
                ModelState.Remove("Lastname");
                ModelState.Remove("RetypePassword");
                if (ModelState.IsValid)
                {
                    var userPassword = EncryptionDecryption.Encrypt(objloginModel.Password.ToString());

                    //Check the user email and password
                    var loggedinUser = _userService.GetByEmail(getUser);
                    
                    //Here can be implemented checking logic from the database
                    if (loggedinUser != null)
                    {
                        var Name = loggedinUser.FirstName + " " + loggedinUser.Lastname;
                        HttpContext.Session.SetString("Userlogeddin", "true");
                        HttpContext.Session.SetString("Name", Name);

                        var userClaims = new List<Claim>()
                        {
                             new Claim("UserEmail", objloginModel.EmailAddress),
                             new Claim(ClaimTypes.Email, objloginModel.EmailAddress),
                             //new Claim(ClaimTypes.Role, objloginModel.Role.ToString())
                        };

                        var userIdentity = new ClaimsIdentity(userClaims, "User Identity");

                        var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                        HttpContext.SignInAsync(userPrincipal);

                        return RedirectToAction("Dashboard", "Home");
                    }
                    else
                    {
                        TempData["Error"] = CommonValidations.RecordNotExistsMsg;
                        return View();
                    }
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            return View();
        }
        #endregion

        /// <summary>
        ///  This action method is for getting Register modal.
        /// </summary>
        #region Register(GET)
        [HttpGet]
        public IActionResult Register()
        {
            try
            {
                //if user is already logged in then they can't go back to register page
                if (User.Identity.IsAuthenticated == true)
                {
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        #endregion

        /// <summary>
        /// Register for user from this post Register method.
        /// object of User is objUser.
        /// </summary>
        #region Register(POST)
        [HttpPost]
        [Obsolete]
        public IActionResult Register(User objUser, User user)
        {
            try
            {
                //Remove coloum name from validation ModelState.Remove 
                ModelState.Remove("Password");
                ModelState.Remove("RetypePassword");
                if (ModelState.IsValid)
                {
                    //calling from services
                    var registerUsers = _userService.Register(user);
                    if (registerUsers != null)
                    { 
                        //encrypt the userid for link in url.
                        var userId = EncryptionDecryption.Encrypt(user.UserId.ToString());
 
                        //link generation with userid.
                        var linkPath = "http://localhost:9334/Auth/SetPassword?link=" + userId;

                        string webRootPath = _hostingEnvironment.WebRootPath + "/MalTemplates/SetPasswordTemplate.html";
                        StreamReader reader = new StreamReader(webRootPath);
                        string readFile = reader.ReadToEnd();
                        string myString = string.Empty;
                        myString = readFile;
                        var subject = "Set Password";
                        //when you have to replace the content of html page
                        myString = myString.Replace("@@Name@@", objUser.FirstName);
                        myString = myString.Replace("@@FullName@@", objUser.FirstName + " " + objUser.Lastname);
                        myString = myString.Replace("@@Email@@", objUser.EmailAddress);
                        myString = myString.Replace("@@Link@@", linkPath);
                        var body = myString.ToString();

                        SendEmail(objUser.EmailAddress, body, subject);
                        return RedirectToAction(actionName: "Login");
                    }
                    else
                    {
                        TempData["Error"] = CommonValidations.RecordExistsMsg;
                        return View();
                    }
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            return View();
        }
        #endregion

        /// <summary>
        /// Open forgot password model for sendlink on entered e=mail
        /// </summary>
        #region GetForgotPasswordModel
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ForgotPasswordModel()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        #endregion

        /// <summary>
        /// Link method of sending mail for reset password. 
        /// </summary>
        #region Sendlink
        [HttpPost]
        public IActionResult Sendlink(User objUser)
        {
            var user = _schoolManagementContext.Users.Where(x => x.EmailAddress == objUser.EmailAddress && x.IsActive == true).ToList();
            ModelState.Clear();
            //forgot password code
            String ResetCode = Guid.NewGuid().ToString();

            var uriBuilder = new UriBuilder
            {
                Scheme = Request.Scheme,
                Host = Request.Host.Host,
                Port = Request.Host.Port ?? -1, //bydefault -1
                Path = $"/Auth/ForgotPassword/{ResetCode}"
            };
            var link = uriBuilder.Uri.AbsoluteUri;

            var getUser = (from s in _schoolManagementContext.Users where s.EmailAddress == objUser.EmailAddress select s).FirstOrDefault();
            if (getUser != null)
            {
                getUser.ResetPasswordCode = ResetCode;
                _schoolManagementContext.SaveChanges();

                var subject = "Password Reset Request";
                var body = "Hi " + getUser.FirstName + ", <br/> You recently requested to reset the password for your account. Click the link below to reset ." +
                 "<br/> <br/><a href='" + link + "'>" + link + "</a> <br/> <br/>" +
                "If you did not request for reset password please ignore this mail.";

                SendEmail(getUser.EmailAddress, body, subject);

                TempData["linkSendMsg"] = CommonValidations.LinkSendMsg;
                return RedirectToAction("ForgotPasswordModel", "Auth");
            }
            else
            {
                TempData["WrongMailMsg"] = CommonValidations.WrongMailMsg;
                return RedirectToAction("ForgotPasswordModel", "Auth");
            }
        }
        #endregion

        /// <summary>
        ///  In this method we are sending main set password template to user. where they can set their new password.
        /// </summary>
        #region SendEmail
        private void SendEmail(string email, string body, string subject)
        {
            try
            {
                using (MailMessage mm = new MailMessage("krishnaa9121@gmail.com", email))
                {
                    mm.Subject = subject;
                    mm.Body = body;
                    mm.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential("krishnaa9121@gmail.com", "Kri$hn@91");
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                    }
                }
            }
            catch (Exception)
            {
                errorflag = true;
            }
        }
        #endregion

        /// <summary>
        /// Reset Password get method.
        /// </summary>
        #region ForgotPassword
        public IActionResult ForgotPassword(String id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return NotFound();
                }
                var user = _userService.GetResetPassword(id);
                if (user != null)
                {
                    ForgotPassword modal = new ForgotPassword();
                    modal.ResetCode = id;
                    return View(modal);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        #endregion

        /// <summary>
        /// Reset Password post method.
        /// </summary>
        #region ForgotPassword(POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPassword model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _userService.ResetPassword(model);
                    if (user != null)
                    {
                        TempData["successMsg"] = CommonValidations.PasswordUpdateMsg;
                    }
                }
                else
                {
                    TempData["failureMsg"] = CommonValidations.PasswordNotUpdateMsg;
                }
                return View(model);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        #endregion

        /// <summary>
        ///  Set password method will call when user set their password from email-template
        /// </summary>
        #region SetPasswordGet
        [HttpGet]
        public IActionResult SetPassword(string link, User user)
        {
            try
            {
                link = EncryptionDecryption.Decrypt(link.ToString());
                int id = Convert.ToInt32(link);
                HttpContext.Session.SetInt32("links", id);
                user.UserId = id;
                var userDetails = _userService.GetUserPassword(user);
                return View(userDetails);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        #endregion

        /// <summary>
        ///  Set password Post method will call when user enter both their password and click to set password.
        /// </summary>
        #region SetPasswordPost
        [HttpPost]
        public IActionResult SetPassword(User objUser, User user)
        {
            try
            {
                string message = string.Empty;
                ModelState.Remove("FirstName");
                ModelState.Remove("Lastname");
                ModelState.Remove("EmailAddress");
                if (ModelState.IsValid)
                {
                    int id = (int)HttpContext.Session.GetInt32("links");
                    user.UserId = id;
                    var setUserPassword = _userService.SetUserPassword(user);
                    if (setUserPassword != null)
                    {
                        return RedirectToAction("Login", "Auth");
                    }
                    else
                    {
                        message = CommonValidations.RecordExistsMsg;
                        return Content(message);
                    }
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            return View();
        }
        #endregion
       
        /// <summary>
        /// Access denined method.
        /// if user is unauthenticate then will reirect to this method.
        /// </summary>
        #region Accessdenined
        public ActionResult Error()
        {
            return View();
        }
        #endregion

        /// <summary>
        /// When user logout from their account.
        /// Set session "Userlogeddin" as false when user logged out from their session.
        /// After logged out session will be clear and user will be redirect to Login.
        /// </summary>
        #region LogOut
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                HttpContext.Session.SetString("Userlogeddin", "false");
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        #endregion
    }
}

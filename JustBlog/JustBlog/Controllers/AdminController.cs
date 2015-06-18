using JustBlog.Models;
using JustBlog.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace JustBlog.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        readonly IAuthProvider _authProvider;

        public AdminController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (_authProvider.IsLoggedIn)
            {               
                return RedirectToUrl(returnUrl);
            }            

            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        private ActionResult RedirectToUrl(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Manage");
        }

        public ActionResult Manage()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && _authProvider.Login(model.UserName, model.Password))
            {
                return RedirectToUrl(returnUrl);
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View();
        }

        public ActionResult Logout()
        {
            _authProvider.Logout();

            return RedirectToAction("Login", "Admin");
        }
    }
}
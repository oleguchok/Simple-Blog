using JustBlog.Core.Repository;
using JustBlog.Models;
using JustBlog.Providers;
using Newtonsoft.Json;
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
        readonly IBlogRepository _blogRepository;

        public AdminController(IAuthProvider authProvider, IBlogRepository blogRepository = null)
        {
            _authProvider = authProvider;
            _blogRepository = blogRepository;
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

        public ContentResult Posts(JqInViewModel jqParams)
        {
            var posts = _blogRepository.Posts(jqParams.page - 1, jqParams.rows,
                jqParams.sidx, jqParams.sord == "asc");

            var totalPosts = _blogRepository.TotalPosts(false);

            return Content(JsonConvert.SerializeObject(new
                {
                    page = jqParams.page,
                    records = totalPosts,
                    rows = posts,
                    total = Math.Ceiling(Convert.ToDouble(totalPosts) / jqParams.rows)
                }, new CustomDateTimeConverter()), "application/json");
        }
    }
}
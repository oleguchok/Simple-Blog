using JustBlog.Core.Repository;
using JustBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JustBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public ViewResult Posts(int p = 1)
        {
            // pick latest 10 posts
            var listViewModel = new ListViewModel(_blogRepository,p);

            ViewBag.Title = "Latest Posts";

            return View("List", listViewModel);
        }
    }
}
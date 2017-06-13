using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cb.DAL.Contexts;
using cb.DAL.Repositories;
using cb.Models;

namespace cb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            PostSqlContext psc = new PostSqlContext();
            PostRepository pr = new PostRepository(psc);

                List<Post> posts = new List<Post>();
                posts = pr.GetAllPost();
                ViewBag.posts = posts;

            return View();
        }

        public ActionResult Fresh()
        {
            PostSqlContext psc = new PostSqlContext();
            PostRepository pr = new PostRepository(psc);

            List<Post> posts = new List<Post>();
            posts = pr.GetFreshPosts();
            ViewBag.posts = posts;

            return View("~/Views/Home/Index.cshtml");
        }

        public ActionResult Hot()
        {
            PostSqlContext psc = new PostSqlContext();
            PostRepository pr = new PostRepository(psc);

            List<Post> posts = new List<Post>();
            posts = pr.GetHotPosts();
            ViewBag.posts = posts;

            return View("~/Views/Home/Index.cshtml");
        }

        public ActionResult Trending()
        {
            PostSqlContext psc = new PostSqlContext();
            PostRepository pr = new PostRepository(psc);

            List<Post> posts = new List<Post>();
            posts = pr.GetAllPost();

            if (Session["LoggedInUser"] != null)
            {
                int userid = Convert.ToInt32(Session["LoggedInUser"]);
                posts = pr.GetTrendingPosts(userid);
            }

            ViewBag.posts = posts;


            return View("~/Views/Home/Index.cshtml");
        }
    }
}
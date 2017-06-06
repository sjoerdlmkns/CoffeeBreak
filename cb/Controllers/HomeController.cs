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

            List<Post> posts = pr.GetAllPost();
            ViewBag.posts = posts;

            return View();
        }

        public ActionResult About()
        {
            

            return View();
        }

        public ActionResult Contact()
        {
           

            return View();
        }
    }
}
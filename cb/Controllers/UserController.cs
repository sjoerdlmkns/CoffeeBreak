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
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserProfile(int id)
        {
            UserSqlContext ucontext = new UserSqlContext();
            UserRepository urepo = new UserRepository(ucontext);

            User user = urepo.GetGebruikerById(id);

            ViewBag.user = user;

            //if (Session["LoggedInUser"] != null)
            //{
            //    UserSqlContext ucontext = new UserSqlContext();
            //    UserRepository urepo = new UserRepository(ucontext);

            //    User user = urepo.GetGebruikerById(id);

            //    ViewBag.user = user;
            //}
            //else
            //{
            //    ViewBag.user = null;
            //}

            return View("~/Views/User/UserProfile.cshtml");
        }
    }
}
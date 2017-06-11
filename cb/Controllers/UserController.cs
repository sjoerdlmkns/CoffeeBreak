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

            PostSqlContext pcontext = new PostSqlContext();
            PostRepository prepo = new PostRepository(pcontext);

            List<Post> posts = prepo.GetPostsByUserId(user.Id);

            ViewBag.posts = posts;

            ViewBag.TotalScore = posts.Sum(post => post.Score);

            return View("~/Views/User/UserProfile.cshtml");
        }

        [HttpPost]
        public ActionResult ChangePassword(FormCollection form)
        {
            UserSqlContext ucontext = new UserSqlContext();
            UserRepository urepo = new UserRepository(ucontext);

            urepo.ChangeUserPasswordById(Convert.ToInt32(Session["LoggedInUser"]), form["newpassword"]);

            return RedirectToAction("UserProfile", new { id = Convert.ToInt32(Session["LoggedInUser"]) });
        }

        [HttpGet]
        public ActionResult SendFriendRequest(int id)
        {
            UserSqlContext ucontext = new UserSqlContext();
            UserRepository urepo = new UserRepository(ucontext);

            urepo.SendFriendRequest(Convert.ToInt32(Session["LoggedInUser"]), id);

            return View("~/Views/User/UserProfile.cshtml");
        }

        [HttpGet]
        public ActionResult AcceptFriendRequest(int id)
        {
            UserSqlContext ucontext = new UserSqlContext();
            UserRepository urepo = new UserRepository(ucontext);

            urepo.AcceptFriendRequest(id, Convert.ToInt32(Session["LoggedInUser"]));

            return View("~/Views/User/UserProfile.cshtml");
        }
    }
}
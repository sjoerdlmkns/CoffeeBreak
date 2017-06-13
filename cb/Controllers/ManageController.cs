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
    public class ManageController : Controller
    {
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Overview()
        {
            UserSqlContext ucontext = new UserSqlContext();
            UserRepository ur = new UserRepository(ucontext);

            User currentuser = ur.GetGebruikerById(Convert.ToInt32(Session["LoggedInUser"]));

            if (currentuser.IsManager != true)
            {
                return RedirectToAction("Index", "Home");
            }


                MessageSqlContext mcontext = new MessageSqlContext();
                MessageRepository mr = new MessageRepository(mcontext);

                List<string> badWordList = mr.GetAllBadWords();
                ViewBag.badWords = badWordList;



                List<User> userlist = ur.GetAllUsers();
                ViewBag.users = userlist;


                PostSqlContext pcontext = new PostSqlContext();
                PostRepository pr = new PostRepository(pcontext);

                List<Post> postlist = pr.GetAllPost();
                ViewBag.posts = postlist;

                return View("~/Views/Manage/Overview.cshtml");
        }

        [HttpPost]
        public ActionResult OverviewAddBadWord(FormCollection form)
        {
            MessageSqlContext context = new MessageSqlContext();
            MessageRepository mr = new MessageRepository(context);

            mr.AddBadWord(form["word"]);

            return Overview();
        }

        [HttpGet]
        public ActionResult OverviewRemoveBadWord(string id)
        {
            MessageSqlContext context = new MessageSqlContext();
            MessageRepository mr = new MessageRepository(context);

            mr.RemoveBadWord(id);

            return RedirectToAction("Overview", "Manage");
        }
    }
}
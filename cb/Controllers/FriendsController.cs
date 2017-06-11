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
    public class FriendsController : Controller
    {
        // GET: Friends
        public ActionResult Index()
        {
            UserSqlContext ucontext = new UserSqlContext();
            UserRepository ur = new UserRepository(ucontext);

            List<User> friendrequests = ur.GetFriendRequestsByUserId(Convert.ToInt32(Session["LoggedInUser"]));
            ViewBag.friendrequests = friendrequests;

            List<User> friends = ur.GetFriendsByUserId(Convert.ToInt32(Session["LoggedInUser"]));
            ViewBag.friends = friends;

            return View();
        }
    }
}
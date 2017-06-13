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
    public class MessageController : Controller
    {
        // GET: Message
        public ActionResult Index()
        {
            UserSqlContext ucontext = new UserSqlContext();
            UserRepository ur = new UserRepository(ucontext);

            List<User> friends = ur.GetFriendsByUserId(Convert.ToInt32(Session["LoggedInUser"]));
            ViewBag.friends = friends;


            return View();
        }

        public ActionResult OpenChat(int id)
        {
            UserSqlContext ucontext = new UserSqlContext();
            UserRepository ur = new UserRepository(ucontext);

            List<User> friends = ur.GetFriendsByUserId(Convert.ToInt32(Session["LoggedInUser"]));
            ViewBag.friends = friends;

            MessageSqlContext mcontext = new MessageSqlContext();
            MessageRepository mr = new MessageRepository(mcontext);

            List<Message> messages = mr.GetAllMessagesByUserIds(Convert.ToInt32(Session["LoggedInUser"]), id);
            ViewBag.messages = messages;
            ViewBag.recieverid = id;

           return View("~/Views/Message/Index.cshtml");
         
        }

        [HttpPost]
        public ActionResult NewMessage(FormCollection form)
        {
            UserSqlContext ucontext = new UserSqlContext();
            UserRepository ur = new UserRepository(ucontext);

            User sender = ur.GetGebruikerById(Convert.ToInt32(Session["LoggedInUser"]));
            int recieverid = Convert.ToInt32(form["recieverid"]);
            User reciever = ur.GetGebruikerById(recieverid);

            Message message = new Message(
                sender,
                reciever,
                form["message"]);

            MessageSqlContext mcontext = new MessageSqlContext();
            MessageRepository mr = new MessageRepository(mcontext);

            mr.CreateMessage(message);


            return RedirectToAction("OpenChat", "Message", new { id = recieverid });

        }
    }
}
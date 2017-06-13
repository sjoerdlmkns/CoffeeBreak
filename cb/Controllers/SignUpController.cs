using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cb.DAL.Contexts;
using cb.DAL.Repositories;
using cb.Enums;
using cb.Models;

namespace cb.Controllers
{
    public class SignUpController : Controller
    {
        // GET: SignUp
        public ActionResult Index()
        {
            if (Session["LoggedInUser"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(FormCollection form)
        {
            HttpPostedFileBase file = Request.Files["image"];


            string fileName = file.FileName;
            string fileContentType = file.ContentType;
            byte[] fileBytes = new byte[file.ContentLength];
            file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
            string image = Convert.ToBase64String(fileBytes);

            var newuser = new User(
                form["username"],
                form["password"],
                Gender.Male,
                image);

            UserSqlContext context = new UserSqlContext();
            UserRepository ur = new UserRepository(context);

            ur.CreateUser(newuser);

            Session["LoggedInUser"] = ur.GetUserIdByUsername(newuser.Username);

            return RedirectToAction("Index", "Home");
        }
    }
}
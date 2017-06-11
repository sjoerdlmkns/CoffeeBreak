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
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        // GET: Post
        public ActionResult NewPost()
        {
            if (Session["LoggedInUser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View("~/Views/Post/NewPost.cshtml");
        }


        [HttpPost]
        public ActionResult CreateNewPost(FormCollection form)
        {
            if (Session["LoggedInUser"] == null)
            {
                return RedirectToAction("Index", "Home");
            }


            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["image"];

                
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    string image = Convert.ToBase64String(fileBytes);

                    UserSqlContext ucontext = new UserSqlContext();
                    UserRepository urepo = new UserRepository(ucontext);

                    User user = urepo.GetGebruikerById(Convert.ToInt32(Session["LoggedInUser"]));

                    Post post = new Post(
                        user,
                        form["titel"],
                        Category.Fail,
                        image);


                    PostSqlContext context = new PostSqlContext();
                    PostRepository pr = new PostRepository(context);

                    pr.CreatePost(post);
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult PostPage(int id)
        {
            if (Session["LoggedInUser"] != null)
            {
                UserSqlContext ucontext = new UserSqlContext();
                UserRepository urepo = new UserRepository(ucontext);

                User user = urepo.GetGebruikerById(Convert.ToInt32(Session["LoggedInUser"]));

                ViewBag.user = user;
            }
            else
            {
                ViewBag.user = null;
            }

            PostSqlContext pcontext = new PostSqlContext();
            PostRepository pr = new PostRepository(pcontext);

            var post = pr.GetPostById(id);

            ViewBag.post = post;

            return View("~/Views/Post/PostPage.cshtml");
        }

        [HttpGet]
        public ActionResult LikePost(int id)
        {
            PostSqlContext pcontext = new PostSqlContext();
            PostRepository pr = new PostRepository(pcontext);

            pr.LikePost(id, Convert.ToInt32(Session["LoggedInUser"]));

            return RedirectToAction("PostPage", new { id});
        }
    }
}
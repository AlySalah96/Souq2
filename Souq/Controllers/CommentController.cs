using Souq.Models;
using Souq.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Souq.Controllers
{
    public class CommentController : Controller
    {
        private readonly CommentService commentService;

        public CommentController()
        {
            commentService = new CommentService(new SouqContext());
        }
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }

        

        //
        //public ActionResult Add(Comment newComment)
        //{
        //    if(!ModelState.IsValid)

        //}
    }
}
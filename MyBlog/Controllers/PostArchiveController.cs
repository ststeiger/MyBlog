
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MyBlog.Controllers
{


    public class PostArchiveController : Controller
    {


        //
        // GET: /PostArchive/
        public ActionResult Index(int? year, int? month, int? day, int? postid)
        {
            System.Console.WriteLine(year);

            return View();
        }


    }


}

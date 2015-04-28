
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
		//[HttpPost, HttpGet]
		public ActionResult Index(int? year, int? month, int? day, int? postid)
        {
            System.Console.WriteLine(year);

			try
			{}
			catch(System.IndexOutOfRangeException ex)
			{
			}


			System.DateTime dat = new DateTime (2012, 2, 28);

            return View();
        }


    }


}

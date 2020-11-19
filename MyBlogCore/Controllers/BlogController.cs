using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;


// using System.Web.Mvc;


// meinerettung.ch/felix
namespace MyBlogCore.Controllers
{
    
    
    public class BlogController : Controller
    {

        protected MyDal m_dal;


        public IActionResult IndexABC()
        {
            // return View();
            int num1 = 1;
            int num2 = 2;
            int result = 3;
            return Content($"Result of {num1} + {num2} is {result}", "text/plain");
        }

        public class cSearchResult
        {
            public cSearchResult() { }
            public cSearchResult(string q)
            {
                this.searched_for = q;
            } // End Constructor 


            public string searched_for;
            public System.Collections.Generic.List<T_BlogPost> searchResults;
        } // End Class cSearchResult 


        /*
        ;WITH CTE AS 
        (
	              select 'hello|123' as abc
            union select 'hello+456' as abc
	        union select 'hello-456' as abc
	        union select 'hello~456' as abc
	        union select 'hello!456' as abc
	        union select 'hello*456' as abc
	        union select 'hello#456' as abc
	        union select 'hello#456' as abc
            union select 'hel[l]o-456' as abc 
        )
        SELECT * FROM CTE 
        WHERE abc ILIKE '%\h\e\l\l\o%' ESCAPE '\'
        */

        // WHERE col LIKE @str ESCAPE '\' 
        public static string LikeEscape(string str)
        {
            // WHERE last_name LIKE 'M%!%' ESCAPE '\';
            // http://www.postgresql.org/docs/9.0/static/functions-matching.html
            // # & / ~ ~* !~ !~* ^ $ ,  ; : * .

            // http://www.postgresql.org/docs/9.3/static/sql-syntax-lexical.html
            // Operators: + - * / < > = ~ ! @ # % ^ & | ` ?
            // string specialCharacters = @"\_%+-|[]()?~!*#";

            string strRet = null;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (char c in str)
            {
                sb.Append('\\');
                sb.Append(c);
            } // Next c 

            strRet = sb.ToString();
            sb.Length = 0;
            sb = null;
            return strRet;
        } // End Function LikeEscape


        public JsonResult Search(string q)
        {
            cSearchResult SearchResult = new cSearchResult(q);

            System.Collections.Generic.List<T_BlogPost> ls;
            using (System.Data.IDbCommand cmd = this.m_dal.CreateCommand(@"
SELECT 
	 BP_UID
    ,BP_Title
    ,BP_Content
    ,BP_CreoleText
    ,BP_BBCode
    ,BP_HtmlContent
    ,BP_EntryDate
FROM T_BlogPost 
WHERE (1=1) 
AND 
(
    {fn ILIKE(BP_Title, @searchtext )} 
    OR 
    {fn ILIKE(BP_Content, @searchtext )} 
)
"))
            {
                this.m_dal.AddParameter(cmd, "@searchtext", "%" + LikeEscape(q) + "%");
                ls = this.m_dal.GetList<T_BlogPost>(cmd);
            } // End Using cmd 

            SearchResult.searchResults = ls;

            // return Json(SearchResult, JsonRequestBehavior.AllowGet);
            return Json(SearchResult, new Newtonsoft.Json.JsonSerializerSettings());
        } // End Action Search

        // Content(strHTML, "text/html");


        public class BlogIndex
        {
            public System.Collections.Generic.IList<T_BlogPost> lsBlogEntries;
        } // End Class BlogIndex 


        public void UpdateBlogStructure(IList<T_BlogPost> lsBlogEntries)
        {
            T_BlogPost bp;
            foreach (T_BlogPost bpThisPost in lsBlogEntries)
            {
                bp = bpThisPost;
                string strHTML = ReplaceURLs(bp.BP_Content);

                strHTML = strHTML.Replace("\r\n", "\n");
                strHTML = strHTML.Replace("\n", "<br />");

                bp.BP_HtmlContent = strHTML;
                this.m_dal.Insert<T_BlogPost>(bp);
            } // Next bpThisPost 

        } // End Sub UpdateBlogStructure 


        public static object objlock = new object();
        public static int iCount = 0;

        public ActionResult UploadFiles()
        {
            /*
            lock (objlock)
            {
                iCount++;

                string meth = Request.HttpMethod;
                string str = string.Join(Environment.NewLine + Environment.NewLine + Environment.NewLine, Request.Params);

                foreach (string strKey in Request.Files)
                {
                    System.Web.HttpPostedFileBase pfb = Request.Files[strKey];

                    string SaveToPath = @"c:\temp";
                    SaveToPath = System.IO.Path.Combine(SaveToPath, pfb.FileName);

                    string mime = pfb.ContentType;
                    long size = pfb.ContentLength;

                    pfb.SaveAs(SaveToPath);
                } // Next strKey

                Console.WriteLine(iCount);
                return Content(Request.Params["index"]);
            } // End lock (objlock)
            */

            return null;
        } // End Action UploadFiles 


        public ActionResult Upload()
        {
            return View();
        } // End Action Upload


        public ContentResult Preview(string id, string q)
        {
            q = System.Uri.UnescapeDataString(q);
            // string strHTML = RenderUtils.RenderMarkdown (q);
            // string strHTML = RenderUtils.RenderBbCode(q);
            string strHTML = MyBlog.RenderUtils.RenderMediaWikiMarkup(q);

            return Content(strHTML, "text/html", System.Text.Encoding.UTF8);
        } // End Action Upload


        //
        // GET: /Blog/
        public ActionResult Index()
        {
            BlogIndex bi = new BlogIndex();
            // bi.lsBlogEntries = this.m_dal.GetList<T_BlogPost>(@"");

            using (System.Data.IDbCommand cmd = this.m_dal.CreateLimitedCommand(@"
SELECT {0} 
	 T_BlogPost.*
	,row_number() OVER (ORDER BY BP_EntryDate DESC) AS rownum 
FROM T_BlogPost 
ORDER BY BP_EntryDate DESC
", 100))
            {
                bi.lsBlogEntries = this.m_dal.GetList<T_BlogPost>(cmd);
            }


            // UpdateBlogStructure(bi.lsBlogEntries);

            return View(bi);
        } // End Action Index


        //
        // GET: /Blog/NewEntry
        public ActionResult NewEntry()
        {
            return View();
        } // End Action NewEntry


        public class T_BlogPost
        {
            public Guid BP_UID = System.Guid.NewGuid();
            public Guid BP_Author_USR_UID = System.Guid.Empty;


            public string BP_Title;
            public string BP_Content;
            public string BP_HtmlContent;

            public string BP_Excerpt;
            public string BP_ExcerptHTML;

            public string BP_CommentCount;

            public Guid BP_PostType; // Post, Comment FollowUp


            public DateTime BP_EntryDate = System.DateTime.UtcNow;
            public DateTime BP_LastModifiedDate = System.DateTime.UtcNow;
        } // End Class T_BlogPost


        //
        // POST: /Blog/Create
        [HttpPost]
        public ActionResult AddEntry(string txtTitle, string taBody)
        {
            try
            {
                // TODO: Add insert logic here
                T_BlogPost bp = new T_BlogPost();

                bp.BP_Title = txtTitle; // Request.Params["txtTitle"];
                bp.BP_Title = taBody; // Request.Params["taBody"];

                // this.m_dal.Insert<T_BlogPost>(bp);
                return RedirectToAction("Success");
            }
            catch(System.Exception ex)
            {
                // return View();
                return Json(new { success = false, responseText = ex.Message, stackTrace = ex.StackTrace });
                
            }
        } // End Action AddEntry


        //
        // POST: /Blog/Create
        [HttpPost]
        public ActionResult UpdateEntry(string hdnBP_UID, string txtTitle, string taBody)
        {
            try
            {
                // TODO: Add insert logic here
                T_BlogPost bp = new T_BlogPost();

                //bp.BP_UID = (Guid) System.Convert.ChangeType(Request.Params["hdnBP_UID"], typeof(Guid));
                bp.BP_UID = new System.Guid(hdnBP_UID);

                bp.BP_Title = txtTitle; // Request.Params["txtTitle"];
                bp.BP_Content = taBody; // Request.Params["taBody"];

                // this.m_dal.Insert<T_BlogPost>(bp);
                return RedirectToAction("Success");
            }
            catch
            {
                // return View();
                int num1 = 1;
                int num2 = 2;
                int result = 3;
                return Content($"Result of {num1} + {num2} is {result}", "text/plain");
            }
        } // End Action AddEntry


        //
        // GET: /Blog/Success/5
        public ActionResult Success(string id)
        {
            return View();
        } // End Action Success


        // string str = ReplaceURLs("http://www.google.com/ncr?abc=def#ghi");
        public static string ReplaceURLs(string strPlainText)
        {
            string strPattern = @"((http|ftp|https|[a-zA-Z]):(//|\\)([a-zA-Z0-9\\\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?)|(www\.([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?)";

            strPlainText = System.Text.RegularExpressions.Regex.Replace(strPlainText, strPattern,
             delegate (System.Text.RegularExpressions.Match ma)
             {
                 string url = ma.Groups[0].Value;
                 if (System.Text.RegularExpressions.Regex.IsMatch(url, @"^[a-zA-Z]:\\")) // Starts with drive letter
                 {
                     return string.Format("<a target=\"_blank\" href=\"file:///{0}\">{1}</a>", url.Replace(@"\", "/"), url);
                 }

                 return string.Format("<a target=\"_blank\" href=\"{0}\">{0}</a>", url);
             }
            );

            return strPlainText;
        } // End Function ReplaceURLs 


        public static string GetMachineId()
        {
            string macAddresses = "";

            foreach (System.Net.NetworkInformation.NetworkInterface nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                //if (nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                //{
                //    macAddresses += nic.GetPhysicalAddress().ToString();
                //    break;
                //}

                if (nic.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Loopback)
                {
                    if (nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                    {
                        macAddresses += nic.GetPhysicalAddress().ToString();
                        break;
                    }
                }

            } // Next nic

            return macAddresses;
        } // End Function GetMachineId 


        //
        // GET: /Blog/Delete/5
        public ActionResult ShowEntry(string id)
        {
            T_BlogPost bp = null;


            string lol = "http://localhost/image.aspx?&postimage_text=%0A%5Burl%3Dhttp%3A%2F%2Fpostimg.org%2Fimage%2Fu0zc6aznf%2F%5D%5Bimg%5Dhttp%3A%2F%2Fs1.postimg.org%2Fu0zc6aznf%2Fhtc_hero_wallpaper_03.jpg%5B%2Fimg%5D%5B%2Furl%5D%0A";
            //"http://localhost/image.aspx?&postimage_text=[url=http://postimg.org/image/u0zc6aznf/][img]http://s1.postimg.org/u0zc6aznf/htc_hero_wallpaper_03.jpg[/img][/url]


            lol = System.Web.HttpUtility.UrlDecode(lol);
            Console.WriteLine(lol);


            using (System.Data.IDbCommand cmd = this.m_dal.CreateLimitedCommand("SELECT {0} BP_UID FROM T_BlogPost ORDER BY BP_EntryDate DESC;", 1))
            {
                id = this.m_dal.ExecuteScalar<string>(cmd);
            } // End Using cmd 


            using (System.Data.IDbCommand cmd = this.m_dal.CreateCommand("SELECT * FROM T_BlogPost WHERE BP_UID = @__bp_uid"))
            {
                this.m_dal.AddParameter(cmd, "__bp_uid", new System.Guid(id));
                bp = this.m_dal.GetClass<T_BlogPost>(cmd);
            } // End Using cmd

            bp.BP_Content = ReplaceURLs(bp.BP_Content);


            // http://stackoverflow.com/questions/16389234/create-dropdown-with-predefined-values-in-asp-net-mvc-3-using-razor-view/16389278#16389278


            ViewData["myList"] =
                new SelectList(new[] { "10", "15", "25", "50", "100", "1000" }
                .Select(x => new { value = x, text = x }),
                "value", "text", "15");

            List<SelectListItem> ls = new List<SelectListItem>();

            ls.Add(new SelectListItem() { Text = "Yes", Value = "true", Selected = true });
            ls.Add(new SelectListItem() { Text = "No", Value = "false", Selected = false });
            ls.Add(new SelectListItem() { Text = "Not Applicable", Value = "NULL", Selected = false });

            ViewData["myList"] = ls;

            ViewData["myList"] = new[] { "10", "15", "25", "50", "100", "1000" }
               .Select(x => new SelectListItem
               {
                   Selected = x == "25",
                   Text = x,
                   Value = x
               });

            ViewData["myList"] =
                new SelectList(new[] { "10", "15", "25", "50", "100", "1000" }
                .Select(x => new SelectListItem { Value = x, Text = x }),
                "Value", "Text", "15");

            ViewData["myList"] =
                from c in new[] { "10", "15", "25", "50", "100", "1000" }
                select new SelectListItem
                {
                    Selected = (c == "25"),
                    Text = c,
                    Value = c
                };

            return View(bp);
        } // End Action ShowEntry


        //
        // GET: /Blog/Delete/5
        public ActionResult EditEntry(string id)
        {
            T_BlogPost bp = null;

            using (System.Data.IDbCommand cmd = this.m_dal.CreateLimitedCommand("SELECT {0} BP_UID FROM T_BlogPost ORDER BY BP_EntryDate DESC;", 1))
            {
                id = this.m_dal.ExecuteScalar<string>(cmd);
            } // End Using cmd 

            using (System.Data.IDbCommand cmd = this.m_dal.CreateCommand("SELECT * FROM T_BlogPost WHERE BP_UID = @__bp_uid"))
            {
                this.m_dal.AddParameter(cmd, "__bp_uid", new System.Guid(id));
                bp = this.m_dal.GetClass<T_BlogPost>(cmd);
            } // End Using cmd

            return View(bp);
        } // End Action EditEntry 


    } // End Class BlogController : Controller 


} // End Namespace MyBlog.Controllers 

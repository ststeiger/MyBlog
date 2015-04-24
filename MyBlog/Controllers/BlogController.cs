
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MyBlog.Controllers
{


    public class BlogController : Controller
    {


        public string PrettifyEncode(string strContent)
        {
            if (string.IsNullOrEmpty(strContent))
                return "";

            strContent = strContent.Replace("\t", "      ").Replace(" ", "&nbsp;").Replace("\"", "&quot;").Replace("'", "&#39;");
            return strContent;
        } // End Function PrettifyEncode


        public ContentResult Test()
        {
            string strContent = Server.MapPath("~/DnsPost.txt");
            strContent = System.IO.File.ReadAllText(strContent, System.Text.Encoding.UTF8);


            //strContent = "MonoPost.txt";


            // http://bbcode.codeplex.com/
            //strContent = "[url=http://codekicker.de]codekicker[url]";

            var engine = new WikiPlex.WikiEngine();
            //string output = engine.Render("This is my wiki source!");

            // http://codekicker.de/fragen/Codekicker.BBCode-BBCode-to-HTML/418
            CodeKicker.BBCode.BBCodeParser bbCodeParser = new CodeKicker.BBCode.BBCodeParser(CodeKicker.BBCode.ErrorMode.ErrorFree, null, new[]
            {
                new CodeKicker.BBCode.BBTag("b", "<b>", "</b>"), 
                new CodeKicker.BBCode.BBTag("i", "<span style=\"font-style:italic;\">", "</span>"),
                new CodeKicker.BBCode.BBTag("u", "<span style=\"text-decoration:underline;\">", "</span>"),
                new CodeKicker.BBCode.BBTag("code", "<pre class=\"InsetBox\">", "</pre>", true, true, content => content.Trim()),
                new CodeKicker.BBCode.BBTag("img", "<img src=\"${content}\" />", "", false, true),
                new CodeKicker.BBCode.BBTag("quote", "<blockquote>", "</blockquote>", true, true, content => content.Trim()),
                // http://wikiplex.codeplex.com/wikipage?title=Syntax%20Highlighting&referringTitle=Documentation
                
                
                //new CodeKicker.BBCode.BBTag(System.Text.RegularExpressions.Regex.Escape("csharp"), "<div>", "</div>", true, true, content => Server.HtmlDecode(engine.Render("{code:c#}" + content + "{code:c#}"))),
                
                new CodeKicker.BBCode.BBTag("asax", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Asax))),
                new CodeKicker.BBCode.BBTag("ashx", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Ashx))),
                new CodeKicker.BBCode.BBTag("aspx", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Aspx))),
                new CodeKicker.BBCode.BBTag("aspxcs", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.AspxCs))),
                new CodeKicker.BBCode.BBTag("aspxvb", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.AspxVb))),
                new CodeKicker.BBCode.BBTag("CPP", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Cpp))),
                new CodeKicker.BBCode.BBTag("csharp", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.CSharp))),
                new CodeKicker.BBCode.BBTag("CSS", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Css))),
                new CodeKicker.BBCode.BBTag("HTML", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Html))),
                new CodeKicker.BBCode.BBTag("Java", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Java))),
                new CodeKicker.BBCode.BBTag("JavaScript", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.JavaScript))),
                new CodeKicker.BBCode.BBTag("PHP", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Php))),
                new CodeKicker.BBCode.BBTag("PowerShell", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.PowerShell))),
                //new CodeKicker.BBCode.BBTag("SQL", "<code>", "</code>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Sql))),
                new CodeKicker.BBCode.BBTag("VBdotNET", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.VbDotNet))),
                new CodeKicker.BBCode.BBTag("XML", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Xml))),


                // new CodeKicker.BBCode.BBTag("sql", "<pre class=\"prettyprint linenums lang-sql\">", "</pre>", true, true, content => Server.HtmlEncode(content).Replace("\t", "      ").Replace(" ", "&nbsp;")),
                // new CodeKicker.BBCode.BBTag("python", "<pre class=\"prettyprint linenums lang-py\">", "</pre>", true, true, content => Server.HtmlEncode(content).Replace("\t", "      ").Replace(" ", "&nbsp;")),

                // http://stackoverflow.com/questions/1219860/javascript-jquery-html-encoding
                new CodeKicker.BBCode.BBTag("sql", "<pre class=\"prettyprint linenums lang-sql\">", "</pre>", true, true, content => PrettifyEncode(content)),
                new CodeKicker.BBCode.BBTag("python", "<pre class=\"prettyprint linenums lang-py\">", "</pre>", true, true, content => PrettifyEncode(content)),
                
                //new CodeKicker.BBCode.BBTag("csharp", "<div>", "</div>", true, true, content => new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.CSharp), new CodeKicker.BBCode.BBAttribute[] { CodeKicker.BBCode.HtmlEncodingMode.UnsafeDontEncode} ),
                
                
                new CodeKicker.BBCode.BBTag("list", "<ul>", "</ul>", true, true),
                new CodeKicker.BBCode.BBTag("*", "<li>", "</li>", true, false),
                new CodeKicker.BBCode.BBTag("url", "<a href=\"${href}\" rel=\"nofollow\">", "</a>", new CodeKicker.BBCode.BBAttribute("href", ""), new CodeKicker.BBCode.BBAttribute("href", "href")),
            });


            strContent = strContent == null ? "" : strContent;
            //return bbCodeParser.ToHtml(str.NullToEmpty()).Replace("\r", "").Replace("\n", "<br/>");
            string str = bbCodeParser.ToHtml(strContent).Replace("\r", "").Replace("\n", "<br/>");



            string strHTML = @"<!doctype html>
<html itemscope="""" itemtype=""http://schema.org/WebPage"" lang=""en"">
<head>
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1"" />
    <meta charset=""utf-8"" />
    <title>Markdown Sample Rendering</title>
    <meta name=""viewport"" content=""width=device-width"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
    <link rel=""icon"" href=""/favicon.ico"" type=""image/x-icon"" />

    <style type=""text/css"" media=""all"">
    </style>
</head>
<body>
	{0}
</body>
</html>
";

            strHTML = string.Format(strHTML, str);

            return Content(strHTML, "text/html");
        } // End Action Test


        public class cSearchResult
        {
			public cSearchResult(){}
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
            }

            strRet = sb.ToString();
            sb.Length = 0;
            sb = null;
            return strRet;
        }



        public JsonResult Search(string q)
        {
            cSearchResult SearchResult = new cSearchResult(q);
            
            System.Collections.Generic.List<T_BlogPost> ls; 
            using (System.Data.IDbCommand cmd = Settings.DAL.CreateCommand(@"
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
                Settings.DAL.AddParameter(cmd, "@searchtext", "%" + LikeEscape(q) + "%");
                ls = Settings.DAL.GetList<T_BlogPost>(cmd);
            } // End Using cmd 

			SearchResult.searchResults = ls;

            return Json(SearchResult, JsonRequestBehavior.AllowGet);
        } // End Action Search


		public ContentResult CodeBlock()
		{
			MarkdownSharp.Markdown m = new MarkdownSharp.Markdown();

			string input = "code sample:\n\n    <head>\n    <title>page title</title>\n    </head>\n";
			string actual = m.Transform(input);

            string strHTML = @"<!doctype html>
<html itemscope="""" itemtype=""http://schema.org/WebPage"" lang=""en"">
<head>
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1"" />
    <meta charset=""utf-8"" />
    <title>Markdown Sample Rendering</title>
    <meta name=""viewport"" content=""width=device-width"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
    <link rel=""icon"" href=""/favicon.ico"" type=""image/x-icon"" />

    <style type=""text/css"" media=""all"">
    </style>
</head>
<body>
	{0}
</body>
</html>
";

            strHTML = string.Format(strHTML, actual);

			return Content (strHTML, "text/html");
		} // End Action CodeBlock 


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
                Settings.DAL.Insert<T_BlogPost>(bp);
            } // Next bpThisPost 
            
        } // End Sub UpdateBlogStructure 


        public static object objlock = new object();
        public static int iCount = 0;

        public ActionResult UploadFiles()
        {
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
            
        } // End Action UploadFiles 


        public ActionResult Upload()
        {
            return View();
        } // End Action Upload


        //
        // GET: /Blog/
        public ActionResult Index()
        {
            BlogIndex bi = new BlogIndex();
            bi.lsBlogEntries = Settings.DAL.GetList<T_BlogPost>(@"

");

			using (System.Data.IDbCommand cmd = Settings.DAL.CreateLimitedCommand(@"
SELECT {0} 
	 T_BlogPost.*
	,row_number() OVER (ORDER BY BP_EntryDate DESC) AS rownum 
FROM T_BlogPost 
ORDER BY BP_EntryDate DESC
", 100))
			{
				bi.lsBlogEntries = Settings.DAL.GetList<T_BlogPost> (cmd);
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
        public ActionResult AddEntry(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                T_BlogPost bp = new T_BlogPost();

                bp.BP_Title = Request.Params["txtTitle"];
                bp.BP_Content = Request.Params["taBody"];

                Settings.DAL.Insert<T_BlogPost>(bp);
                return RedirectToAction("Success");
            }
            catch
            {
                return View();
            }
        } // End Action AddEntry


        //
        // POST: /Blog/Create
        [HttpPost]
        public ActionResult UpdateEntry(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                T_BlogPost bp = new T_BlogPost();

                //bp.BP_UID = (Guid) System.Convert.ChangeType(Request.Params["hdnBP_UID"], typeof(Guid));
                bp.BP_UID = new Guid(Request.Params["hdnBP_UID"]);

                bp.BP_Title = Request.Params["txtTitle"];
                bp.BP_Content = Request.Params["taBody"];

                Settings.DAL.Insert<T_BlogPost>(bp);
                return RedirectToAction("Success");
            }
            catch
            {
                return View();
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
             delegate(System.Text.RegularExpressions.Match ma)
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


			using(System.Data.IDbCommand cmd = Settings.DAL.CreateLimitedCommand("SELECT {0} BP_UID FROM T_BlogPost ORDER BY BP_EntryDate DESC;", 1))
			{
				id = Settings.DAL.ExecuteScalar<string>(cmd);
			} // End Using cmd 
            

            using (System.Data.IDbCommand cmd = Settings.DAL.CreateCommand("SELECT * FROM T_BlogPost WHERE BP_UID = @__bp_uid"))
            {
                Settings.DAL.AddParameter(cmd, "__bp_uid", new System.Guid(id));
                bp = Settings.DAL.GetClass<T_BlogPost>(cmd);
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
               .Select(x => new  SelectListItem
                    {
                        Selected = x=="25",
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
            
			using(System.Data.IDbCommand cmd = Settings.DAL.CreateLimitedCommand("SELECT {0} BP_UID FROM T_BlogPost ORDER BY BP_EntryDate DESC;", 1))
			{
				id = Settings.DAL.ExecuteScalar<string>(cmd);
			} // End Using cmd 

            using (System.Data.IDbCommand cmd = Settings.DAL.CreateCommand("SELECT * FROM T_BlogPost WHERE BP_UID = @__bp_uid"))
            {
                Settings.DAL.AddParameter(cmd, "__bp_uid", new System.Guid(id));
                bp = Settings.DAL.GetClass<T_BlogPost>(cmd);
            } // End Using cmd

            return View(bp);
        } // End Action EditEntry 


    } // End Class BlogController : Controller 


} // End Namespace MyBlog.Controllers 

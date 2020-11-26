
using System;
using System.Linq;
// using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


using Dapper;
using MyBlogCore;
using MyBlogCore.Controllers;

/*
/Blog/Index
/Blog/ShowEntry
/Blog/NewEntry => AddEntry
/Blog/EditEntry => UpdateEntry
/Blog/Search?q=
*/

// https://stackoverflow.com/questions/10946931/ignoring-properties-in-dapper

namespace OnlineYournal.Controllers
{


    public class JsonCamelCaseResult
        : IActionResult // ActionResult
    {

        public enum JsonRequestBehavior_t
        {
            AllowGet,
            DenyGet,
        }

        public JsonCamelCaseResult(object data, JsonRequestBehavior_t jsonRequestBehavior)
        {
            Data = data;
            JsonRequestBehavior = jsonRequestBehavior;
        }

        public System.Text.Encoding ContentEncoding { get; set; }

        public string ContentType { get; set; }

        public object Data { get; set; }

        public JsonRequestBehavior_t JsonRequestBehavior { get; set; }


        public async System.Threading.Tasks.Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new System.ArgumentNullException("context");
            }

            if (JsonRequestBehavior == JsonRequestBehavior_t.DenyGet && string.Equals(context.HttpContext.Request.Method, "GET", System.StringComparison.OrdinalIgnoreCase))
            {
                throw new System.InvalidOperationException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.");
            }

            var response = context.HttpContext.Response;

            // https://stackoverflow.com/questions/9254891/what-does-content-type-application-json-charset-utf-8-really-mean
            response.ContentType = "application/json; charset=utf-8";
            //response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";
            //if (ContentEncoding != null)
            //{
            //    response.ContentEncoding = ContentEncoding;
            //}

            if (Data == null)
                return;

            /*
            Newtonsoft.Json.JsonSerializerSettings jsonSerializerSettings =
                new Newtonsoft.Json.JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                };

            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(response.Body, System.Text.Encoding.UTF8))
            {
                using (Newtonsoft.Json.JsonTextWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(writer))
                {
                    Newtonsoft.Json.JsonSerializer ser = Newtonsoft.Json.JsonSerializer.Create(jsonSerializerSettings);

                    ser.Serialize(jsonWriter, Data);
                    jsonWriter.Flush();
                }

                writer.Flush();
            }
            */


            System.Text.Json.JsonSerializerOptions options = new System.Text.Json.JsonSerializerOptions()
            {
                IncludeFields = true,
                WriteIndented = true
                // PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
            };

            await System.Text.Json.JsonSerializer.SerializeAsync(response.Body, Data, options);
        }
    }




    public class JsonStreamingResult
        : IActionResult
    {

        public enum JsonRequestBehavior_t
        {
            AllowGet,
            DenyGet,
        }


        public System.Text.Encoding ContentEncoding { get; set; }

        public string ContentType { get; set; }


        protected string m_sql;
        protected System.Collections.Generic.Dictionary<string, object> m_parameters;
        protected AnySqlWebAdmin.RenderType_t m_renderType;
        protected SqlFactory m_factory;

        public JsonStreamingResult(
              SqlFactory factory
            , AnySqlWebAdmin.RenderType_t renderType
            , JsonRequestBehavior_t jsonRequestBehavior
            , string sql
            , System.Collections.Generic.Dictionary<string, object> pars
            )
        {
            this.m_factory = factory;
            this.m_sql = sql;
            this.m_parameters = pars;
            this.m_renderType = renderType;
            JsonRequestBehavior = jsonRequestBehavior;
        }


        public JsonRequestBehavior_t JsonRequestBehavior { get; set; }

        public async System.Threading.Tasks.Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new System.ArgumentNullException("context");
            }

            if (JsonRequestBehavior == JsonRequestBehavior_t.DenyGet && string.Equals(context.HttpContext.Request.Method, "GET", System.StringComparison.OrdinalIgnoreCase))
            {
                throw new System.InvalidOperationException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.");
            }

            var response = context.HttpContext.Response;

            // https://stackoverflow.com/questions/9254891/what-does-content-type-application-json-charset-utf-8-really-mean

            if (this.m_sql == null)
            {
                response.StatusCode = 500;
                response.ContentType = "application/json; charset=utf-8";
                using (System.IO.StreamWriter output = new System.IO.StreamWriter(response.Body, System.Text.Encoding.UTF8))
                {
                    await output.WriteAsync("{ error: true, msg: \"SQL-command is NULL or empty\"}");
                }

                return;
            }


            using (System.Data.Common.DbConnection con = this.m_factory.Connection)
            {
                await AnySqlWebAdmin.SqlServiceJsonHelper.AnyDataReaderToJson(
                      con
                    , this.m_sql
                    , this.m_parameters
                    , context.HttpContext
                    , this.m_renderType
                );
            }

        }
    }




    public class XmlStreamingResult
        : IActionResult
    {

        public enum XmlRequestBehavior_t
        {
            AllowGet,
            DenyGet,
        }


        public System.Text.Encoding ContentEncoding { get; set; }

        public string ContentType { get; set; }
        public XmlRequestBehavior_t XmlRequestBehavior { get; set; }


        protected string m_sql;
        protected System.Collections.Generic.Dictionary<string, object> m_parameters;
        protected OnlineYournal.XmlRenderType_t m_renderType;
        protected SqlFactory m_factory;



        public XmlStreamingResult(
              SqlFactory factory
            , OnlineYournal.XmlRenderType_t renderType
            , XmlRequestBehavior_t xmlRequestBehavior
            , string sql
            , System.Collections.Generic.Dictionary<string, object> pars
            )
        {
            this.m_factory = factory;
            this.m_sql = sql;
            this.m_parameters = pars;
            this.m_renderType = renderType;
            XmlRequestBehavior = xmlRequestBehavior;
        }


        

        public async System.Threading.Tasks.Task ExecuteResultAsync(ActionContext context)
        {
            if (context == null)
            {
                throw new System.ArgumentNullException("context");
            }

            if (XmlRequestBehavior == XmlRequestBehavior_t.DenyGet && string.Equals(context.HttpContext.Request.Method, "GET", System.StringComparison.OrdinalIgnoreCase))
            {
                throw new System.InvalidOperationException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.");
            }

            var response = context.HttpContext.Response;

            // https://stackoverflow.com/questions/9254891/what-does-content-type-application-json-charset-utf-8-really-mean

            if (this.m_sql == null)
            {
                response.StatusCode = 500;
                response.ContentType = "application/xml; charset=utf-8";
                using (System.IO.StreamWriter output = new System.IO.StreamWriter(response.Body, System.Text.Encoding.UTF8))
                {
                    await output.WriteAsync("{ error: true, msg: \"SQL-command is NULL or empty\"}");
                }

                return;
            }


            using (System.Data.Common.DbConnection con = this.m_factory.Connection)
            {
                await OnlineYournal.SqlServiceXmlHelper.AnyDataReaderToXml(
                      con
                    , this.m_sql
                    , this.m_parameters
                    , "dbo"
                    , "T_BlogPost"
                    , this.m_renderType
                    , context.HttpContext
                    
                );
            }

        }
    }


    public class Blog : Controller
    {

        private static SqlFactory CreateAppropriateFactory()
        {
            if(System.Environment.OSVersion.Platform == PlatformID.Unix)
                return SqlFactory.CreateInstance<Npgsql.NpgsqlFactory>();
            
            return SqlFactory.CreateInstance<System.Data.SqlClient.SqlClientFactory>();
        }
        
        protected SqlFactory m_fac = CreateAppropriateFactory();
        
        
        public IActionResult IndexABC()
        {
            // return View();
            int num1 = 1;
            int num2 = 2;
            int result = 3;
            return Content($"Result of {num1} + {num2} is {result}", "text/plain");
        }


        public IActionResult JsonData()
        {
            string sql = "SELECT * FROM T_BlogPost";
            System.Collections.Generic.Dictionary<string, object> pars =
                new System.Collections.Generic.Dictionary<string, object>();

            return new JsonStreamingResult(
                  this.m_fac
                , AnySqlWebAdmin.RenderType_t.DataTable | AnySqlWebAdmin.RenderType_t.Indented
                , JsonStreamingResult.JsonRequestBehavior_t.AllowGet
                , sql
                , pars
            );
        }


        public IActionResult XmlData()
        {
            string sql = "SELECT * FROM T_BlogPost";
            System.Collections.Generic.Dictionary<string, object> pars =
                new System.Collections.Generic.Dictionary<string, object>();
            
            return new XmlStreamingResult(
                  this.m_fac
                , XmlRenderType_t.Default | XmlRenderType_t.Indented
                , XmlStreamingResult.XmlRequestBehavior_t.AllowGet
                , sql
                , pars
            );
        }


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


        public IActionResult Search(string q)
        {
            cSearchResult SearchResult = new cSearchResult(q);

            string sql = @"
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
";
            sql = this.m_fac.ReplaceOdbcFunctions(sql);

            System.Collections.Generic.List<T_BlogPost> ls;

            using (var con = this.m_fac.Connection)
            {
                ls = con.Query<T_BlogPost>(sql, new { searchtext = "%" + LikeEscape(q) + "%" }).ToList();
            }

            SearchResult.searchResults = ls;

            // https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-5-0#include-fields
            System.Text.Json.JsonSerializerOptions options = new System.Text.Json.JsonSerializerOptions()
            {
                IncludeFields = true,
                WriteIndented = true
            };


            // https://localhost:44397/Blog/Search?q=test
            // return Json(SearchResult, JsonRequestBehavior.AllowGet);
            // return Json(SearchResult); //, new Newtonsoft.Json.JsonSerializerSettings());
            // return Json(SearchResult, options);
            return new JsonCamelCaseResult(SearchResult, JsonCamelCaseResult.JsonRequestBehavior_t.AllowGet);
        } // End Action Search

        // Content(strHTML, "text/html");


        public void UpdateBlogStructure(System.Collections.Generic.IEnumerable<T_BlogPost> lsBlogEntries)
        {
            T_BlogPost bp;

            using (System.Data.Common.DbConnection con = this.m_fac.Connection)
            {

                foreach (T_BlogPost bpThisPost in lsBlogEntries)
                {
                    bp = bpThisPost;
                    string strHTML = ReplaceURLs(bp.BP_Content);

                    strHTML = strHTML.Replace("\r\n", "\n");
                    strHTML = strHTML.Replace("\n", "<br />");

                    bp.BP_HtmlContent = strHTML;

                    con.Insert<T_BlogPost>(bp);
                } // next bpThisPost 

            } // End Using con 

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
            string sql = @"
SELECT 
	 T_BlogPost.*
	,ROW_NUMBER() OVER (ORDER BY BP_EntryDate DESC) AS rownum 
FROM T_BlogPost 
ORDER BY BP_EntryDate DESC
" + this.m_fac.PagingTemplate(100);

            using (System.Data.Common.DbConnection con = this.m_fac.Connection)
            {
                bi.lsBlogEntries = con.Query<T_BlogPost>(sql).ToList();
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
                bp.BP_Content = taBody; // Request.Params["taBody"];

                using (System.Data.Common.DbConnection con = this.m_fac.Connection)
                {
                    con.Insert<T_BlogPost>(bp);
                } // End Using con 

                return RedirectToAction("Success");
            }
            catch (System.Exception ex)
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

                using (System.Data.Common.DbConnection con = this.m_fac.Connection)
                {
                    con.Insert<T_BlogPost>(bp);
                } // End Using con 

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
            if (string.IsNullOrEmpty(strPlainText))
                return strPlainText;

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
        public ActionResult ShowEntry(System.Guid? id)
        {
            string host = (string)this.RouteData.Values["Host"];
            System.Console.WriteLine(host);


            T_BlogPost bp = null;

            // string lol = "http://localhost/image.aspx?&postimage_text=%0A%5Burl%3Dhttp%3A%2F%2Fpostimg.org%2Fimage%2Fu0zc6aznf%2F%5D%5Bimg%5Dhttp%3A%2F%2Fs1.postimg.org%2Fu0zc6aznf%2Fhtc_hero_wallpaper_03.jpg%5B%2Fimg%5D%5B%2Furl%5D%0A";
            //  //"http://localhost/image.aspx?&postimage_text=[url=http://postimg.org/image/u0zc6aznf/][img]http://s1.postimg.org/u0zc6aznf/htc_hero_wallpaper_03.jpg[/img][/url]

            // lol = System.Web.HttpUtility.UrlDecode(lol);
            // System.Console.WriteLine(lol);



            if (!id.HasValue)
            {
                string fetchLatestId = "SELECT BP_UID FROM T_BlogPost ORDER BY BP_EntryDate DESC" + this.m_fac.PagingTemplate(1);

                using (System.Data.Common.DbConnection con = this.m_fac.Connection)
                {
                    id = con.QuerySingle<System.Guid>(fetchLatestId);
                }
            }
            

            string sql = "SELECT * FROM T_BlogPost WHERE BP_UID = @__bp_uid";

            using (System.Data.Common.DbConnection con = this.m_fac.Connection)
            {
                bp = con.QuerySingle<T_BlogPost>(sql, new { __bp_uid = id });
            }

            bp.BP_Content = ReplaceURLs(bp.BP_Content);


            // http://stackoverflow.com/questions/16389234/create-dropdown-with-predefined-values-in-asp-net-mvc-3-using-razor-view/16389278#16389278


            ViewData["myList"] =
                new SelectList(new[] { "10", "15", "25", "50", "100", "1000" }
                .Select(x => new { value = x, text = x }),
                "value", "text", "15");

            System.Collections.Generic.List<SelectListItem> ls = new System.Collections.Generic.List<SelectListItem>();

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
        public ActionResult EditEntry(System.Guid? id)
        {
            T_BlogPost bp = null;
            // string sql = "SELECT {0} BP_UID 
            string sql = "SELECT BP_UID FROM T_BlogPost ORDER BY BP_EntryDate DESC" + this.m_fac.PagingTemplate(1);
            using (System.Data.Common.DbConnection con = this.m_fac.Connection)
            {
                id = con.QuerySingle<System.Guid>(sql);
            }

            sql = "SELECT * FROM T_BlogPost WHERE BP_UID = @__bp_uid";

            using (System.Data.Common.DbConnection con = this.m_fac.Connection)
            {
                bp = con.QuerySingle<T_BlogPost>(sql, new { __bp_uid = id });
            }

            return View(bp);
        } // End Action EditEntry 


    } // End Class BlogController : Controller 


} // End Namespace MyBlog.Controllers 

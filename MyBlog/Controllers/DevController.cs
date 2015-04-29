
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MyBlog.Controllers
{


    public class DevController : Controller
    {


        public ContentResult Index()
        {
            return Info();
        }


        //
        // GET: /Dev/
        public ContentResult Info()
        {
            string str = @"
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1"" />
    <meta charset=""utf-8"" />
    <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
    <meta http-equiv=""cache-control"" content=""max-age=0"" />
    <meta http-equiv=""cache-control"" content=""no-cache"" />
    <meta http-equiv=""expires"" content=""0"" />
    <meta http-equiv=""expires"" content=""Tue, 01 Jan 1980 1:00:00 GMT"" />
    <meta http-equiv=""pragma"" content=""no-cache"" />

    <title>NewEntry</title>
    
    <script src=""reqwest.js"" charset=""utf-8"" type=""text/javascript""></script>
    <script src=""ajax_compat.js"" charset=""utf-8"" type=""text/javascript""></script>
    
    
    <style type=""text/css"" media=""all"">
		
    </style>
</head>
<body>
	
<p>{0}</p>
	
</body>
</html>

";

            if(Request.IsLocal)
                str = string.Format(str, Settings.DAL.GetConnectionString());
            else
                str = string.Format(str, "Top Secret");
            
            return Content(str, "text/html", System.Text.Encoding.UTF8);
        }


    }


}

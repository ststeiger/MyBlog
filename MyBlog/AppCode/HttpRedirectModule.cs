
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MyBlog
{


    public class HttpRedirectModule : IHttpModule
    {

        public HttpRedirectModule() 
        { } // End Constructor

        public void Dispose()
        {
            throw new NotImplementedException();
        } // End Dispose


        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(context_BeginRequest);
        } // End Sub Init


        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;

            //if(context.Request.FilePath.Contains("blahblah.html"))
            //    context.Response.Redirect("http://www.google.com");

            if (!context.Request.IsSecureConnection)
            {
                // send user to SSL 
                string serverName = HttpUtility.UrlEncode(context.Request.ServerVariables["SERVER_NAME"]);
                string filePath = context.Request.FilePath;
                context.Response.Redirect("https://" + serverName + filePath);
            } // End if (!AppObject.Request.IsSecureConnection)
        } // End Sub context_BeginRequest


    } // End Class HttpRedirectModule : IHttpModule


} // End Namespace MyBlog

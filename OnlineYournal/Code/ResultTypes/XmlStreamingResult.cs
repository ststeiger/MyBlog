
using MyBlogCore;


namespace OnlineYournal 
{

    public class XmlStreamingResult
        : Microsoft.AspNetCore.Mvc.IActionResult
    {

        public enum XmlRequestBehavior_t
        {
            AllowGet,
            DenyGet,
        }


        public XmlRequestBehavior_t XmlRequestBehavior { get; set; }

        public string ContentType { get; set; } = "application/xml";
        public System.Text.Encoding ContentEncoding { get; set; } = System.Text.Encoding.UTF8;
        



        protected string m_sql;
        protected object m_parameters;
        protected OnlineYournal.XmlRenderType_t m_renderType;
        protected SqlFactory m_factory;



        public XmlStreamingResult(
              SqlFactory factory
            , OnlineYournal.XmlRenderType_t renderType
            , XmlRequestBehavior_t xmlRequestBehavior
            , string sql
            , object parameters
            )
        {
            this.m_factory = factory;
            this.m_sql = sql;
            this.m_parameters = parameters;
            this.m_renderType = renderType;
            this.XmlRequestBehavior = xmlRequestBehavior;
        } // End Constructor 




        async System.Threading.Tasks.Task Microsoft.AspNetCore.Mvc.IActionResult.ExecuteResultAsync(
            Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            if (context == null)
            {
                throw new System.ArgumentNullException("context");
            }

            if (XmlRequestBehavior == XmlRequestBehavior_t.DenyGet && string.Equals(context.HttpContext.Request.Method, "GET", System.StringComparison.OrdinalIgnoreCase))
            {
                throw new System.InvalidOperationException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.");
            }

            Microsoft.AspNetCore.Http.HttpResponse response = context.HttpContext.Response;

            // https://stackoverflow.com/questions/9254891/what-does-content-type-application-json-charset-utf-8-really-mean

            if (this.m_sql == null)
            {
                response.StatusCode = 500;
                response.ContentType = this.ContentType + "; charset=" + this.ContentEncoding.WebName;
                using (System.IO.StreamWriter output = new System.IO.StreamWriter(response.Body, this.ContentEncoding))
                {
                    await output.WriteAsync("{ error: true, msg: \"SQL-command is NULL or empty\"}");
                }

                return;
            } // End if (this.m_sql == null) 


            using (System.Data.Common.DbConnection con = this.m_factory.Connection)
            {
                await OnlineYournal.SqlServiceXmlHelper.AnyDataReaderToXml(
                      con
                    , this.m_sql
                    , this.m_parameters
                    , "dbo"
                    , "T_BlogPost"
                    , this.ContentEncoding
                    , this.m_renderType
                    , context.HttpContext
                );
            } // End Using con 

        } // End Task ExecuteResultAsync 


    } // End Class XmlStreamingResult 


}

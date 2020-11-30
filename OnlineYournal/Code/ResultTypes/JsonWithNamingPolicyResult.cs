
namespace OnlineYournal
{


    public class JsonWithNamingPolicyResult
    : Microsoft.AspNetCore.Mvc.IActionResult // ActionResult
    {

        public enum JsonRequestBehavior_t
        {
            AllowGet,
            DenyGet,
        }


        public object Data { get; set; }
        public string ContentType { get; set; } = "application/json";
        public JsonRequestBehavior_t JsonRequestBehavior { get; set; }
        public System.Text.Encoding ContentEncoding { get; set; } = System.Text.Encoding.UTF8;
        public System.Text.Json.JsonNamingPolicy NamingPolicy;


        public JsonWithNamingPolicyResult(object data, JsonRequestBehavior_t jsonRequestBehavior
            , System.Text.Json.JsonNamingPolicy namingPolicy = null)
        {
            this.Data = data;
            this.JsonRequestBehavior = jsonRequestBehavior;
            this.NamingPolicy = namingPolicy;
        }


        async System.Threading.Tasks.Task Microsoft.AspNetCore.Mvc.IActionResult.ExecuteResultAsync(
            Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            if (context == null)
            {
                throw new System.ArgumentNullException("context");
            }

            if (JsonRequestBehavior == JsonRequestBehavior_t.DenyGet 
                && string.Equals(context.HttpContext.Request.Method, "GET", System.StringComparison.OrdinalIgnoreCase))
            {
                throw new System.InvalidOperationException("This request has been blocked because sensitive information could be disclosed to third party web sites when this is used in a GET request. To allow GET requests, set JsonRequestBehavior to AllowGet.");
            }

            Microsoft.AspNetCore.Http.HttpResponse response = context.HttpContext.Response;
            // https://stackoverflow.com/questions/9254891/what-does-content-type-application-json-charset-utf-8-really-mean
            response.ContentType = this.ContentType + "; charset=" + this.ContentEncoding.WebName;

            if (Data == null)
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(response.Body, this.ContentEncoding))
                {
                    // await writer.WriteLineAsync("null");
                    await writer.WriteLineAsync("{}");
                } // End Using writer 

                return;
            } // End if (Data == null) 


#if false
            Newtonsoft.Json.JsonSerializerSettings jsonSerializerSettings =
                new Newtonsoft.Json.JsonSerializerSettings
                {
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                };


            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(response.Body, this.ContentEncoding))
            {
                using (Newtonsoft.Json.JsonTextWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(writer))
                {
                    Newtonsoft.Json.JsonSerializer ser = Newtonsoft.Json.JsonSerializer.Create(jsonSerializerSettings);

                    ser.Serialize(jsonWriter, Data);
                    await jsonWriter.FlushAsync();
                } // End Using jsonWriter 

                await writer.FlushAsync();
            } // End Using writer 
#endif


            System.Text.Json.JsonSerializerOptions options = new System.Text.Json.JsonSerializerOptions()
            {
                IncludeFields = true,
                WriteIndented = true,
                PropertyNamingPolicy = this.NamingPolicy
                // PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
            };

            await System.Text.Json.JsonSerializer.SerializeAsync(response.Body, Data, options);
        } // End Task ExecuteResultAsync 


    } // End Class JsonWithNamingPolicyResult 


} // End Namespace 

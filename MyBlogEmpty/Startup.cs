
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



namespace MyBlogEmpty
{


    public class Startup
    {

        private IHostEnvironment CurrentEnvironment { get; set; }

        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration, Microsoft.Extensions.Hosting.IHostEnvironment env)
        {
            this.CurrentEnvironment = env;
        } // End Constructor 



        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            // NodeServicesServiceCollectionExtensions.AddNodeServices(services);
            // NodeServicesServiceCollectionExtensions.AddNodeServices(services, options => options.ProjectPath = this.CurrentEnvironment.ContentRootPath);


            // https://stackoverflow.com/questions/58205735/working-with-spa-services-in-netcore-3-0
            // NodeServicesServiceCollectionExtensions.AddNodeServices(services, options => {
            //     // options.ProjectPath = System.AppContext.BaseDirectory;
            //     options.ProjectPath = this.CurrentEnvironment.ContentRootPath;
            // });


            NodeServicesServiceCollectionExtensions.AddNodeServices(services,
                delegate (Microsoft.AspNetCore.NodeServices.NodeServicesOptions options)
                {
                    // options.ProjectPath = System.AppContext.BaseDirectory;
                    // options.ProjectPath = System.AppContext.BaseDirectory;
                    options.ProjectPath = this.CurrentEnvironment.ContentRootPath;
                }
            );

        } // End Sub ConfigureServices 



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/HelloWorld", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

            // https://www.c-sharpcorner.com/article/nodeservices-where-javascript-and-net-meet-back-on-the-other-side/
            Microsoft.AspNetCore.NodeServices.INodeServices nodeServices = app.ApplicationServices.GetService<Microsoft.AspNetCore.NodeServices.INodeServices>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    int num1 = 10;
                    int num2 = 20;
                    // num2 = 0;


                    object result = "";
                    try
                    {
                        // result = await nodeServices.InvokeAsync<int>("NodeScripts/test_module.js", num1, num2);
                        // result = await nodeServices.InvokeExportAsync<int>("NodeScripts/test_module.js", "add", num1, num2);
                        // result = await nodeServices.InvokeExportAsync<double>("NodeScripts/test_module.js", "divide", num1, num2);
                        result = await nodeServices.InvokeAsync<string>("NodeScripts/parsee.js", "Hello world");
                    }
                    catch (System.Exception ex)
                    {
                        result = ex.Message;
                    }

                    string res = $"Result of {num1} op {num2} is {result}";
                    await context.Response.WriteAsync(res);
                });
            });

        } // End Sub Configure 


    } // End Class Startup 


} // End Namespace MyBlogEmpty

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.NodeServices.HostingModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MyBlogCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IHostEnvironment CurrentEnvironment { get; set; }


        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration,
            Microsoft.Extensions.Hosting.IHostEnvironment env)
        {
            Configuration = configuration;
            this.CurrentEnvironment = env;
        } // End Constructor 


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            System.Collections.IDictionary dict = System.Environment.GetEnvironmentVariables();
            object path = dict["PATH"];
            dict["PATH"] = System.Convert.ToString(path) + ":/root/.nvm/versions/node/v14.3.0/bin";


            NodeServicesServiceCollectionExtensions.AddNodeServices(services,
                delegate(Microsoft.AspNetCore.NodeServices.NodeServicesOptions options)
                {
                    // options.ProjectPath = System.AppContext.BaseDirectory;
                    // options.ProjectPath = System.AppContext.BaseDirectory;
                    options.ProjectPath = this.CurrentEnvironment.ContentRootPath;
                }
            );

            // NodeServicesServiceCollectionExtensions.AddNodeServices(services, options => options.EnvironmentVariables);
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            /*
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            */

            // https://www.c-sharpcorner.com/article/nodeservices-where-javascript-and-net-meet-back-on-the-other-side/
            Microsoft.AspNetCore.NodeServices.INodeServices nodeServices =
                app.ApplicationServices.GetService<Microsoft.AspNetCore.NodeServices.INodeServices>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/w/api.php", async context =>
                {
                    // https://stackoverflow.com/questions/5527868/exact-phrase-search-using-lucene
                    // https://lucene.apache.org/core/3_0_3/fileformats.html
                    // https://www.avajava.com/tutorials/lessons/how-do-i-use-lucene-to-index-and-search-text-files.html
                    // https://lucene.apache.org/core/3_0_3/api/core/org/apache/lucene/document/Field.html
                    // https://michelenasti.com/2018/10/02/let-s-write-a-simple-version-of-the-require-function.html
                    try
                    {
#if true
                        string answer =
                            System.IO.File.ReadAllText("/root/Desktop/wiki.json", System.Text.Encoding.UTF8);
                        context.Response.StatusCode = 200;
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(answer);
#else
                        using (System.Net.WebClient wc = new WebClient())
                        {
                            string ct = context.Request.ContentType;
                            if(ct!= null) 
                                wc.Headers.Add("Content-Type",ct);
                            // wc.Headers.Add("Content-Type","application/x-www-form-urlencoded");
                            
                            string url = "http://en.wikipedia.org/w/api.php";
                            url += context.Request.QueryString.Value;
                        
                            string postData = "";
                            // byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postData);
                            // byte[] byteResult= wc.UploadData(url, context.Request.Method, byteArray);
                            // string answer = System.Text.Encoding.UTF8.GetString(byteResult);
                            string answer = wc.DownloadString(url);
                            System.Console.WriteLine(answer);
                            System.IO.File.WriteAllText("/root/Desktop/wiki.json", answer, System.Text.Encoding.UTF8);
                            context.Response.StatusCode = 200;
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync(answer);
                        }
#endif
                    }
                    catch (Exception e)
                    {
                        context.Response.StatusCode = 500;
                        System.Console.WriteLine(e);
                        throw;
                    }
                });

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
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MyBlogCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            NodeServicesServiceCollectionExtensions.AddNodeServices(services);
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
            app.UseHttpsRedirection();
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




        }
    }
}

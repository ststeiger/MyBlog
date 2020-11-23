using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineYournal
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
            // This method configures the MVC services for the commonly used features with controllers with views. 
            // This combines the effects of Microsoft.Extensions.DependencyInjection.MvcCoreServiceCollectionExtensions.AddMvcCore(Microsoft.Extensions.DependencyInjection.IServiceCollection),
            // Microsoft.Extensions.DependencyInjection.MvcApiExplorerMvcCoreBuilderExtensions.AddApiExplorer(Microsoft.Extensions.DependencyInjection.IMvcCoreBuilder),
            // Microsoft.Extensions.DependencyInjection.MvcCoreMvcCoreBuilderExtensions.AddAuthorization(Microsoft.Extensions.DependencyInjection.IMvcCoreBuilder),
            // Microsoft.Extensions.DependencyInjection.MvcCorsMvcCoreBuilderExtensions.AddCors(Microsoft.Extensions.DependencyInjection.IMvcCoreBuilder),
            // Microsoft.Extensions.DependencyInjection.MvcDataAnnotationsMvcCoreBuilderExtensions.AddDataAnnotations(Microsoft.Extensions.DependencyInjection.IMvcCoreBuilder),
            // Microsoft.Extensions.DependencyInjection.MvcCoreMvcCoreBuilderExtensions.AddFormatterMappings(Microsoft.Extensions.DependencyInjection.IMvcCoreBuilder),
            // Microsoft.Extensions.DependencyInjection.TagHelperServicesExtensions.AddCacheTagHelper(Microsoft.Extensions.DependencyInjection.IMvcCoreBuilder),
            // Microsoft.Extensions.DependencyInjection.MvcViewFeaturesMvcCoreBuilderExtensions.AddViews(Microsoft.Extensions.DependencyInjection.IMvcCoreBuilder),
            // and Microsoft.Extensions.DependencyInjection.MvcRazorMvcCoreBuilderExtensions.AddRazorViewEngine(Microsoft.Extensions.DependencyInjection.IMvcCoreBuilder).
            // To add services for pages call Microsoft.Extensions.DependencyInjection.MvcServiceCollectionExtensions.AddRazorPages(Microsoft.Extensions.DependencyInjection.IServiceCollection)
            // services.AddControllersWithViews();

            services.AddSingleton<SearchValueTransformer>();

            services.AddControllersWithViews(delegate(Microsoft.AspNetCore.Mvc.MvcOptions opts) { 
                // opts.default
            });


            

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

            // https://stackoverflow.com/questions/60791843/changing-routedata-in-asp-net-core-3-1-in-middleware
            app.Use(async (context, next) =>
            {
                string url = context.Request.Headers["HOST"];
                string[] splittedUrl = url.Split('.');

                if (splittedUrl != null && (splittedUrl.Length > 0))
                {
                    context.GetRouteData().Values.Add("Host", splittedUrl[0]);
                    // context.GetRouteData().Values["controller"] = "test";
                }

                // if (splittedUrl != null && (splittedUrl.Length > 0 && splittedUrl[0] == "admin"))
                // {
                // context.GetRouteData().Values.Add("area", "Admin");
                // }

                    // Call the next delegate/middleware in the pipeline
                    await next();
            });


            app.UseAuthorization();

            /*
            app.UseMvc(routes =>
            {
                routes.DefaultHandler = new AreaRouter();
                // routes.MapRoute(name: "areaRoute", template: "{controller=Home}/{action=Index}");
            });
            */
            // https://stackoverflow.com/questions/278668/is-it-possible-to-make-an-asp-net-mvc-route-based-on-a-subdomain

            // https://stackoverflow.com/questions/57172884/mapping-subdomains-to-areas-in-asp-net-core-3
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDynamicControllerRoute<SearchValueTransformer>("search/{**product}");

                endpoints.MapControllerRoute(
                    name: "default",
                    // pattern: "{controller=Home}/{action=Index}/{id?}"
                    //pattern: "{controller=Blog}/{action=Index}/{id?}" 
                    pattern: "{controller=Blog}/{action=ShowEntry}/{id?}" 
                );
                
            });
        }
    } // End Class Startup 

    // https://stackoverflow.com/questions/32582232/imlementing-a-custom-irouter-in-asp-net-5-vnext-mvc-6
    // https://stackoverflow.com/questions/32565768/change-route-collection-of-mvc6-after-startup


    class SearchValueTransformer
        : DynamicRouteValueTransformer
    {
        // private readonly IProductLocator _productLocator;
        public SearchValueTransformer(/*IProductLocator productLocator*/)
        {
            // this._productLocator = productLocator;
        }

        // https://weblogs.asp.net/ricardoperes/dynamic-routing-in-asp-net-core-3#:~:text=ASP.NET%20Core%203%20introduced,request%20will%20be%20dispatched%20to.
        public override async ValueTask<RouteValueDictionary> TransformAsync(
            HttpContext httpContext, RouteValueDictionary values)
        {
            string productString = values["product"] as string;
            object controller = "Blog";
            object action = "Dataa";
            object id = 123; // await this._productLocator.FindProduct("product", out var controller);

            values["controller"] = controller;
            values["action"] = action;
            values["id"] = id;

            return values;
        }
    }


}

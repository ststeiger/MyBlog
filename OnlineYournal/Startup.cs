
using Microsoft.AspNetCore.Builder; // for app.Use*
using Microsoft.AspNetCore.Routing; // for GetRouteData 

using Microsoft.Extensions.Hosting; // for IsDevelopment
// using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; // for AddSingleton, AddOptions, AddControllersWithViews 
using Microsoft.Extensions.Configuration; // for GetConnectionString 



namespace OnlineYournal
{


    public class Startup
    {


        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }


        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();

            services.AddSingleton<SearchValueTransformer>();


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
            /// string cs = Configuration.GetConnectionString("DefaultConnection"); 



            services.AddControllersWithViews(
                    delegate (Microsoft.AspNetCore.Mvc.MvcOptions opts)
                    {
                        // opts.default
                    }
                ).AddRazorRuntimeCompilation(
                    delegate (Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation.MvcRazorRuntimeCompilationOptions options)
                    {
                        // options.FileProviders.Clear();
                        options.FileProviders.Add(new DatabaseFileProvider("cs"));
                        // https://github.com/aspnet/FileSystem/blob/master/src/FS.Embedded/EmbeddedFileProvider.cs
                        // options.FileProviders.Add( new Microsoft.Extensions.FileProviders.EmbeddedFileProvider(typeof(Startup).Assembly) );
                    }
                )
            ;

            

            // https://www.mikesdotnetting.com/article/301/loading-asp-net-core-mvc-views-from-a-database-or-other-location
            // https://stackoverflow.com/questions/38247080/using-razor-outside-of-mvc-in-net-core
            // https://github.com/dotnet/AspNetCore.Docs/issues/14593
            //services.Configure<Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation.MvcRazorRuntimeCompilationOptions>(
            //    delegate(Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation.MvcRazorRuntimeCompilationOptions options) 
            //    {
            //        // Requires adding nuget Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation
            //        // options.FileProviders.Clear();
            //        options.FileProviders.Add(new DatabaseFileProvider("cs"));
            //    }
            //);




            services.AddOptions<Microsoft.AspNetCore.Builder.StaticFileOptions>()
                .Configure<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Hosting.IWebHostEnvironment>(
                    delegate (Microsoft.AspNetCore.Builder.StaticFileOptions options, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContext, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
                    {
                        options.FileProvider = new DomainSpecificFileProvider(httpContext, env);
                    }
            );
        } // End Sub ConfigureServices 


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(Microsoft.AspNetCore.Builder.IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders =    Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor
                                    | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
            });



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


            app.UseDefaultFiles(
                new Microsoft.AspNetCore.Builder.DefaultFilesOptions()
                {
                    DefaultFileNames = new System.Collections.Generic.List<string>()
                    {
                        "index.htm", "index.html" 
                    }
                }
            );


            app.UseStaticFiles(
                new Microsoft.AspNetCore.Builder.StaticFileOptions()
                {
                    ServeUnknownFileTypes = true,
                    DefaultContentType = "application/octet-stream",
                    ContentTypeProvider = new ExtensionContentTypeProvider(),

                    OnPrepareResponse = delegate (Microsoft.AspNetCore.StaticFiles.StaticFileResponseContext context)
                    {
                        // https://stackoverflow.com/questions/49547/how-do-we-control-web-page-caching-across-all-browsers

                        // The Cache-Control is per the HTTP 1.1 spec for clients and proxies
                        // If you don't care about IE6, then you could omit Cache-Control: no-cache.
                        // (some browsers observe no-store and some observe must-revalidate)
                        context.Context.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate, max-age=0";
                        // Other Cache-Control parameters such as max-age are irrelevant 
                        // if the abovementioned Cache-Control parameters (no-cache,no-store,must-revalidate) are specified.


                        // Expires is per the HTTP 1.0 and 1.1 specs for clients and proxies. 
                        // In HTTP 1.1, the Cache-Control takes precedence over Expires, so it's after all for HTTP 1.0 proxies only.
                        // If you don't care about HTTP 1.0 proxies, then you could omit Expires.
                        context.Context.Response.Headers["Expires"] = "-1, 0, Tue, 01 Jan 1980 1:00:00 GMT";

                        // The Pragma is per the HTTP 1.0 spec for prehistoric clients, such as Java WebClient
                        // If you don't care about IE6 nor HTTP 1.0 clients 
                        // (HTTP 1.1 was introduced 1997), then you could omit Pragma.
                        context.Context.Response.Headers["pragma"] = "no-cache";


                        // On the other hand, if the server auto-includes a valid Date header, 
                        // then you could theoretically omit Cache-Control too and rely on Expires only.

                        // Date: Wed, 24 Aug 2016 18:32:02 GMT
                        // Expires: 0

                        // But that may fail if e.g. the end-user manipulates the operating system date 
                        // and the client software is relying on it.
                        // https://stackoverflow.com/questions/21120882/the-date-time-format-used-in-http-headers
                    } // End Sub OnPrepareResponse 

                }
            );



            app.UseRouting();


            // https://stackoverflow.com/questions/60791843/changing-routedata-in-asp-net-core-3-1-in-middleware
            app.Use(
                async delegate (Microsoft.AspNetCore.Http.HttpContext context, System.Func<System.Threading.Tasks.Task> next)
                {
                    string url = context.Request.Headers["HOST"];
                    string[] splittedUrl = url.Split('.');

                    if (splittedUrl != null && (splittedUrl.Length > 0))
                    {
                        context.GetRouteData().Values.Add("Host", splittedUrl[0]);
                        // context.GetRouteData().Values["controller"] = "test";
                    } // End if (splittedUrl != null && (splittedUrl.Length > 0)) 


                    // if (splittedUrl != null && (splittedUrl.Length > 0 && splittedUrl[0] == "admin"))
                    // {
                    //     context.GetRouteData().Values.Add("area", "Admin");
                    // }


                    // Call the next delegate/middleware in the pipeline
                    await next();
                }
            );


            app.UseAuthorization();

            /*
            app.UseMvc(
                delegate(IRouteBuilder routes)
                {
                    routes.DefaultHandler = new Microsoft.AspNetCore.Mvc.Routing.AreaRouter();
                    // routes.MapRoute(name: "areaRoute", template: "{controller=Home}/{action=Index}");
                }
            );
            */


            // https://stackoverflow.com/questions/278668/is-it-possible-to-make-an-asp-net-mvc-route-based-on-a-subdomain

            // https://stackoverflow.com/questions/57172884/mapping-subdomains-to-areas-in-asp-net-core-3
            app.UseEndpoints(
                delegate (Microsoft.AspNetCore.Routing.IEndpointRouteBuilder endpoints)
                {
                    endpoints.MapDynamicControllerRoute<SearchValueTransformer>("search/{**product}");

                    endpoints.MapControllerRoute(
                        name: "default", 
                        // pattern: "{controller=Home}/{action=Index}/{id?}", 
                        // pattern: "{controller=Blog}/{action=Index}/{id?}", 
                        pattern: "{controller=Blog}/{action=ShowEntry}/{id?}" 
                    );
                }
            );

        } // End Sub Configure 


    } // End Class Startup 


} // End Namespace OnlineYournal 

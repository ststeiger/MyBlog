
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace OnlineYournal
{


    public class Program
    {


        public static void Main(string[] args)
        {
            using (System.IO.FileSystemWatcher watcher = new System.IO.FileSystemWatcher())
            {
                // listenOptions.UseHttps("testCert.pfx", "testPassword");                                
                watcher.NotifyFilter = System.IO.NotifyFilters.Size
                    | System.IO.NotifyFilters.LastWrite
                    | System.IO.NotifyFilters.CreationTime
                    | System.IO.NotifyFilters.FileName // Needed if text-file is changed with Visual Studio ...
                ;


                CreateHostBuilder(args, watcher).Build().Run();
            }

        } // End Sub Main 


        // https://github.com/dotnet/aspnetcore/discussions/28238
        // https://github.com/aspnet/KestrelHttpServer/issues/2103
        // https://ayende.com/blog/181281-A/building-a-lets-encrypt-acme-v2-client
        // https://weblog.west-wind.com/posts/2016/feb/22/using-lets-encrypt-with-iis-on-windows
        // https://medium.com/@MaartenSikkema/automatically-request-and-use-lets-encrypt-certificates-in-dotnet-core-9d0d152a59b5
        // https://github.com/dotnet/aspnetcore/issues/1190
        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-5.0#code-try-30
        public static IHostBuilder CreateHostBuilder(string[] args, System.IO.FileSystemWatcher watcher)
        {
            // Microsoft.AspNetCore.Server.IIS
            
            return Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    delegate (IWebHostBuilder webBuilder)
                    {

#if false 

                        // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-5.0#code-try-30
                        webBuilder.ConfigureKestrel(
                            delegate (
                                 WebHostBuilderContext builderContext
                                , Microsoft.AspNetCore.Server.Kestrel.Core.KestrelServerOptions serverOptions)
                            {
                                // https://github.com/dotnet/aspnetcore/pull/24286
                                // From config-file with reload on change 
                                // serverOptions.Configure(builderContext.Configuration.GetSection("Kestrel"), reloadOnChange: false);

                                // On Linux, CipherSuitesPolicy can be used to filter TLS handshakes on a per-connection basis:
                                serverOptions.ConfigureHttpsDefaults(Configuration.Kestrel.Https.HttpsDefaults); // End ConfigureHttpsDefaults 

                                // serverOptions.Listen(System.Net.IPAddress.Loopback, 5001,
                                serverOptions.ListenAnyIP(5005,
                                    delegate (Microsoft.AspNetCore.Server.Kestrel.Core.ListenOptions listenOptions)
                                    {
                                        Configuration.Kestrel.Https.ListenAnyIP(listenOptions, watcher);
                                    }
                                ); // End ListenAnyIp 

                            }
                        ); // End ConfigureKestrel 
#endif
                        
                        // https://developers.redhat.com/blog/2018/07/24/improv-net-core-kestrel-performance-linux/
                        webBuilder.UseLinuxTransport();
                        Newtonsoft.Json.JsonConvert.DefaultSettings = null;
                        
                        /*
                        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
                        {
                            webBuilder.UseIISIntegration();
                        }
                        else webBuilder.UseKestrel();
                        */

                        webBuilder.UseStartup<Startup>()
                        // .UseApplicationInsights()
                        ;

            }); // End ConfigureWebHostDefaults 

        } // End Function CreateHostBuilder 


    } // End Class Program 


} // End Namespace OnlineYournal 


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
            CreateHostBuilder(args).Build().Run();
        } // End Sub Main 


        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    delegate(IWebHostBuilder webBuilder)
                    {
                        webBuilder.UseStartup<Startup>();
                    }
                )
            ;
        } // End Function CreateHostBuilder 


    } // End Class Program 


} // End Namespace OnlineYournal 

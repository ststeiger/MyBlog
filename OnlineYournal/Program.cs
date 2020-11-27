
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace OnlineYournal
{
    public class Program
    {
        
        private static void OnChange(object sender, System.IO.FileSystemEventArgs e)
        {
            System.Console.WriteLine(e.FullPath.ToString() + " is changed!");
        }

        private static void OnRenamed(object sender, System.IO.RenamedEventArgs e)
        {
            System.Console.WriteLine(e.FullPath.ToString() + " was renamed!");
        }

        public static void RunWatcher()
        {
            string filePath1 = "/var/local/buhitg/ststeiger/MyBlog/DevTests/myfile";
            System.IO.FileSystemWatcher watcher = new System.IO.FileSystemWatcher();
            watcher.Path = System.IO.Path.GetDirectoryName(filePath1); 
            watcher.Filter = System.IO.Path.GetFileName(filePath1);
            // watcher.Filter = "*.txt";
            // watcher.NotifyFilter = System.IO.NotifyFilters.LastAccess | System.IO.NotifyFilters.LastWrite | System.IO.NotifyFilters.FileName;
            watcher.NotifyFilter = System.IO.NotifyFilters.LastWrite | System.IO.NotifyFilters.Size;
            
            
            watcher.Changed += new System.IO.FileSystemEventHandler(OnChange);
            watcher.Created += new System.IO.FileSystemEventHandler(OnChange);
            watcher.Deleted += new System.IO.FileSystemEventHandler(OnChange);
            watcher.Renamed += new System.IO.RenamedEventHandler(OnRenamed);
            
            watcher.EnableRaisingEvents = true;

            // System.Console.ReadKey();
            // RunWatcher();
        }


        public static void Main(string[] args)
        {
            RunWatcher();
            CreateHostBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
        
        
    }
    
    
}

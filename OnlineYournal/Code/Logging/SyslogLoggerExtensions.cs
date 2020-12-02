
namespace OnlineYournal.Log
{
    
    
    public static class SyslogLoggerExtensions
    {
        public static Microsoft.Extensions.Logging.ILoggerFactory AddSyslog(this Microsoft.Extensions.Logging.ILoggerFactory factory,
            string host, 
            int port,
            System.Func<string, Microsoft.Extensions.Logging.LogLevel, bool> filter = null)
        {
            factory.AddProvider(new SyslogLoggerProvider(host, port, filter));
            return factory;
        }
    }
    
    
}
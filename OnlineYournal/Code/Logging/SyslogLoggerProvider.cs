
namespace OnlineYournal.Log
{
    
    // https://gunnarpeipman.com/aspnet-core-syslog/
    public class SyslogLoggerProvider 
        : Microsoft.Extensions.Logging.ILoggerProvider
    {
        private string _host;
        private int _port;
 
        private readonly System.Func<string, Microsoft.Extensions.Logging.LogLevel, bool> _filter;
        
        
        public SyslogLoggerProvider(string host, int port, System.Func<string, Microsoft.Extensions.Logging.LogLevel, bool> filter)
        {
            _host = host;
            _port = port;
 
            _filter = filter;
        }
        
        
        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            return new SyslogLogger(categoryName, _host, _port, _filter);
        }
        
        
        public void Dispose()
        {
        }
        
        
    }
    
    
}
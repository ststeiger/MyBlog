
namespace OnlineYournal.Log
{
    
    
    public class SyslogLogger 
        : Microsoft.Extensions.Logging.ILogger
    {
        private const int SyslogFacility = 16;
        
        
        private string _categoryName;
        private string _host;
        private int _port;
        
        private readonly System.Func<string, Microsoft.Extensions.Logging.LogLevel, bool> _filter;
        
        
        public SyslogLogger(string categoryName,
            string host,
            int port,
            System.Func<string, Microsoft.Extensions.Logging.LogLevel, bool> filter)
        {
            _categoryName = categoryName;
            _host = host;
            _port = port;
            _filter = filter;
        }

        public System.IDisposable BeginScope<TState>(TState state)
        {
            return NoopDisposable.Instance;
        }

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            return (_filter == null || _filter(_categoryName, logLevel));
        }

        public void Log<TState>(
              Microsoft.Extensions.Logging.LogLevel logLevel
            , Microsoft.Extensions.Logging.EventId eventId
            , TState state
            , System.Exception exception
            , System.Func<TState, System.Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new System.ArgumentNullException(nameof(formatter));
            }

            string message = formatter(state, exception);

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            System.FormattableString fmtString = $"{logLevel}: {message}";
            message = fmtString.ToString(System.Globalization.CultureInfo.InvariantCulture);

            if (exception != null)
            {
                message += System.Environment.NewLine + System.Environment.NewLine + exception.ToString();
            }

            SyslogLogLevel syslogLevel = MapToSyslogLevel(logLevel);
            Send(syslogLevel, message);
        }

        internal void Send(SyslogLogLevel logLevel, string message)
        {
            if (string.IsNullOrWhiteSpace(_host) || _port <= 0)
            {
                return;
            }

            string hostName = System.Net.Dns.GetHostName();
            int level = SyslogFacility * 8 + (int) logLevel;
            string logMessage = string.Format(System.Globalization.CultureInfo.InvariantCulture
                , "<{0}>{1} {2}", level, hostName, message
            );
            
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(logMessage);
            
            using (System.Net.Sockets.UdpClient client = new System.Net.Sockets.UdpClient())
            {
                client.Send(bytes, bytes.Length, _host, _port); 
            }
        }

        private SyslogLogLevel MapToSyslogLevel(Microsoft.Extensions.Logging.LogLevel level)
        {
            if (level == Microsoft.Extensions.Logging.LogLevel.Critical)
                return SyslogLogLevel.Critical;
            if (level == Microsoft.Extensions.Logging.LogLevel.Debug)
                return SyslogLogLevel.Debug;
            if (level == Microsoft.Extensions.Logging.LogLevel.Error)
                return SyslogLogLevel.Error;
            if (level == Microsoft.Extensions.Logging.LogLevel.Information)
                return SyslogLogLevel.Info;
            if (level == Microsoft.Extensions.Logging.LogLevel.None)
                return SyslogLogLevel.Info;
            if (level == Microsoft.Extensions.Logging.LogLevel.Trace)
                return SyslogLogLevel.Info;
            if (level == Microsoft.Extensions.Logging.LogLevel.Warning)
                return SyslogLogLevel.Warn;
            
            return SyslogLogLevel.Info;
        }
    }
}

#define WITH_ASYNC_DISPOSABLE 

namespace OnlineYournal.Log
{
    public sealed class NoopDisposable 
        : System.IDisposable 
#if WITH_ASYNC_DISPOSABLE
        , System.IAsyncDisposable
#endif
    
    {
        public static NoopDisposable Instance = new NoopDisposable();
        
    
#if WITH_ASYNC_DISPOSABLE
        /// <summary>
        /// Does nothing.
        /// </summary>
        public  System.Threading.Tasks.ValueTask DisposeAsync() => new System.Threading.Tasks.ValueTask();
#endif
        
        
        public void Dispose()
        {
        }
    }
    
}
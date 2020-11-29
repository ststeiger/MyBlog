using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace OnlineYournal
{
    
    
    public class ClientAppFileProvider 
        : IFileProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        public ClientAppFileProvider(IHttpContextAccessor httpContextAccessor, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _httpContextAccessor = httpContextAccessor;
            _env = env;
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            // https://www.programmersought.com/article/9546758626/
            throw new System.NotImplementedException();
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            var host = _httpContextAccessor.HttpContext.Request.Host;
            if (host.Equals("app.domain.com"))
            {
                subpath = Path.Combine("app", subpath);
            }
            else if (host.Equals("admin.domain.com"))
            {
                subpath = Path.Combine("admin", subpath);
            }
            else if (host.Equals("www.domain.com"))
            {
                subpath = Path.Combine("www", subpath);
            }

            return _env.ContentRootFileProvider.GetFileInfo(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            // https://github.com/mikebrind/RazorEngineViewOptionsFileProviders/blob/master/RazorEngineViewOptionsFileProviders/src/RazorEngineViewOptionsFileProviders/DatabaseChangeToken.cs
            throw new System.NotImplementedException();
        }
    }

}
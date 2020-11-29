
namespace OnlineYournal
{

    // https://stackoverflow.com/questions/58016988/determine-static-files-based-off-route-or-domain-in-asp-net-core-for-spa
    // https://github.com/aspnet/FileSystem/blob/master/src/FS.Physical/PhysicalFileProvider.cs
    // https://raw.githubusercontent.com/aspnet/FileSystem/master/src/FS.Physical/Internal/PathUtils.cs
    // https://github.com/mikebrind/RazorEngineViewOptionsFileProviders/blob/master/RazorEngineViewOptionsFileProviders/src/RazorEngineViewOptionsFileProviders/DatabaseFileProvider.cs
    // https://github.com/lamondlu/AWSS3FileProvider
    // https://www.programmersought.com/article/9546758626/
    // https://github.com/mikebrind/RazorEngineViewOptionsFileProviders/blob/master/RazorEngineViewOptionsFileProviders/src/RazorEngineViewOptionsFileProviders/DatabaseChangeToken.cs
    public class DomainSpecificFileProvider 
        : Microsoft.Extensions.FileProviders.IFileProvider
    {

        private readonly Microsoft.AspNetCore.Http.IHttpContextAccessor _httpContextAccessor;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _env;


        public DomainSpecificFileProvider(Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            _httpContextAccessor = httpContextAccessor;
            _env = env;
        } // End Constructor 


        public Microsoft.Extensions.FileProviders.IDirectoryContents GetDirectoryContents(string subpath)
        {
            // Microsoft.Extensions.FileProviders.IDirectoryContents dc = _env.ContentRootFileProvider.GetDirectoryContents(subpath);
            // Microsoft.Extensions.FileProviders.IDirectoryContents dc = _env.WebRootFileProvider.GetDirectoryContents(subpath);

            return _env.WebRootFileProvider.GetDirectoryContents(subpath); ;
        } // End Function GetDirectoryContents 


        public Microsoft.Extensions.FileProviders.IFileInfo GetFileInfo(string subpath)
        {
            if (_httpContextAccessor.HttpContext.Request.Host.HasValue)
            {
                // string host = _httpContextAccessor.HttpContext.Request.Host.Value;
                string host = _httpContextAccessor.HttpContext.Request.Host.Host;

                if (subpath != null && host != null)
                {
                    string subpath2 = subpath.StartsWith("/") ? subpath : "/" + subpath;
                    subpath2 = "/" + host + subpath2;

                    // Microsoft.Extensions.FileProviders.IFileInfo fi = _env.ContentRootFileProvider.GetFileInfo(subpath);
                    // Microsoft.Extensions.FileProviders.IFileInfo fi = _env.WebRootFileProvider.GetFileInfo(subpath);

                    Microsoft.Extensions.FileProviders.IFileInfo fiOverride = _env.WebRootFileProvider.GetFileInfo(subpath2);
                    if (fiOverride.Exists)
                        return fiOverride;
                } // End if (subpath != null && host != null) 
            } // End if (_httpContextAccessor.HttpContext.Request.Host.HasValue) 

            return _env.WebRootFileProvider.GetFileInfo(subpath);
        } // End Function GetFileInfo 


        public Microsoft.Extensions.Primitives.IChangeToken Watch(string filter)
        {
            return _env.WebRootFileProvider.Watch(filter);
        } // End Function Watch 


    } // End Class DomainSpecificFileProvider 


}

namespace OnlineYournal
{


    public class DatabaseFileProvider 
        : Microsoft.Extensions.FileProviders.IFileProvider
    {
        private MyBlogCore.SqlFactory m_factory;
        public DatabaseFileProvider(MyBlogCore.SqlFactory factory)
        {
            this.m_factory = factory;
        }
        public Microsoft.Extensions.FileProviders.IDirectoryContents GetDirectoryContents(string subpath)
        {
            throw new System.NotImplementedException();
        }

        public Microsoft.Extensions.FileProviders.IFileInfo GetFileInfo(string subpath)
        {
            var result = new DatabaseFileInfo(this.m_factory, subpath);
            return result.Exists ? result as Microsoft.Extensions.FileProviders.IFileInfo : new Microsoft.Extensions.FileProviders.NotFoundFileInfo(subpath);
        }

        public Microsoft.Extensions.Primitives.IChangeToken Watch(string filter)
        {
            return new DatabaseChangeToken(this.m_factory, filter);
        }


    }


}

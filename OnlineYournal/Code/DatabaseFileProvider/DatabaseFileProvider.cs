
namespace OnlineYournal
{


    public class DatabaseFileProvider 
        : Microsoft.Extensions.FileProviders.IFileProvider
    {
        private string _connection;
        public DatabaseFileProvider(string connection)
        {
            _connection = connection;
        }
        public Microsoft.Extensions.FileProviders.IDirectoryContents GetDirectoryContents(string subpath)
        {
            throw new System.NotImplementedException();
        }

        public Microsoft.Extensions.FileProviders.IFileInfo GetFileInfo(string subpath)
        {
            var result = new DatabaseFileInfo(_connection, subpath);
            return result.Exists ? result as Microsoft.Extensions.FileProviders.IFileInfo : new Microsoft.Extensions.FileProviders.NotFoundFileInfo(subpath);
        }

        public Microsoft.Extensions.Primitives.IChangeToken Watch(string filter)
        {
            return new DatabaseChangeToken(_connection, filter);
        }


    }


}

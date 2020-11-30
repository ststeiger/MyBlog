
using System.Data.SqlClient;


namespace OnlineYournal
{


    public class DatabaseFileInfo 
        : Microsoft.Extensions.FileProviders.IFileInfo
    {
        private string _viewPath;
        private byte[] _viewContent;
        private System.DateTimeOffset _lastModified;
        private bool _exists;
        

        public bool Exists => _exists;

        public bool IsDirectory => false;

        public System.DateTimeOffset LastModified => _lastModified;


        public DatabaseFileInfo(MyBlogCore.SqlFactory factory, string viewPath)
        {
            _viewPath = viewPath;
            GetView(factory, viewPath);
        }


        public long Length
        {
            get
            {
                using (var stream = new System.IO.MemoryStream(_viewContent))
                {
                    return stream.Length;
                }
            }
        }

        public string Name => System.IO.Path.GetFileName(_viewPath);

        public string PhysicalPath => null;

        public System.IO.Stream CreateReadStream()
        {
            return new System.IO.MemoryStream(_viewContent);
        }

        private void GetView(MyBlogCore.SqlFactory factory, string viewPath)
        {
            string query = @"
SELECT Content, LastModified FROM Views WHERE Location = @Path; 
UPDATE Views SET LastRequested = GetUtcDate() WHERE Location = @Path; 
";
            try
            {
                using (System.Data.Common.DbConnection cnn = factory.Connection)
                {
                    using (System.Data.Common.DbCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.AddWithValue("@Path", viewPath);
                        
                        if (cnn.State != System.Data.ConnectionState.Open)
                            cnn.Open();

                        using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                        {
                            _exists = reader.HasRows;
                            if (_exists)
                            {
                                reader.Read();
                                _viewContent = System.Text.Encoding.UTF8.GetBytes(reader["Content"].ToString());
                                _lastModified = System.Convert.ToDateTime(reader["LastModified"]);
                            } // End if (_exists) 

                        } // End Using reader 

                    } // End Using cmd 

                    if (cnn.State != System.Data.ConnectionState.Closed)
                        cnn.Close();
                } // End Using cnn 
            }
            catch (System.Exception ex)
            {
                // if something went wrong, Exists will be false
            }
        }
    }
}
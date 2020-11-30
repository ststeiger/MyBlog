
using System.Data.SqlClient;



namespace OnlineYournal
{


    public class DatabaseChangeToken 
        : Microsoft.Extensions.Primitives.IChangeToken
    {
        private string _connection;
        private string _viewPath;

        private MyBlogCore.SqlFactory m_factory;
        
        public bool ActiveChangeCallbacks => false;


        public DatabaseChangeToken(MyBlogCore.SqlFactory factory, string viewPath)
        {
            this.m_factory = factory;
            _viewPath = viewPath;
        }
        
        
        public bool HasChanged
        {
            get
            {
                try
                {
                    using (System.Data.Common.DbConnection cnn =  this.m_factory.Connection)
                    {
                        using (System.Data.Common.DbCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT LastRequested, LastModified FROM Views WHERE Location = @Path;";
                            cmd.AddWithValue("@Path", _viewPath);

                            if (cnn.State != System.Data.ConnectionState.Open)
                                cnn.Open();

                            using (System.Data.Common.DbDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    if (reader["LastRequested"] == System.DBNull.Value)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        return System.Convert.ToDateTime(reader["LastModified"]) > System.Convert.ToDateTime(reader["LastRequested"]);
                                    }
                                }
                                else
                                {
                                    return false;
                                }

                            } // End Using reader 

                        } // End Using cmd 


                        if (cnn.State != System.Data.ConnectionState.Closed)
                            cnn.Close();
                    } // End Using cnn 

                }
                catch (System.Exception)
                {
                    return false;
                }
            }
        }


        public System.IDisposable RegisterChangeCallback(System.Action<object> callback, object state) 
        {
            return EmptyDisposable.Instance;
        }


    }



    internal class EmptyDisposable : System.IDisposable
    {
        public static EmptyDisposable Instance { get; } = new EmptyDisposable();
        private EmptyDisposable() { }
        public void Dispose() { }
    }


}


namespace DB
{


    public class SqlFactory
    {

        private static int s_seed;
        private static readonly System.Threading.ThreadLocal<System.Random> s_random;

        protected string m_connectionString;
        protected string[] m_connectionStrings;
        protected int m_connectionCount;

        protected delegate string GetConnectionString_t();
        protected GetConnectionString_t m_GetInternalConnectionString;


        protected System.Data.Common.DbProviderFactory Factory;



        private static System.Random GetRandom()
        {
            return new System.Random(System.Threading.Interlocked.Increment(ref s_seed));
        }


        protected string GetScalarConnectionString()
        {
            return this.m_connectionString;
        }


        protected string GetConnectionStringFromArray()
        {
            int i = s_random.Value.Next(0, this.m_connectionCount);

            return this.m_connectionStrings[i];
        }


        public void SetConnectionStrings(params string[] connectionStrings)
        {
            if (connectionStrings == null)
                return;
            
            if (connectionStrings.Length > 1)
            {
                this.m_connectionCount = connectionStrings.Length;
                this.m_connectionStrings = connectionStrings;
                this.m_GetInternalConnectionString = GetConnectionStringFromArray;
            }

            else if (connectionStrings.Length == 1)
            {
                this.m_connectionString = connectionStrings[0];
                this.m_GetInternalConnectionString = GetScalarConnectionString;
            }
            else
                throw new System.InvalidOperationException("SqlFactory needs at least one connection string");
            
        }


        static SqlFactory()
        {
            s_seed = System.Environment.TickCount;
            s_random = new System.Threading.ThreadLocal<System.Random>(GetRandom);
        }



        public SqlFactory(System.Data.Common.DbProviderFactory factory, params string[] connectionStrings)
        {
            this.Factory = factory;
            SetConnectionStrings(connectionStrings);
        }


        public SqlFactory(params string[] connectionStrings)
            : this(System.Data.SqlClient.SqlClientFactory.Instance, connectionStrings)
        { }


        public SqlFactory()
         : this((string[])null)
        { }


        public string ConnectionString
        {
            get
            {
                return this.m_GetInternalConnectionString();
            }
        }


        public System.Data.Common.DbConnection Connection
        {
            get
            {
                System.Data.Common.DbConnection conn = this.Factory.CreateConnection();
                conn.ConnectionString = this.ConnectionString;

                return conn;
            }
        }


    }


}

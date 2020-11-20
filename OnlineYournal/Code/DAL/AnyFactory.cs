
namespace MyBlogCore
{


    public class AnyFactory<T>
        : SqlFactory
        where T : System.Data.Common.DbProviderFactory

    {


        protected string m_cs;

        public override string ConnectionString
        {
            get
            {
                if (m_cs != null)
                    return this.m_cs;

                return this.m_cs;
            }
        }

        protected delegate string OdbcFunctionReplacementCallback_t(System.Text.RegularExpressions.Match mThisMatch);

        protected OdbcFunctionReplacementCallback_t m_callback;


        protected override string OdbcFunctionReplacementCallback(System.Text.RegularExpressions.Match mThisMatch)
        {
            if (m_callback != null)
                return m_callback(mThisMatch);

            return "";
        }


        public AnyFactory()
            : base(default(T))
        {
            System.Type t = typeof(T);
            if (object.ReferenceEquals(t, typeof(Npgsql.NpgsqlFactory)))
            {
                this.m_cs = pg_implements.LocalConnectionString;
                m_callback = pg_implements.OdbcFunctionReplacementCallback;
            }
            else if (object.ReferenceEquals(t, typeof(Npgsql.NpgsqlFactory)))
            {
                this.m_cs = ms_implements.LocalConnectionString;
                m_callback = ms_implements.OdbcFunctionReplacementCallback;
            }
            else
            {
                throw new System.NotImplementedException(t.FullName);
            }

        } // End Constructor 


    } // End Class 


}

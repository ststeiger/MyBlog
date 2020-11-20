
namespace MyBlogCore 
{

    public abstract class SqlFactory
    {


        // Credits: http://web.archive.org/web/20100123183531/http://blogs.msdn.com/bclteam/archive/2005/03/15/396452.aspx
        protected const string strOdbcMatchingPattern = odbc_implements.strOdbcMatchingPattern;


        protected System.Data.Common.DbProviderFactory Factory;

        public abstract string ConnectionString { get; }


        protected SqlFactory(System.Data.Common.DbProviderFactory fac)
        {
            this.Factory = fac;
        }


        public virtual System.Data.Common.DbConnection Connection
        {
            get
            {
                System.Data.Common.DbConnection conn = this.Factory.CreateConnection();
                conn.ConnectionString = this.ConnectionString;

                return conn;
            }
        }


        public virtual string PagingTemplate(ulong offset, ulong rows)
        {
            return string.Concat(
                 "\r\nOFFSET "
                , offset.ToString(System.Globalization.CultureInfo.InvariantCulture)
                , " ROWS \r\nFETCH NEXT "
                , rows.ToString(System.Globalization.CultureInfo.InvariantCulture)
                , " ROWS ONLY \r\n"
            );
        }


        public virtual string PagingTemplate(ulong rows)
        {
            return PagingTemplate(0, rows);
        }

        protected virtual string OdbcFunctionReplacementCallback(System.Text.RegularExpressions.Match mThisMatch)
        {
            return "";
        }


        // MsgBox(String.Join(Environment.NewLine, GetArguments("bla")));
        protected virtual string[] GetArguments(string strAllArguments)
        {
            
            return odbc_implements.GetArguments(strAllArguments);
        } // End Function GetArguments


        public virtual string ReplaceOdbcFunctions(string sql)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return sql;
            }

            string retValue = System.Text.RegularExpressions.Regex.Replace(sql, strOdbcMatchingPattern, OdbcFunctionReplacementCallback);
            return retValue;
        } // End Function ReplaceOdbcFunctions


        public static SqlFactory CreateInstance<T>(string cs)
            where T : System.Data.Common.DbProviderFactory
        {

            AnyFactory<T> fac = System.Activator.CreateInstance<AnyFactory<T>>();


            return fac;
        }


        public static SqlFactory CreateInstance<T>()
            where T : System.Data.Common.DbProviderFactory
        {
            SqlFactory fac = CreateInstance<T>(null);

            return fac;
        }


    }


}

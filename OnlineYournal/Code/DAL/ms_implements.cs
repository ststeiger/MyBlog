
namespace MyBlogCore
{


    internal class ms_implements
    {
        private const string strOdbcMatchingPattern = odbc_implements.strOdbcMatchingPattern;

        
        internal static string LocalConnectionString
        {
            get
            {
                System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();
                csb.DataSource = System.Environment.MachineName;
                csb.InitialCatalog = "Blogz";
                // csb.Port = 5432;

                csb.IntegratedSecurity = true;

                if (!csb.IntegratedSecurity)
                {
                    csb.UserID = "apws";
                    csb.Password = "Test123";
                }
                
                csb.Pooling = true;
                csb.ApplicationName = "MyBlog";

                return csb.ConnectionString;
            }
        }



        internal static string OdbcFunctionReplacementCallback(System.Text.RegularExpressions.Match mThisMatch)
        {
            // Get the matched string.
            string strExpression = mThisMatch.Groups[1].Value;
            string strFunctionName = mThisMatch.Groups[2].Value;
            string strArguments = mThisMatch.Groups[3].Value;

            if (System.Text.RegularExpressions.Regex.IsMatch(strArguments, strOdbcMatchingPattern))
            {
                strArguments = System.Text.RegularExpressions.Regex.Replace(strArguments, strOdbcMatchingPattern, OdbcFunctionReplacementCallback);
            }


            // Simple one or 0 arguments
            // if (StringComparer.OrdinalIgnoreCase.Equals("lcase", strFunctionName))
            // { return "LOWER(" + strArguments + ") "; }



            string[] astrArguments = odbc_implements.GetArguments(strArguments);

            if (System.StringComparer.InvariantCultureIgnoreCase.Equals("ilike", strFunctionName))
            {
                string strTerm = "( " + astrArguments[0] + " LIKE " + astrArguments[1] + @" ESCAPE '\' ) ";
                return strTerm;
            }

            if (System.StringComparer.InvariantCultureIgnoreCase.Equals("like", strFunctionName))
            {
                string strTerm = "( " + astrArguments[0] + " COLLATE Latin1_General_BIN LIKE " + astrArguments[1] + @" ESCAPE '\' ) ";
                return strTerm;
            }



            // if (StringComparer.OrdinalIgnoreCase.Equals("left", strFunctionName))
            // { string strTerm = "LPAD(" + astrArguments[0] + ", " + astrArguments[1] + ", '') "; return strTerm;}

            return "ODBC FUNCTION \"" + strFunctionName + "\" not defined in abstraction layer...";
        } // End Function OdbcFunctionReplacementCallback


    }


}

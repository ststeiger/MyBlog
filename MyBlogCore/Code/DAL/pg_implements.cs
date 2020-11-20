
namespace MyBlogCore
{


    internal class pg_implements
    {

        private const string strOdbcMatchingPattern = odbc_implements.strOdbcMatchingPattern;


        internal static string LocalConnectionString
        {
            get
            {
                Npgsql.NpgsqlConnectionStringBuilder csb = new Npgsql.NpgsqlConnectionStringBuilder();
                csb.Host = "127.0.0.1";
                csb.Database = "blogz";
                csb.Port = 5432;

                csb.IntegratedSecurity = false;

                if (!csb.IntegratedSecurity)
                {
                    csb.Username = "apws";
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
                strArguments = System.Text.RegularExpressions.Regex.Replace(strArguments, strOdbcMatchingPattern,
                    OdbcFunctionReplacementCallback);
            }


            if (System.StringComparer.OrdinalIgnoreCase.Equals("lcase", strFunctionName))
            {
                return "LOWER(" + strArguments + ") ";
            }


            if (System.StringComparer.OrdinalIgnoreCase.Equals("ucase", strFunctionName))
            {
                return "UPPER(" + strArguments + ") ";
            }


            if (System.StringComparer.OrdinalIgnoreCase.Equals("now", strFunctionName))
            {
                return "CURRENT_TIMESTAMP ";
            }





            string[] astrArguments = odbc_implements.GetArguments(strArguments);

            if (System.StringComparer.InvariantCultureIgnoreCase.Equals("ilike", strFunctionName))
            {
                string strTerm = "( " + astrArguments[0] + " ILIKE " + astrArguments[1] + @" ESCAPE '\' ) ";
                return strTerm;
            }

            if (System.StringComparer.InvariantCultureIgnoreCase.Equals("like", strFunctionName))
            {
                string strTerm = "( " + astrArguments[0] + " LIKE " + astrArguments[1] + @" ESCAPE '\' ) ";
                return strTerm;
            }


            if (System.StringComparer.OrdinalIgnoreCase.Equals("left", strFunctionName))
            {
                string strTerm = "LPAD(" + astrArguments[0] + ", " + astrArguments[1] + ", '') ";
                return strTerm;
            }

            if (System.StringComparer.OrdinalIgnoreCase.Equals("right", strFunctionName))
            {
                string strTerm = "SUBSTRING(" + astrArguments[0] + " FROM CHAR_LENGTH(" + astrArguments[0] +
                                 " ) - ( " + astrArguments[1] + " ) + 1 ) ";
                return strTerm;
            }


            if (System.StringComparer.OrdinalIgnoreCase.Equals("concat", strFunctionName))
            {
                string strTerm = astrArguments[0] + " || " + astrArguments[1];
                return strTerm;
            }


            //AND EXTRACT(day from HIDOC_Datum - timestamp '2011-12-07' ) = 0

            if (System.StringComparer.OrdinalIgnoreCase.Equals("timestampdiff", strFunctionName))
            {
                string strTerm = "";
                if (System.StringComparer.OrdinalIgnoreCase.Equals("SQL_TSI_DAY", astrArguments[0]))
                {
                    strTerm = "abs(extract(day from " + astrArguments[1] + " - " + astrArguments[2] + " )) ";
                }
                else
                {
                    throw new System.NotImplementedException();
                }

                return strTerm;
            }


            if (System.StringComparer.OrdinalIgnoreCase.Equals("dayofmonth", strFunctionName))
            {
                string strTerm = "date_part('day', " + astrArguments[0] + ") ";
                return strTerm;
            }


            if (System.StringComparer.OrdinalIgnoreCase.Equals("month", strFunctionName))
            {
                string strTerm = "date_part('month', " + astrArguments[0] + ") ";
                return strTerm;
            }


            if (System.StringComparer.OrdinalIgnoreCase.Equals("year", strFunctionName))
            {
                string strTerm = "date_part('year', " + astrArguments[0] + ") ";
                return strTerm;
            }

            return "ODBC FUNCTION \"" + strFunctionName + "\" not defined in abstraction layer...";
        } // End Function OdbcFunctionReplacementCallback



    }




}

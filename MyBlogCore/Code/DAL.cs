
using System.Collections.Generic;
using Dapper;
using MyBlogCore.Controllers;
using Npgsql;


namespace MyBlogCore
{
    

    
    
    
    
    public class AnyFactory<T>  
        :SqlFactory
        where T : System.Data.Common.DbProviderFactory
        
    {


        protected string m_cs;

        public override string ConnectionString
        {
            get
            {
                if (m_cs != null)
                    return this.m_cs;
                
                Npgsql.NpgsqlConnectionStringBuilder csb = new NpgsqlConnectionStringBuilder();
                csb.Host = "127.0.0.1";
                csb.Database = "blogz";
                csb.Port = 5432;
                csb.Username = "apws";
                csb.Password = "Test123";
                csb.IntegratedSecurity = false;
                csb.Pooling = true;
                csb.ApplicationName = "MyBlog";


                this.m_cs = csb.ConnectionString;
                return this.m_cs;
            }
        }

        protected delegate string OdbcFunctionReplacementCallback_t(System.Text.RegularExpressions.Match mThisMatch);
        
        protected OdbcFunctionReplacementCallback_t m_callback;
        
        protected override string OdbcFunctionReplacementCallback(System.Text.RegularExpressions.Match mThisMatch)
        {
            if(m_callback != null)
                return m_callback(mThisMatch);
            
            return "";
        }
        
        
        public AnyFactory()
            :base(default(T))
        {
            System.Type t = typeof(T);
            if(object.ReferenceEquals(t, typeof(NpgsqlFactory)))
                m_callback = pg_implements.OdbcFunctionReplacementCallback;
        }

    }

    
    
    
    
    
    
    
    
        internal class pg_implements
        {
            // Credits: http://web.archive.org/web/20100123183531/http://blogs.msdn.com/bclteam/archive/2005/03/15/396452.aspx
            internal const string strOdbcMatchingPattern = "({fn\\s*(.+?)\\s*\\(([^{}]*(((?<Open>{)[^{}]*)+((?<Close-Open>})[^{}]*)+)*(?(Open)(?!)))\\s*\\)\\s*})";


            
        // MsgBox(String.Join(Environment.NewLine, GetArguments("bla")));
        internal static string[] GetArguments(string strAllArguments)
        {
            string EscapeCharacter = System.Convert.ToChar(8).ToString();

            strAllArguments = strAllArguments.Replace("''", EscapeCharacter);

            bool bInString = false;
            int iLastSplitAt = 0;

            System.Collections.Generic.List<string> lsArguments = new System.Collections.Generic.List<string>();


            int iInFunction = 0;

            for (int i = 0; i < strAllArguments.Length; i++)
            {
                char strCurrentChar = strAllArguments[i];

                if (strCurrentChar == '\'')
                    bInString = !bInString;


                if (bInString)
                    continue;


                if (strCurrentChar == '(')
                    iInFunction += 1;


                if (strCurrentChar == ')')
                    iInFunction -= 1;



                if (strCurrentChar == ',')
                {

                    if (iInFunction == 0)
                    {
                        string strExtract = "";
                        if (iLastSplitAt != 0)
                        {
                            strExtract = strAllArguments.Substring(iLastSplitAt + 1, i - iLastSplitAt - 1);
                        }
                        else
                        {
                            strExtract = strAllArguments.Substring(iLastSplitAt, i - iLastSplitAt);
                        }

                        strExtract = strExtract.Replace(EscapeCharacter, "''");
                        lsArguments.Add(strExtract);
                        iLastSplitAt = i;
                    } // End if (iInFunction == 0)

                } // End if (strCurrentChar == ',')

            } // Next i


            string strExtractLast = "";
            if (lsArguments.Count > 0)
            {
                strExtractLast = strAllArguments.Substring(iLastSplitAt + 1);
            }
            else
            {
                strExtractLast = strAllArguments.Substring(iLastSplitAt);
            }

            strExtractLast = strExtractLast.Replace(EscapeCharacter, "''");
            lsArguments.Add(strExtractLast);

            string[] astrResult = lsArguments.ToArray();
            lsArguments.Clear();
            lsArguments = null;

            return astrResult;
        } // End Function GetArguments

            
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





                string[] astrArguments = GetArguments(strArguments);

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

    
    
    
    
    
    
    
    public abstract class SqlFactory 
    {

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
                ,offset.ToString(System.Globalization.CultureInfo.InvariantCulture) 
                ," ROWS \r\nFETCH NEXT " 
                ,rows.ToString(System.Globalization.CultureInfo.InvariantCulture) 
                ," ROWS ONLY \r\n" 
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
            string EscapeCharacter = System.Convert.ToChar(8).ToString();

            strAllArguments = strAllArguments.Replace("''", EscapeCharacter);

            bool bInString = false;
            int iLastSplitAt = 0;

            System.Collections.Generic.List<string> lsArguments = new System.Collections.Generic.List<string>();


            int iInFunction = 0;

            for (int i = 0; i < strAllArguments.Length; i++)
            {
                char strCurrentChar = strAllArguments[i];

                if (strCurrentChar == '\'')
                    bInString = !bInString;


                if (bInString)
                    continue;


                if (strCurrentChar == '(')
                    iInFunction += 1;


                if (strCurrentChar == ')')
                    iInFunction -= 1;



                if (strCurrentChar == ',')
                {

                    if (iInFunction == 0)
                    {
                        string strExtract = "";
                        if (iLastSplitAt != 0)
                        {
                            strExtract = strAllArguments.Substring(iLastSplitAt + 1, i - iLastSplitAt - 1);
                        }
                        else
                        {
                            strExtract = strAllArguments.Substring(iLastSplitAt, i - iLastSplitAt);
                        }

                        strExtract = strExtract.Replace(EscapeCharacter, "''");
                        lsArguments.Add(strExtract);
                        iLastSplitAt = i;
                    } // End if (iInFunction == 0)

                } // End if (strCurrentChar == ',')

            } // Next i


            string strExtractLast = "";
            if (lsArguments.Count > 0)
            {
                strExtractLast = strAllArguments.Substring(iLastSplitAt + 1);
            }
            else
            {
                strExtractLast = strAllArguments.Substring(iLastSplitAt);
            }

            strExtractLast = strExtractLast.Replace(EscapeCharacter, "''");
            lsArguments.Add(strExtractLast);

            string[] astrResult = lsArguments.ToArray();
            lsArguments.Clear();
            lsArguments = null;

            return astrResult;
        } // End Function GetArguments

        // Credits: http://web.archive.org/web/20100123183531/http://blogs.msdn.com/bclteam/archive/2005/03/15/396452.aspx
        protected const string strOdbcMatchingPattern = "({fn\\s*(.+?)\\s*\\(([^{}]*(((?<Open>{)[^{}]*)+((?<Close-Open>})[^{}]*)+)*(?(Open)(?!)))\\s*\\)\\s*})";
        

        public virtual string ReplaceOdbcFunctions(string sql)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return sql;
            }
            
            string retValue = System.Text.RegularExpressions.Regex.Replace(sql, strOdbcMatchingPattern, OdbcFunctionReplacementCallback);
            return retValue;
        } // End Function ReplaceOdbcFunctions
        
        
    }


    public class SqlClientFactory
        : SqlFactory
    {

        private static int s_seed;
        private static readonly System.Threading.ThreadLocal<System.Random> s_random;

        protected string m_connectionString;
        protected string[] m_connectionStrings;
        protected int m_connectionCount;
        
        protected delegate string GetConnectionString_t();
        protected GetConnectionString_t m_GetInternalConnectionString;


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


        static SqlClientFactory()
        {
            s_seed = System.Environment.TickCount;
            s_random = new System.Threading.ThreadLocal<System.Random>(GetRandom);
        }



        public SqlClientFactory(System.Data.Common.DbProviderFactory factory, params string[] connectionStrings)
            :base(factory)
        {
            this.Factory = factory;
            SetConnectionStrings(connectionStrings);
        }


        public SqlClientFactory(params string[] connectionStrings)
            : this(System.Data.SqlClient.SqlClientFactory.Instance, connectionStrings)
        { }


        public SqlClientFactory()
         : this((string[])null)
        { }


        public override string ConnectionString
        {
            get
            {
                return this.m_GetInternalConnectionString();
            }
            
        }


        
        
        protected override string OdbcFunctionReplacementCallback(System.Text.RegularExpressions.Match mThisMatch)
        {
            // Get the matched string.
            string strExpression = mThisMatch.Groups[1].Value;
            string strFunctionName = mThisMatch.Groups[2].Value;
            string strArguments = mThisMatch.Groups[3].Value;

            if (System.Text.RegularExpressions.Regex.IsMatch(strArguments, strOdbcMatchingPattern))
            {
                strArguments = System.Text.RegularExpressions.Regex.Replace(strArguments, strOdbcMatchingPattern, OdbcFunctionReplacementCallback);
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





            string[] astrArguments = GetArguments(strArguments);

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
                string strTerm = "SUBSTRING(" + astrArguments[0] + " FROM CHAR_LENGTH(" + astrArguments[0] + " ) - ( " + astrArguments[1] + " ) + 1 ) ";
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




    public class NewDal
    {

        public static async System.Threading.Tasks.Task Test()
        {
            SqlFactory fac = new SqlClientFactory();
            // DbConnection.ProviderFactory => DbProviderFactory; 

            using (System.Data.Common.DbConnection con = fac.Connection)
            {
                string sql = "SELECT * FROM T_BlogPost";
                string sql_paged = sql += fac.PagingTemplate(3, 2);
                string sql_limited = sql += fac.PagingTemplate(1);

                IEnumerable<BlogController.T_BlogPost> a = con.Query<BlogController.T_BlogPost>(sql);
                IEnumerable<BlogController.T_BlogPost> aa = await con.QueryAsync<BlogController.T_BlogPost>(sql_paged);

                BlogController.T_BlogPost b = con.QuerySingle<BlogController.T_BlogPost>(sql_limited);
                BlogController.T_BlogPost ba = await con.QuerySingleAsync<BlogController.T_BlogPost>(sql_limited);
            } // End Using con 

        } // End Sub Test 


    } // End Class NewDal 
    
    
} // End Namespace MyBlog.Controllers 

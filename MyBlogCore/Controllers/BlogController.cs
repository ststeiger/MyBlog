using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;


// using System.Web.Mvc;


// meinerettung.ch/felix
namespace MyBlogCore.Controllers
{


    public class MyDal
    {


        public System.Data.IDbCommand CreateCommand(string sql)
        {
            return null;
        }


        public System.Data.IDbCommand CreateCommand()
        {
            return null;
        }


        public virtual System.Data.IDbCommand CreateCommand()
        {
            return CreateCommand("");
        } // End Function CreateCommand

        /*
        public virtual System.Data.IDbCommand CreateCommand(string strSQL)
        {
            System.Data.IDbCommand idbc = this.m_providerFactory.CreateCommand();
            idbc.CommandText = strSQL;
			
            return idbc;
        } // End Function CreateCommand
        */


        // From Type to DBType
        protected virtual System.Data.DbType GetDbType(System.Type type)
        {
            // http://social.msdn.microsoft.com/Forums/en/winforms/thread/c6f3ab91-2198-402a-9a18-66ce442333a6
            string strTypeName = type.Name;
            System.Data.DbType DBtype = System.Data.DbType.String; // default value

            try
            {
                if (object.ReferenceEquals(type, typeof(System.DBNull)))
                {
                    return DBtype;
                }

                if (object.ReferenceEquals(type, typeof(System.Byte[])))
                {
                    return System.Data.DbType.Binary;
                }

                DBtype = (System.Data.DbType)System.Enum.Parse(typeof(System.Data.DbType), strTypeName, true);

                // Es ist keine Zuordnung von DbType UInt64 zu einem bekannten SqlDbType vorhanden.
                // http://msdn.microsoft.com/en-us/library/bbw6zyha(v=vs.71).aspx
                if (DBtype == System.Data.DbType.UInt64)
                    DBtype = System.Data.DbType.Int64;
            }
            catch (System.Exception)
            {
                // add error handling to suit your taste
            }

            return DBtype;
        } // End Function GetDbType


        protected virtual string SqlTypeFromDbType(System.Data.DbType type)
        {
            string strRetVal = null;

            // http://msdn.microsoft.com/en-us/library/cc716729.aspx
            switch (type)
            {
                case System.Data.DbType.Guid:
                    strRetVal = "uniqueidentifier";
                    break;
                case System.Data.DbType.Date:
                    strRetVal = "date";
                    break;
                case System.Data.DbType.Time:
                    strRetVal = "time(7)";
                    break;
                case System.Data.DbType.DateTime:
                    strRetVal = "datetime";
                    break;
                case System.Data.DbType.DateTime2:
                    strRetVal = "datetime2";
                    break;
                case System.Data.DbType.DateTimeOffset:
                    strRetVal = "datetimeoffset(7)";
                    break;

                case System.Data.DbType.StringFixedLength:
                    strRetVal = "nchar(MAX)";
                    break;
                case System.Data.DbType.String:
                    strRetVal = "nvarchar(MAX)";
                    break;

                case System.Data.DbType.AnsiStringFixedLength:
                    strRetVal = "char(MAX)";
                    break;
                case System.Data.DbType.AnsiString:
                    strRetVal = "varchar(MAX)";
                    break;

                case System.Data.DbType.Single:
                    strRetVal = "real";
                    break;
                case System.Data.DbType.Double:
                    strRetVal = "float";
                    break;
                case System.Data.DbType.Decimal:
                    strRetVal = "decimal(19, 5)";
                    break;
                case System.Data.DbType.VarNumeric:
                    strRetVal = "numeric(19, 5)";
                    break;

                case System.Data.DbType.Boolean:
                    strRetVal = "bit";
                    break;
                case System.Data.DbType.SByte:
                case System.Data.DbType.Byte:
                    strRetVal = "tinyint";
                    break;
                case System.Data.DbType.Int16:
                    strRetVal = "smallint";
                    break;
                case System.Data.DbType.Int32:
                    strRetVal = "int";
                    break;
                case System.Data.DbType.Int64:
                    strRetVal = "bigint";
                    break;
                case System.Data.DbType.Xml:
                    strRetVal = "xml";
                    break;
                case System.Data.DbType.Binary:
                    strRetVal = "varbinary(MAX)"; // or image
                    break;
                case System.Data.DbType.Currency:
                    strRetVal = "money";
                    break;
                case System.Data.DbType.Object:
                    strRetVal = "sql_variant";
                    break;

                case System.Data.DbType.UInt16:
                case System.Data.DbType.UInt32:
                case System.Data.DbType.UInt64:
                    throw new System.NotImplementedException("Uints not mapped - MySQL only");
            }

            return strRetVal;
        } // End Function SqlTypeFromDbType


        public static string JoinArray<T>(string separator, T[] inputTypeArray)
        {
            return JoinArray<T>(separator, inputTypeArray, object.ReferenceEquals(typeof(T), typeof(string)));
        }


        public static string JoinArray<T>(string separator, T[] inputTypeArray, bool sqlEscapeString)
        {
            string strRetValue = null;
            System.Collections.Generic.List<string> ls = new System.Collections.Generic.List<string>();

            for (int i = 0; i < inputTypeArray.Length; ++i)
            {
                string str = System.Convert.ToString(inputTypeArray[i], System.Globalization.CultureInfo.InvariantCulture);

                if (!string.IsNullOrEmpty(str))
                {
                    // SQL-Escape
                    if (sqlEscapeString)
                        str = str.Replace("'", "''");

                    ls.Add(str);
                } // End if (!string.IsNullOrEmpty(str))

            } // Next i 

            strRetValue = string.Join(separator, ls.ToArray());
            ls.Clear();
            ls = null;

            return strRetValue;
        } // End Function JoinArray


        public virtual void AddArrayParameter<T>(System.Data.IDbCommand command, string strParameterName, params T[] values)
        {
            if (values == null)
                return;

            if (!strParameterName.StartsWith("@"))
                strParameterName = "@" + strParameterName;

            string strSqlInStatement = JoinArray<T>(",", values);

            command.CommandText = command.CommandText.Replace(strParameterName, strSqlInStatement);
        } // End Function AddArrayParameter


        public virtual System.Data.IDbDataParameter AddParameter(System.Data.IDbCommand command, string strParameterName, object objValue)
        {
            return AddParameter(command, strParameterName, objValue, System.Data.ParameterDirection.Input);
        } // End Function AddParameter


        public virtual System.Data.IDbDataParameter AddParameter(System.Data.IDbCommand command, string strParameterName, object objValue, System.Data.ParameterDirection pad)
        {
            if (objValue == null)
            {
                //throw new ArgumentNullException("objValue");
                objValue = System.DBNull.Value;
            } // End if (objValue == null)

            System.Type tDataType = objValue.GetType();
            System.Data.DbType dbType = GetDbType(tDataType);

            return AddParameter(command, strParameterName, objValue, pad, dbType);
        } // End Function AddParameter


        public virtual System.Data.IDbDataParameter AddParameter(System.Data.IDbCommand command, string strParameterName, object objValue, System.Data.ParameterDirection pad, System.Data.DbType dbType)
        {
            System.Data.IDbDataParameter parameter = command.CreateParameter();

            if (!strParameterName.StartsWith("@"))
            {
                strParameterName = "@" + strParameterName;
            } // End if (!strParameterName.StartsWith("@"))

            parameter.ParameterName = strParameterName;
            parameter.DbType = dbType;
            parameter.Direction = pad;

            // Es ist keine Zuordnung von DbType UInt64 zu einem bekannten SqlDbType vorhanden.
            // No association  DbType UInt64 to a known SqlDbType

            if (objValue == null)
                parameter.Value = System.DBNull.Value;
            else
                parameter.Value = objValue;

            command.Parameters.Add(parameter);
            return parameter;
        } // End Function AddParameter


        public virtual System.Data.IDbCommand CreateCommand(string strSQL)
        {
            System.Data.IDbCommand idbc = this.m_providerFactory.CreateCommand();
            idbc.CommandTimeout = 300;

            idbc.CommandText = ReplaceOdbcFunctions(strSQL);

            return idbc;
        } // End Function CreateCommand


        public virtual System.Data.IDbCommand CreateLimitedCommand(string strSQL, int iLimit)
        {
            strSQL = string.Format(strSQL, "", "LIMIT " + iLimit.ToString());
            return CreateCommand(strSQL);
        }


        public virtual System.Data.IDbCommand CreateLimitedOdbcCommand(string strSQL, int iLimit)
        {
            strSQL = ReplaceOdbcFunctions(strSQL);
            return CreateLimitedCommand(strSQL, iLimit);
        }


        public System.Data.IDbCommand CreatePagedCommand(string strSQL, int ulngStartIndex, int ulngEndIndex)
        {
            return CreatePagedCommand(strSQL, (ulong)ulngStartIndex, (ulong)ulngEndIndex);
        }


        public virtual System.Data.IDbCommand CreatePagedCommand(string strSQL, ulong ulngPageNumber, ulong ulngPageSize)
        {
            ulong ulngStartIndex = ((ulngPageSize * ulngPageNumber) - ulngPageSize) + 1;
            ulong ulngEndIndex = ulngStartIndex + ulngPageSize - 1;

            strSQL += " " + System.Environment.NewLine + "OFFSET " + ulngStartIndex.ToString() + " " + System.Environment.NewLine + "LIMIT " + ulngPageSize.ToString() + " ";

            //OFFSET @ulngStartIndex 
            //LIMIT (@ulngEndIndex - @ulngStartIndex) 

            System.Data.IDbCommand cmd = this.CreateCommand(strSQL);

            // fsck... {fn NOW()}
            // cmd.CommandText = string.Format(strSQL, " /* TOP 1 */ ", "OFFSET 0 FETCH NEXT 1 ROWS ONLY");

            this.AddParameter(cmd, "ulngStartIndex", ulngStartIndex);
            this.AddParameter(cmd, "ulngEndIndex", ulngEndIndex);
            return cmd;
        }


        public virtual System.Data.IDbCommand CreateLimitedCommand(string strSQL, int iLimit)
        {
            strSQL = string.Format(strSQL, "", "LIMIT " + iLimit.ToString());
            return CreateCommand(strSQL);
        }

        public System.Data.IDbDataParameter AddParameter(System.Data.IDbCommand command, string strParameterName, object objValue)
        {
            return null; // AddParameter(command, strParameterName, objValue, System.Data.ParameterDirection.Input);
        } // End Function AddParameter


        //protected const System.Reflection.BindingFlags m_CaseSensitivity = System.Reflection.BindingFlags.IgnoreCase;
        protected const System.Reflection.BindingFlags m_CaseSensitivity = System.Reflection.BindingFlags.Instance
            | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.IgnoreCase
            ;


        public static System.Reflection.MemberInfo GetMemberInfo(System.Type t, string strName)
        {
            System.Reflection.MemberInfo mi = t.GetField(strName, m_CaseSensitivity);

            if (mi == null)
                mi = t.GetProperty(strName, m_CaseSensitivity);

            return mi;
        } // End Function GetMemberInfo


        public static bool IsNullable(System.Type t)
        {
            if (t == null)
                return false;

            return t.IsGenericType && object.ReferenceEquals(t.GetGenericTypeDefinition(), typeof(System.Nullable<>));
        } // End Function IsNullable


        public static object MyChangeType(object objVal, System.Type t)
        {
            bool typeIsNullable = IsNullable(t);
            bool typeCanBeAssignedNull = !t.IsValueType || typeIsNullable;

            if (objVal == null || object.ReferenceEquals(objVal, System.DBNull.Value))
            {
                if (typeCanBeAssignedNull)
                    return null;
                else
                    throw new System.ArgumentNullException("objVal ([DataSource] => SetProperty => MyChangeType => you're trying to NULL a type that NULL cannot be assigned to...)");
            }

            //getbasetype
            System.Type tThisType = objVal.GetType();

            if (typeIsNullable)
            {
                t = System.Nullable.GetUnderlyingType(t);
            }


            if (object.ReferenceEquals(tThisType, t))
                return objVal;

            // Convert Guid => string 
            if (object.ReferenceEquals(t, typeof(string)) && object.ReferenceEquals(tThisType, typeof(System.Guid)))
            {
                return objVal.ToString();
            }

            // Convert string => Guid 
            if (object.ReferenceEquals(t, typeof(System.Guid)) && object.ReferenceEquals(tThisType, typeof(string)))
            {
                return new System.Guid(objVal.ToString());
            }

            return System.Convert.ChangeType(objVal, t);
        } // End Function MyChangeType

        public static void SetMemberValue(object obj, System.Reflection.MemberInfo mi, object objValue)
        {
            if (mi is System.Reflection.FieldInfo)
            {
                System.Reflection.FieldInfo fi = (System.Reflection.FieldInfo)mi;
                fi.SetValue(obj, MyChangeType(objValue, fi.FieldType));
                return;
            }

            if (mi is System.Reflection.PropertyInfo)
            {
                System.Reflection.PropertyInfo pi = (System.Reflection.PropertyInfo)mi;
                pi.SetValue(obj, MyChangeType(objValue, pi.PropertyType), null);
                return;
            }

            // Else silently ignore value
        } // End Sub SetMemberValue


        public virtual T GetClass<T>(System.Data.IDbCommand cmd, T tThisClassInstance)
        {
            System.Type t = typeof(T);

            lock (cmd)
            {
                using (System.Data.IDataReader idr = ExecuteReader(cmd))
                {

                    lock (idr)
                    {

                        while (idr.Read())
                        {

                            for (int i = 0; i < idr.FieldCount; ++i)
                            {
                                string strName = idr.GetName(i);
                                object objVal = idr.GetValue(i);

                                System.Reflection.MemberInfo mi = GetMemberInfo(t, strName);
                                SetMemberValue(tThisClassInstance, mi, objVal);

                                /*
                                System.Reflection.FieldInfo fi = t.GetField(strName, m_CaseSensitivity);
                                if (fi != null)
                                    fi.SetValue(tThisClassInstance, MyChangeType(objVal, fi.FieldType));
                                else
                                {
                                    System.Reflection.PropertyInfo pi = t.GetProperty(strName, m_CaseSensitivity);
                                    if (pi != null)
                                        pi.SetValue(tThisClassInstance, MyChangeType(objVal, pi.PropertyType), null);

                                } // End else of if (fi != null)
                                */
                            } // Next i

                            break;
                        } // Whend

                        idr.Close();
                    } // End Lock idr

                } // End Using idr

            } // End lock cmd

            return tThisClassInstance;
        } // End Function GetClass

        public virtual T GetClass<T>(System.Data.IDbCommand cmd)
        {
            T tThisClassInstance = System.Activator.CreateInstance<T>();
            return GetClass<T>(cmd, tThisClassInstance);
        }


        public virtual T ExecuteScalar<T>(System.Data.IDbCommand cmd)
        {
            T tThisClassInstance = System.Activator.CreateInstance<T>();
            return tThisClassInstance; 
        }


        public static bool Log(object obj)
        {
            return Log(obj, (string)null);
        } // End Sub Log(object obj)


        public static bool Log(object obj, System.Data.IDbCommand cmd)
        {
            string strSQL = "";
            foreach (System.Data.IDbDataParameter para in cmd.Parameters)
            {
                strSQL += para.ParameterName + ":\t" + para.Value.ToString() + "" + System.Environment.NewLine;
            } // Next para

            strSQL += System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine;

            strSQL += cmd.CommandText;

            return Log("", obj, strSQL);
        } // End Sub Log


        public static bool Log(object obj, string strSQL)
        {
            return Log("", obj, strSQL);
        } // End Sub Log


        public static bool Log(string strLocation, object obj, string strSQL)
        {
            return Log(strLocation, obj, strSQL, true);
        } // End Sub Log


        public static bool Log(string strLocation, object obj, string strSQL, bool bRethrow)
        {
            if (!string.IsNullOrEmpty(strLocation))
            {
                System.Console.WriteLine("Error in " + strLocation + ": " + System.Environment.NewLine + System.Environment.NewLine);
            } // End if (!string.IsNullOrEmpty(strLocation))

            if (!string.IsNullOrEmpty(strSQL))
            {
                System.Console.WriteLine("SQL statement: " + System.Environment.NewLine + strSQL + System.Environment.NewLine + System.Environment.NewLine);
            } // End if(!string.IsNullOrEmpty(strSQL))

            if (obj != null)
            {
                if (obj.GetType().ToString().EndsWith("Exception"))
                {
                    System.Exception ex = (System.Exception)obj;
                    string strMessage = "";
                    if (ex != null)
                    {
                        strMessage = ex.Message;
                        if (ex.InnerException != null)
                        {
                            strMessage += System.Environment.NewLine;
                            strMessage += ex.InnerException.Message + System.Environment.NewLine + System.Environment.NewLine;
                            strMessage += "Inner stacktrace: " + System.Environment.NewLine
                                + ex.InnerException.StackTrace + System.Environment.NewLine
                                + System.Environment.NewLine;
                        } // End if (ex.InnerException != null)
                        strMessage += System.Environment.NewLine + System.Environment.NewLine + "Stacktrace: " + ex.StackTrace + System.Environment.NewLine;
                    } // End if (ex != null)

                    System.Console.WriteLine(strMessage);
                    strMessage = null;
                } // End if(obj.GetType().ToString().EndsWith("Exception"))
                else
                    System.Console.WriteLine(obj.ToString());
            } // End if (obj != null)
            else
                System.Console.WriteLine("obj = null");


            try
            {
                System.Console.WriteLine(System.Environment.NewLine + System.Environment.NewLine);
                System.Console.WriteLine(new string('=', System.Console.WindowWidth));
                System.Console.WriteLine(System.Environment.NewLine + System.Environment.NewLine);
            }
            catch //(Exception ex)
            {

            }

            return bRethrow;
        } // End Sub Log


        public virtual System.Data.IDbConnection GetConnection()
        {
            return GetConnection(null);
        }

        protected System.Data.Common.DbProviderFactory m_providerFactory = null;

        public System.Data.IDbConnection GetConnection(string strDb)
        {
            System.Data.Common.DbConnection con = m_providerFactory.CreateConnection();
            con.ConnectionString = GetConnectionString(strDb);

            return con;
        }


        public virtual System.Data.IDataReader ExecuteReader(System.Data.IDbCommand cmd, System.Data.CommandBehavior cmdBehaviour)
        {
            System.Data.IDataReader idr = null;

            lock (cmd)
            {
                System.Data.IDbConnection idbc = GetConnection();
                cmd.Connection = idbc;

                if (cmd.Connection.State != System.Data.ConnectionState.Open)
                    cmd.Connection.Open();

                try
                {
                    idr = cmd.ExecuteReader(cmdBehaviour);
                }
                catch (System.Exception ex)
                {
                    if (Log(ex, cmd))
                        throw;
                }
            } // End Lock cmd

            return idr;
        } // End Function ExecuteReader


        public virtual System.Data.IDataReader ExecuteReader(System.Data.IDbCommand cmd)
        {
            return ExecuteReader(cmd, System.Data.CommandBehavior.CloseConnection);
        } // End Function ExecuteReader


        public virtual System.Data.IDataReader ExecuteReader(string strSQL, System.Data.CommandBehavior cmdBehaviour)
        {
            System.Data.IDataReader idr = null;

            using (System.Data.IDbCommand cmd = this.CreateCommand(strSQL))
            {
                idr = ExecuteReader(cmd, cmdBehaviour);
            } // End Using cmd

            return idr;
        } // End Function ExecuteReader


        public virtual System.Data.IDataReader ExecuteReader(string strSQL)
        {
            return ExecuteReader(strSQL, System.Data.CommandBehavior.CloseConnection);
        } // End Function ExecuteReader


        public virtual bool IsSimpleType(System.Type tThisType)
        {

            if (tThisType.IsPrimitive)
            {
                return true;
            }

            if (object.ReferenceEquals(tThisType, typeof(System.String)))
            {
                return true;
            }

            if (object.ReferenceEquals(tThisType, typeof(System.DateTime)))
            {
                return true;
            }

            if (object.ReferenceEquals(tThisType, typeof(System.Guid)))
            {
                return true;
            }

            if (object.ReferenceEquals(tThisType, typeof(System.Decimal)))
            {
                return true;
            }

            if (object.ReferenceEquals(tThisType, typeof(System.Object)))
            {
                return true;
            }

            return false;
        } // End Function IsSimpleType

        public System.Collections.Generic.List<T> GetList<T>(System.Data.IDbCommand cmd)
        {
            System.Collections.Generic.List<T> lsReturnValue = new System.Collections.Generic.List<T>();
            T tThisValue = default(T);
            System.Type tThisType = typeof(T);

            lock (cmd)
            {
                using (System.Data.IDataReader idr = ExecuteReader(cmd))
                {

                    lock (idr)
                    {


                        if (IsSimpleType(tThisType))
                        {
                            while (idr.Read())
                            {
                                object objVal = idr.GetValue(0);
                                tThisValue = (T)MyChangeType(objVal, typeof(T));
                                //tThisValue = System.Convert.ChangeType(objVal, T),

                                lsReturnValue.Add(tThisValue);
                            } // End while (idr.Read())

                        }
                        else
                        {
                            int myi = idr.FieldCount;

                            System.Reflection.FieldInfo[] fis = new System.Reflection.FieldInfo[idr.FieldCount];
                            //Action<T, object>[] setters = new Action<T, object>[idr.FieldCount];

                            for (int i = 0; i < idr.FieldCount; ++i)
                            {
                                string strName = idr.GetName(i);
                                System.Reflection.FieldInfo fi = tThisType.GetField(strName, m_CaseSensitivity);
                                fis[i] = fi;

                                //if (fi != null)
                                //    setters[i] = GetSetter<T>(fi);
                            } // Next i


                            while (idr.Read())
                            {
                                //idr.GetOrdinal("")
                                tThisValue = System.Activator.CreateInstance<T>();

                                // Console.WriteLine(idr.FieldCount);
                                for (int i = 0; i < idr.FieldCount; ++i)
                                {
                                    string strName = idr.GetName(i);
                                    object objVal = idr.GetValue(i);

                                    //System.Reflection.FieldInfo fi = t.GetField(strName, m_CaseSensitivity);
                                    if (fis[i] != null)
                                    //if (fi != null)
                                    {
                                        //fi.SetValue(tThisValue, System.Convert.ChangeType(objVal, fi.FieldType));
                                        fis[i].SetValue(tThisValue, MyChangeType(objVal, fis[i].FieldType));
                                    } // End if (fi != null) 
                                    else
                                    {
                                        System.Reflection.PropertyInfo pi = tThisType.GetProperty(strName, m_CaseSensitivity);
                                        if (pi != null)
                                        {
                                            //pi.SetValue(tThisValue, System.Convert.ChangeType(objVal, pi.PropertyType), null);
                                            pi.SetValue(tThisValue, MyChangeType(objVal, pi.PropertyType), null);
                                        } // End if (pi != null)

                                        // Else silently ignore value
                                    } // End else of if (fi != null)

                                    //Console.WriteLine(strName);
                                } // Next i

                                lsReturnValue.Add(tThisValue);
                            } // Whend

                        } // End if IsSimpleType(tThisType)

                        idr.Close();
                    } // End Lock idr

                } // End Using idr

            } // End lock cmd

            return lsReturnValue;
        } // End Function GetList


        public  void Insert<T>(object objInsertValue)
        {
        } // End Sub Insert


    }



    public class BlogController : Controller
    {

        protected MyDal m_dal;


        public IActionResult IndexABC()
        {
            // return View();
            int num1 = 1;
            int num2 = 2;
            int result = 3;
            return Content($"Result of {num1} + {num2} is {result}", "text/plain");
        }

        public class cSearchResult
        {
            public cSearchResult() { }
            public cSearchResult(string q)
            {
                this.searched_for = q;
            } // End Constructor 


            public string searched_for;
            public System.Collections.Generic.List<T_BlogPost> searchResults;
        } // End Class cSearchResult 


        /*
        ;WITH CTE AS 
        (
	              select 'hello|123' as abc
            union select 'hello+456' as abc
	        union select 'hello-456' as abc
	        union select 'hello~456' as abc
	        union select 'hello!456' as abc
	        union select 'hello*456' as abc
	        union select 'hello#456' as abc
	        union select 'hello#456' as abc
            union select 'hel[l]o-456' as abc 
        )
        SELECT * FROM CTE 
        WHERE abc ILIKE '%\h\e\l\l\o%' ESCAPE '\'
        */

        // WHERE col LIKE @str ESCAPE '\' 
        public static string LikeEscape(string str)
        {
            // WHERE last_name LIKE 'M%!%' ESCAPE '\';
            // http://www.postgresql.org/docs/9.0/static/functions-matching.html
            // # & / ~ ~* !~ !~* ^ $ ,  ; : * .

            // http://www.postgresql.org/docs/9.3/static/sql-syntax-lexical.html
            // Operators: + - * / < > = ~ ! @ # % ^ & | ` ?
            // string specialCharacters = @"\_%+-|[]()?~!*#";

            string strRet = null;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (char c in str)
            {
                sb.Append('\\');
                sb.Append(c);
            } // Next c 

            strRet = sb.ToString();
            sb.Length = 0;
            sb = null;
            return strRet;
        } // End Function LikeEscape


        public JsonResult Search(string q)
        {
            cSearchResult SearchResult = new cSearchResult(q);

            System.Collections.Generic.List<T_BlogPost> ls;
            using (System.Data.IDbCommand cmd = this.m_dal.CreateCommand(@"
SELECT 
	 BP_UID
    ,BP_Title
    ,BP_Content
    ,BP_CreoleText
    ,BP_BBCode
    ,BP_HtmlContent
    ,BP_EntryDate
FROM T_BlogPost 
WHERE (1=1) 
AND 
(
    {fn ILIKE(BP_Title, @searchtext )} 
    OR 
    {fn ILIKE(BP_Content, @searchtext )} 
)
"))
            {
                this.m_dal.AddParameter(cmd, "@searchtext", "%" + LikeEscape(q) + "%");
                ls = this.m_dal.GetList<T_BlogPost>(cmd);
            } // End Using cmd 

            SearchResult.searchResults = ls;

            // return Json(SearchResult, JsonRequestBehavior.AllowGet);
            return Json(SearchResult, new Newtonsoft.Json.JsonSerializerSettings());
        } // End Action Search

        // Content(strHTML, "text/html");


        public class BlogIndex
        {
            public System.Collections.Generic.IList<T_BlogPost> lsBlogEntries;
        } // End Class BlogIndex 


        public void UpdateBlogStructure(IList<T_BlogPost> lsBlogEntries)
        {
            T_BlogPost bp;
            foreach (T_BlogPost bpThisPost in lsBlogEntries)
            {
                bp = bpThisPost;
                string strHTML = ReplaceURLs(bp.BP_Content);

                strHTML = strHTML.Replace("\r\n", "\n");
                strHTML = strHTML.Replace("\n", "<br />");

                bp.BP_HtmlContent = strHTML;
                this.m_dal.Insert<T_BlogPost>(bp);
            } // Next bpThisPost 

        } // End Sub UpdateBlogStructure 


        public static object objlock = new object();
        public static int iCount = 0;

        public ActionResult UploadFiles()
        {
            /*
            lock (objlock)
            {
                iCount++;

                string meth = Request.HttpMethod;
                string str = string.Join(Environment.NewLine + Environment.NewLine + Environment.NewLine, Request.Params);

                foreach (string strKey in Request.Files)
                {
                    System.Web.HttpPostedFileBase pfb = Request.Files[strKey];

                    string SaveToPath = @"c:\temp";
                    SaveToPath = System.IO.Path.Combine(SaveToPath, pfb.FileName);

                    string mime = pfb.ContentType;
                    long size = pfb.ContentLength;

                    pfb.SaveAs(SaveToPath);
                } // Next strKey

                Console.WriteLine(iCount);
                return Content(Request.Params["index"]);
            } // End lock (objlock)
            */

            return null;
        } // End Action UploadFiles 


        public ActionResult Upload()
        {
            return View();
        } // End Action Upload


        public ContentResult Preview(string id, string q)
        {
            q = System.Uri.UnescapeDataString(q);
            // string strHTML = RenderUtils.RenderMarkdown (q);
            // string strHTML = RenderUtils.RenderBbCode(q);
            string strHTML = MyBlog.RenderUtils.RenderMediaWikiMarkup(q);

            return Content(strHTML, "text/html", System.Text.Encoding.UTF8);
        } // End Action Upload


        //
        // GET: /Blog/
        public ActionResult Index()
        {
            BlogIndex bi = new BlogIndex();
            // bi.lsBlogEntries = this.m_dal.GetList<T_BlogPost>(@"");

            using (System.Data.IDbCommand cmd = this.m_dal.CreateLimitedCommand(@"
SELECT {0} 
	 T_BlogPost.*
	,row_number() OVER (ORDER BY BP_EntryDate DESC) AS rownum 
FROM T_BlogPost 
ORDER BY BP_EntryDate DESC
", 100))
            {
                bi.lsBlogEntries = this.m_dal.GetList<T_BlogPost>(cmd);
            }


            // UpdateBlogStructure(bi.lsBlogEntries);

            return View(bi);
        } // End Action Index


        //
        // GET: /Blog/NewEntry
        public ActionResult NewEntry()
        {
            return View();
        } // End Action NewEntry


        public class T_BlogPost
        {
            public Guid BP_UID = System.Guid.NewGuid();
            public Guid BP_Author_USR_UID = System.Guid.Empty;


            public string BP_Title;
            public string BP_Content;
            public string BP_HtmlContent;

            public string BP_Excerpt;
            public string BP_ExcerptHTML;

            public string BP_CommentCount;

            public Guid BP_PostType; // Post, Comment FollowUp


            public DateTime BP_EntryDate = System.DateTime.UtcNow;
            public DateTime BP_LastModifiedDate = System.DateTime.UtcNow;
        } // End Class T_BlogPost


        //
        // POST: /Blog/Create
        [HttpPost]
        public ActionResult AddEntry(string txtTitle, string taBody)
        {
            try
            {
                // TODO: Add insert logic here
                T_BlogPost bp = new T_BlogPost();

                bp.BP_Title = txtTitle; // Request.Params["txtTitle"];
                bp.BP_Title = taBody; // Request.Params["taBody"];

                // this.m_dal.Insert<T_BlogPost>(bp);
                return RedirectToAction("Success");
            }
            catch(System.Exception ex)
            {
                // return View();
                return Json(new { success = false, responseText = ex.Message, stackTrace = ex.StackTrace });
                
            }
        } // End Action AddEntry


        //
        // POST: /Blog/Create
        [HttpPost]
        public ActionResult UpdateEntry(string hdnBP_UID, string txtTitle, string taBody)
        {
            try
            {
                // TODO: Add insert logic here
                T_BlogPost bp = new T_BlogPost();

                //bp.BP_UID = (Guid) System.Convert.ChangeType(Request.Params["hdnBP_UID"], typeof(Guid));
                bp.BP_UID = new System.Guid(hdnBP_UID);

                bp.BP_Title = txtTitle; // Request.Params["txtTitle"];
                bp.BP_Content = taBody; // Request.Params["taBody"];

                // this.m_dal.Insert<T_BlogPost>(bp);
                return RedirectToAction("Success");
            }
            catch
            {
                // return View();
                int num1 = 1;
                int num2 = 2;
                int result = 3;
                return Content($"Result of {num1} + {num2} is {result}", "text/plain");
            }
        } // End Action AddEntry


        //
        // GET: /Blog/Success/5
        public ActionResult Success(string id)
        {
            return View();
        } // End Action Success


        // string str = ReplaceURLs("http://www.google.com/ncr?abc=def#ghi");
        public static string ReplaceURLs(string strPlainText)
        {
            string strPattern = @"((http|ftp|https|[a-zA-Z]):(//|\\)([a-zA-Z0-9\\\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?)|(www\.([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?)";

            strPlainText = System.Text.RegularExpressions.Regex.Replace(strPlainText, strPattern,
             delegate (System.Text.RegularExpressions.Match ma)
             {
                 string url = ma.Groups[0].Value;
                 if (System.Text.RegularExpressions.Regex.IsMatch(url, @"^[a-zA-Z]:\\")) // Starts with drive letter
                 {
                     return string.Format("<a target=\"_blank\" href=\"file:///{0}\">{1}</a>", url.Replace(@"\", "/"), url);
                 }

                 return string.Format("<a target=\"_blank\" href=\"{0}\">{0}</a>", url);
             }
            );

            return strPlainText;
        } // End Function ReplaceURLs 


        public static string GetMachineId()
        {
            string macAddresses = "";

            foreach (System.Net.NetworkInformation.NetworkInterface nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                //if (nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                //{
                //    macAddresses += nic.GetPhysicalAddress().ToString();
                //    break;
                //}

                if (nic.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Loopback)
                {
                    if (nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                    {
                        macAddresses += nic.GetPhysicalAddress().ToString();
                        break;
                    }
                }

            } // Next nic

            return macAddresses;
        } // End Function GetMachineId 


        //
        // GET: /Blog/Delete/5
        public ActionResult ShowEntry(string id)
        {
            T_BlogPost bp = null;


            string lol = "http://localhost/image.aspx?&postimage_text=%0A%5Burl%3Dhttp%3A%2F%2Fpostimg.org%2Fimage%2Fu0zc6aznf%2F%5D%5Bimg%5Dhttp%3A%2F%2Fs1.postimg.org%2Fu0zc6aznf%2Fhtc_hero_wallpaper_03.jpg%5B%2Fimg%5D%5B%2Furl%5D%0A";
            //"http://localhost/image.aspx?&postimage_text=[url=http://postimg.org/image/u0zc6aznf/][img]http://s1.postimg.org/u0zc6aznf/htc_hero_wallpaper_03.jpg[/img][/url]


            lol = System.Web.HttpUtility.UrlDecode(lol);
            Console.WriteLine(lol);


            using (System.Data.IDbCommand cmd = this.m_dal.CreateLimitedCommand("SELECT {0} BP_UID FROM T_BlogPost ORDER BY BP_EntryDate DESC;", 1))
            {
                id = this.m_dal.ExecuteScalar<string>(cmd);
            } // End Using cmd 


            using (System.Data.IDbCommand cmd = this.m_dal.CreateCommand("SELECT * FROM T_BlogPost WHERE BP_UID = @__bp_uid"))
            {
                this.m_dal.AddParameter(cmd, "__bp_uid", new System.Guid(id));
                bp = this.m_dal.GetClass<T_BlogPost>(cmd);
            } // End Using cmd

            bp.BP_Content = ReplaceURLs(bp.BP_Content);


            // http://stackoverflow.com/questions/16389234/create-dropdown-with-predefined-values-in-asp-net-mvc-3-using-razor-view/16389278#16389278


            ViewData["myList"] =
                new SelectList(new[] { "10", "15", "25", "50", "100", "1000" }
                .Select(x => new { value = x, text = x }),
                "value", "text", "15");

            List<SelectListItem> ls = new List<SelectListItem>();

            ls.Add(new SelectListItem() { Text = "Yes", Value = "true", Selected = true });
            ls.Add(new SelectListItem() { Text = "No", Value = "false", Selected = false });
            ls.Add(new SelectListItem() { Text = "Not Applicable", Value = "NULL", Selected = false });

            ViewData["myList"] = ls;

            ViewData["myList"] = new[] { "10", "15", "25", "50", "100", "1000" }
               .Select(x => new SelectListItem
               {
                   Selected = x == "25",
                   Text = x,
                   Value = x
               });

            ViewData["myList"] =
                new SelectList(new[] { "10", "15", "25", "50", "100", "1000" }
                .Select(x => new SelectListItem { Value = x, Text = x }),
                "Value", "Text", "15");

            ViewData["myList"] =
                from c in new[] { "10", "15", "25", "50", "100", "1000" }
                select new SelectListItem
                {
                    Selected = (c == "25"),
                    Text = c,
                    Value = c
                };

            return View(bp);
        } // End Action ShowEntry


        //
        // GET: /Blog/Delete/5
        public ActionResult EditEntry(string id)
        {
            T_BlogPost bp = null;

            using (System.Data.IDbCommand cmd = this.m_dal.CreateLimitedCommand("SELECT {0} BP_UID FROM T_BlogPost ORDER BY BP_EntryDate DESC;", 1))
            {
                id = this.m_dal.ExecuteScalar<string>(cmd);
            } // End Using cmd 

            using (System.Data.IDbCommand cmd = this.m_dal.CreateCommand("SELECT * FROM T_BlogPost WHERE BP_UID = @__bp_uid"))
            {
                this.m_dal.AddParameter(cmd, "__bp_uid", new System.Guid(id));
                bp = this.m_dal.GetClass<T_BlogPost>(cmd);
            } // End Using cmd

            return View(bp);
        } // End Action EditEntry 


    } // End Class BlogController : Controller 


} // End Namespace MyBlog.Controllers 

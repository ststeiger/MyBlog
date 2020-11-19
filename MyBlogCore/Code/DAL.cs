
using System.Collections.Generic;
using Dapper;
using MyBlogCore.Controllers;


namespace MyBlogCore
{
    
    
    public abstract class BaseFactory
    {
        
        protected System.Data.Common.DbProviderFactory Factory;
        
        
        public virtual string PagingTemplate(ulong offset, ulong rows)
        {
            return @"OFFSET "+ offset.ToString(System.Globalization.CultureInfo.InvariantCulture) + @" ROWS 
FETCH NEXT " + rows.ToString(System.Globalization.CultureInfo.InvariantCulture) + " ROWS ONLY ";    
        }
        
        
        public string CS
        {
            get { return ""; }
        }
        
        
        protected BaseFactory(System.Data.Common.DbProviderFactory fac)
        {
            this.Factory = fac;
        }
        
    }


    public class SqlFactory 
        : BaseFactory
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




    public class NewDal
    {

        public static async void foo()
        {
            
            
            System.Data.Common.DbConnection con = new System.Data.SqlClient.SqlConnection("Server=localhost,2017;User=sa;Password=Pass123!;Database=basics;");
            
            // DbProviderFactory ProviderFactory => DbProviderFactory; 
            
            
            
            BlogController.T_BlogPost x = await con.QuerySingleAsync<BlogController.T_BlogPost>("select * from T_BlogPost");
            BlogController.T_BlogPost b = con.QuerySingle<BlogController.T_BlogPost>("select * from T_BlogPost");
            IEnumerable<BlogController.T_BlogPost> a = con.Query<BlogController.T_BlogPost>("select * from T_BlogPost");
        }

    }


    public class MyDal
    {

        
        //protected const System.Reflection.BindingFlags m_CaseSensitivity = System.Reflection.BindingFlags.IgnoreCase;
        protected const System.Reflection.BindingFlags m_CaseSensitivity = System.Reflection.BindingFlags.Instance
                                                                           | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.IgnoreCase
            ;


        public static string GetConnectionString(string foo)
        {
            return null;
        }



        protected System.Data.Common.DbProviderFactory m_providerFactory = null;

        public System.Data.IDbConnection GetConnection(string strDb)
        {
            System.Data.Common.DbConnection con = m_providerFactory.CreateConnection();
            con.ConnectionString = GetConnectionString(strDb);

            return con;
        }

        public virtual System.Data.IDbConnection GetConnection()
        {
            return GetConnection(null);
        }

        

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


        private static string ReplaceOdbcFunctions(string sql)
        {
            return sql;
        }
        
        
        public virtual System.Data.IDbCommand CreateCommand(string strSQL)
        {
            System.Data.IDbCommand idbc = this.m_providerFactory.CreateCommand();
            idbc.CommandTimeout = 300;

            idbc.CommandText = ReplaceOdbcFunctions(strSQL);
            
            return idbc;
        } // End Function CreateCommand


        public virtual System.Data.IDbCommand CreateCommand()
        {
            return CreateCommand("");
        } // End Function CreateCommand
        
        
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
    
    
} // End Namespace MyBlog.Controllers 

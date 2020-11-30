
namespace OnlineYournal
{


    public static class DbCommandExtensions
    {


        // From Type to DBType
        private static System.Data.DbType GetDbType(System.Type type)
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


        //public static System.Data.IDbDataParameter AddWithValue(this System.Data.IDbCommand cmd, string parameterName, object value)
        //{
        //    return AddWithValue((System.Data.Common.DbCommand)cmd, parameterName, value, null);
        //}


        public static System.Data.IDbDataParameter AddWithValue(this System.Data.IDbCommand cmd, string parameterName, object value, System.Data.DbType? dbType = null)
        {
            if (value == null)
            {
                value = System.DBNull.Value;
            } // End if (objValue == null)

            if (!dbType.HasValue)
            {
                dbType = GetDbType(value.GetType());
            }

            System.Data.IDbDataParameter p = cmd.CreateParameter();
            p.ParameterName = parameterName;
            p.DbType = dbType.Value;
            p.Value = value;

            cmd.Parameters.Add(p);

            return p;
        }


    } // End Class DbParameterCollectionExtensions 


} // End Namespace SSRS_Manager 


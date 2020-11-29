
namespace OnlineYournal 
{


    public static class DbParameterCollectionExtensions 
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


        public static System.Data.IDbDataParameter AddWithValue(this System.Data.IDataParameterCollection col, string parameterName, object value)
        {
            return AddWithValue((System.Data.Common.DbParameterCollection)col, parameterName, value);
        }


        public static System.Data.Common.DbParameter AddWithValue(this System.Data.Common.DbParameterCollection col, string parameterName, object value) 
        {
            if (value == null)
            {
                value = System.DBNull.Value;
            } // End if (objValue == null)

            System.Type tDataType = value.GetType();

            int i = col.Add(value); 
            col[i].ParameterName = parameterName;
            col[i].DbType = GetDbType(tDataType);

            return col[i]; 
        } // End Function AddWithValue 


    } // End Class DbParameterCollectionExtensions 


} // End Namespace OnlineYournal 

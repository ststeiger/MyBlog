
namespace Dapper
{


    public static class InsertExtensions
    {



        //public static string[] ToNameArray(this System.Reflection.MemberInfo[] arrMi)
        public static string[] ToNameArray(System.Reflection.MemberInfo[] arrMi)
        {
            string[] arr = new string[arrMi.Length];

            for (int i = 0; i < arrMi.Length; ++i)
            {
                arr[i] = arrMi[i].Name;
            } // Next i

            return arr;
        } // End Extension Function System.Reflection.MemberInfo[].ToNameArray


        public static bool Contains(System.Collections.Generic.List<string> ls, string strValueToSearch, System.StringComparer cmp)
        {
            foreach (string strThisValue in ls)
            {
                if (cmp.Equals(strThisValue, strValueToSearch))
                    return true;
            } // Next strThisValue

            return false;
        } // End Extension Function List<string>.Contains


        public static string EscapeTableName(string strTableName)
        {
            return "\"" + strTableName.Replace("\"", "\"\"") + "\"";
        }





        public static void Insert<T>(this System.Data.IDbConnection con, object objInsertValue)
        {
            System.Type tTypeToInsert = typeof(T);
            Insert(con, tTypeToInsert, objInsertValue, tTypeToInsert.Name);
        } // End Sub InsertClassProfiles


        public static void Insert<T>(this System.Data.IDbConnection con, object objInsertValue, string strTableName)
        {
            System.Type tt = typeof(T);
            Insert(con, tt, objInsertValue, strTableName);
        } // End Sub InsertClassProfiles


        public static void Insert(this System.Data.IDbConnection con, System.Type tTypeToInsert, object objInsertValue)
        {
            Insert(con, tTypeToInsert, objInsertValue, tTypeToInsert.Name);
        }





        //typeof(cProfile)
        public static void Insert(this System.Data.IDbConnection con, System.Type tTypeToInsert
            , object objInsertValue, string strTableName)
        {
            System.Reflection.FieldInfo[] fields = tTypeToInsert.GetFields();
            System.Reflection.PropertyInfo[] properties = tTypeToInsert.GetProperties();


            //string[] astrFieldNames = fields.Select(c => c.Name).ToArray();
            //string[] astrFieldNames = fields.ToNameArray();
            string[] astrFieldNames = ToNameArray(fields);
            //string[] astrPropertyNames = properties.Select(c => c.Name).ToArray();
            //string[] astrPropertyNames = properties.ToNameArray();
            string[] astrPropertyNames = ToNameArray(properties);

            System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, System.Type>> lsFieldNames =
                new System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, System.Type>>();


            foreach (string strFieldName in astrFieldNames)
            {
                lsFieldNames.Add(new System.Collections.Generic.KeyValuePair<string, System.Type>
                    (strFieldName, typeof(System.Reflection.FieldInfo)));
            }

            foreach (string strPropertyName in astrPropertyNames)
            {
                lsFieldNames.Add(new System.Collections.Generic.KeyValuePair<string, System.Type>
                    (strPropertyName, typeof(System.Reflection.PropertyInfo)));
            }

            string strSQL = @"
SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = @__tableName
ORDER BY ORDINAL_POSITION 
";

            System.Collections.Generic.List<string> astrDbFields = System.Linq.Enumerable.ToList(
                con.Query<string>(strSQL, new { __tableName = strTableName })
            );


            for (int i = lsFieldNames.Count - 1; i > -1; --i)
            {
                //if (!astrDbFields.Contains(lsFieldNames[i].Key, StringComparer.OrdinalIgnoreCase))
                if (!Contains(astrDbFields, lsFieldNames[i].Key, System.StringComparer.OrdinalIgnoreCase))
                    lsFieldNames.Remove(lsFieldNames[i]);
            }

            strSQL = "INSERT INTO " + EscapeTableName(strTableName) + "( " + System.Environment.NewLine;
            for (int i = 0; i < lsFieldNames.Count; ++i)
            {
                strSQL += i == 0 ? "     " : "    ,";
                strSQL += "    " + lsFieldNames[i].Key + System.Environment.NewLine;
            } // Next i
            strSQL += ") " + System.Environment.NewLine;

            strSQL += "VALUES ( " + System.Environment.NewLine;

            for (int i = 0; i < lsFieldNames.Count; ++i)
            {
                strSQL += i == 0 ? "     " : "    ,";
                strSQL += "@__in_" + lsFieldNames[i].Key + System.Environment.NewLine;
            } // Next i
            strSQL += ") " + System.Environment.NewLine;

            DynamicParameters dbArgs = new DynamicParameters();




            for (int i = 0; i < lsFieldNames.Count; ++i)
            {
                object objValue = null;

                if (object.ReferenceEquals(lsFieldNames[i].Value, typeof(System.Reflection.FieldInfo)))
                    objValue = tTypeToInsert.GetField(lsFieldNames[i].Key).GetValue(objInsertValue);
                else if (object.ReferenceEquals(lsFieldNames[i].Value, typeof(System.Reflection.PropertyInfo)))
                    objValue = tTypeToInsert.GetProperty(lsFieldNames[i].Key).GetValue(objInsertValue, null);
                else
                    throw new System.Exception("No such type or property '" + lsFieldNames[i].Key + "'");

                //object objValue = Profile.GetValue(lsFieldNames[i]);
                dbArgs.Add("__in_" + lsFieldNames[i].Key, objValue);
            } // Next i


            string strPK = astrDbFields[0];

            string sq = "DELETE FROM " + EscapeTableName(strTableName) + " WHERE " + EscapeTableName(strPK) + " = @__in_pk";

            object objPK = null;
            if (object.ReferenceEquals(lsFieldNames[0].Value, typeof(System.Reflection.FieldInfo)))
                objPK = tTypeToInsert.GetField(lsFieldNames[0].Key).GetValue(objInsertValue);
            else if (object.ReferenceEquals(lsFieldNames[0].Value, typeof(System.Reflection.PropertyInfo)))
                objPK = tTypeToInsert.GetProperty(lsFieldNames[0].Key).GetValue(objInsertValue, null);
            else
                throw new System.Exception("No such type or property '" + lsFieldNames[0].Key + "'");

            con.Execute(sq, new { __in_pk = objPK });

            con.Execute(strSQL, dbArgs);
        } // End Sub InsertClassProfiles


    }


}

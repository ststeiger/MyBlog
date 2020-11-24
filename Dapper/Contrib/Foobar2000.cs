
namespace Dapper.Contrib
{


    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class WriteAttribute 
        : System.Attribute
    {
        /// <summary>
        /// Specifies whether a field is writable in the database.
        /// </summary>
        /// <param name="write">Whether a field is writable in the database.</param>
        public WriteAttribute(bool write)
        {
            Write = write;
        }

        /// <summary>
        /// Whether a field is writable in the database.
        /// </summary>
        public bool Write { get; }
    }



    /// <summary>
    /// Specifies that this field is a primary key in the database
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class KeyAttribute 
        : System.Attribute
    {


        public KeyAttribute(string name, params string[] fields)
        {
            this.Name = name;
            this.Fields = fields;
        }


        public KeyAttribute(string name)
            : this(name, null)
        { }

        public string Name;
        public string[] Fields;

    }



    /// <summary>
    /// Defines the name of a table to use in Dapper.Contrib commands.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class TableAttribute 
        : System.Attribute
    {

        /// <summary>
        /// The name of the table in the database
        /// </summary>
        public string Schema { get; set; }
        public string Name { get; set; }


        /// <summary>
        /// Creates a table mapping to a specific name for Dapper.Contrib commands
        /// </summary>
        /// <param name="tableSchema">The name of the schema of this table in the database.</param>
        /// <param name="tableName">The name of this table in the database.</param>
        public TableAttribute(string tableSchema, string tableName)
        {
            Schema = tableSchema;
            Name = tableName;
        }


        /// <summary>
        /// Creates a table mapping to a specific name for Dapper.Contrib commands
        /// </summary>
        /// <param name="tableName">The name of this table in the database.</param>
        public TableAttribute(string tableName)
            :this("dbo", tableName)
        { }

    }

    static class Foobar2000
    {

        private static  readonly char[] s_vowels = new char[] { 'a','e', 'i', 'o', 'u', 'ä' ,'ö', 'ü' };
        private static bool IsVowel(char c)
        {
            c = char.ToLowerInvariant(c);
            if (System.Array.IndexOf(s_vowels, c) > -1)
                return true;
            
            return false;
        }

        private static void foo()
        {
            // In some cases, singular nouns ending in -s or -z, require that you double the -s or -z prior to adding the -es for pluralization.
            
            //  If the noun ends with ‑f or ‑fe, the f is often changed to ‑ve before adding the -s to form the plural version.
            string[] ff = new string[] {"f","fe" };
            // If the singular noun ends in ‑s, -ss, -sh, -ch, -x, or -z, add ‑es to the end to make it plural.
            string[] foo = new string[]{ "s","ss","sh","ch","x","z"};
            
            //  If a singular noun ends in ‑y and the letter before the -y is a consonant, change the ending to ‑ies to make the noun plural.
            //  If the singular noun ends in -y and the letter before the -y is a vowel, simply add an -s to make it plural.
            // https://github.com/ststeiger/Pluralize.NET.Core
        }

        private static bool IsWriteable(System.Reflection.PropertyInfo pi)
        {
            var attributes = pi.GetCustomAttributes(typeof(WriteAttribute), false).AsList();
            if (attributes.Count != 1) return true;

            var writeAttribute = (WriteAttribute)attributes[0];
            return writeAttribute.Write;
        }


        public static bool Gaga<T>(T con) where T : System.Data.IDbConnection
        {
            System.Type t = typeof(T);
            System.Reflection.PropertyInfo[] pis = t.GetProperties();


            System.Reflection.PropertyInfo[] readProperties = System.Linq.Enumerable.ToArray(
                System.Linq.Enumerable.Where(pis, x => x.CanRead));


            System.Reflection.PropertyInfo[] writeProperties = System.Linq.Enumerable.ToArray(
                System.Linq.Enumerable.Where(pis, x => x.CanWrite));


            return true;
        }



        public static bool IsReservedKeyword(System.Data.IDbConnection con, string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                throw new System.ArgumentNullException(nameof(objectName));

            if (con is System.Data.SqlClient.SqlConnection)
            {
                return ms_specific.ReservedKeywords.Contains(objectName);
            }
            else if (con is Npgsql.NpgsqlConnection)
            {
                return pg_specific.ReservedKeywords.Contains(object);
            }

            return false;
        }


        public static bool IsReservedKeyword(string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                throw new System.ArgumentNullException(nameof(objectName));

            return false;
        }

        public static bool ObjectNameNeedsEscaping(string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                throw new System.ArgumentNullException(nameof(objectName));

            if (string.IsNullOrEmpty(objectName))
                return false;

            if (IsReservedKeyword(objectName))
                return true;

            if (objectName.StartsWith("@"))
                return true;


            // The underscore (_), at sign (@), or number sign (#).
            // %^&({}+-/ ]['''
            char[] lsIllegalCharacters = "+-*/%<>=&|^(){}[]\"'´`\t\n\r \\,.;?!~¨¦§°¢£€".ToCharArray();

            for (int i = 0; i < lsIllegalCharacters.Length; ++i)
            {
                if (objectName.IndexOf(lsIllegalCharacters[i]) != -1)
                    return true;
            }

            return false;
        } // End Function MustEscape 


        public static string QuoteObject(string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                throw new System.ArgumentNullException(nameof(objectName));

            return "\"" + objectName.Replace("\"", "\"\"") + "\"";
        }


        public static string QuoteObjectWhereNecessary(string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                throw new System.ArgumentNullException(nameof(objectName));

            if (ObjectNameNeedsEscaping(objectName))
                return QuoteObject(objectName);

            return objectName;
        } // End Function QuoteObjectWhereNecessary


    }


}

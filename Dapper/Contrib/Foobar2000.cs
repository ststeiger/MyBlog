
namespace Dapper.Contrib
{


    public class TableInfo
    {
        public bool NeedsIdentityInsert;

        public string TableSchema;
        public string TableName;

        public string SelectStatement;
        public string InsertStatement;
        public string UpdateStatement;
        public string UpsertStatement;
        // insert into dummy(id, name, size) values(1, 'new_name', 3)
        // on conflict on constraint dummy_pkey
        // do update set name = 'new_name', size = 4;

        // ON CONFLICT (id) DO NOTHING

        // INSERT INTO b(pk_b, b, comment)
        // SELECT pk_a, a, comment FROM a
        // ON CONFLICT(pk_b) DO UPDATE  -- conflict is on the unique column
        // SET b = excluded.b;            -- key word "excluded", refer to target column


        // https://stackoverflow.com/questions/19007884/import-xml-files-to-postgresql/33211885#33211885
        /* 
        
CREATE TABLE IF NOT EXISTS public.foobar
(
    myid int NOT NULL,
    mytesttext character varying(200),
 
    CONSTRAINT pk_foobar PRIMARY KEY (myid)
);



;WITH CTE AS 
(
	SELECT 
	     (xpath('//ID/text()', myTempTable.myXmlColumn))[1]::text::int AS id
	    ,(xpath('//Name/text()', myTempTable.myXmlColumn))[1]::text::character varying(200) AS mytext 
	FROM unnest(xpath('//record', 
	 CAST('<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
	<data-set>
	    <record>
		<ID>1</ID>
		<Name>A</Name>
		<RFC>RFC 1035[1]</RFC>
		<Text>Address record</Text>
		<Desc>Returns a 32-bit IPv4 address, most commonly used to map hostnames to an IP address of the host, but it is also used for DNSBLs, storing subnet masks in RFC 1101, etc.</Desc>
	    </record>
	    <record>
		<ID>2</ID>
		<Name>NS</Name>
		<RFC>RFC 1035[1]</RFC>
		<Text>Name server record</Text>
		<Desc>Delegates a DNS zone to use the given authoritative name servers</Desc>
	    </record>
	</data-set>
	' AS xml)   
	)) AS myTempTable(myXmlColumn)
)
-- SELECT * FROM CTE 
INSERT INTO public.foobar(myid, mytesttext)
SELECT id, mytext FROM CTE 
ON CONFLICT(myid) DO UPDATE 
	SET mytesttext = excluded.mytesttext; -- key word "excluded", refer to target column


-- SELECT * FROM foobar

-- DROP TABLE public.foobar;

        */


        public string[] Columns;
        public string[] KeyColumns;

        public string InsertColumns;
        public string SelectColumns;


        public System.Action<object>[] Setters;
        public System.Action<object>[] Getters;

    }


    public class TableInfoCache<TableType>
    {
        public static object s_lock = new object();
        public static TableInfo Instance;


        private static Dapper.Contrib.Pluralize.Pluralizer plu = new Dapper.Contrib.Pluralize.Pluralizer();


        private static string Singularize(string input)
        {
            return plu.Singularize(input);
        }


        private static string Pluralize(string input)
        {
            return plu.Pluralize(input);
        }


        public static void Create(System.Data.Common.DbProviderFactory factory)
        {
            System.Type t = typeof(TableType);

            lock (s_lock)
            {
                if (TableInfoCache<TableType>.Instance != null)
                {
                    return;
                }

                TableInfoCache<TableType>.Instance = new TableInfo();
            }
        }

        public static void Create<T>() where T : System.Data.Common.DbProviderFactory
        {
            System.Data.Common.DbProviderFactory fact = System.Activator.CreateInstance<T>();
            Create(fact);
        }


    }


    public abstract class BaseTable
    { 
        public virtual string PK { get; set; }



        public virtual void SetValue(int index, object value)
        {
            System.Reflection.PropertyInfo[] pis = null;


            // pis[i].SetValue(this, value);
        }


        public virtual void SetValue(string columnName, object value)
        {
            System.Reflection.PropertyInfo[] pis = null;

            // pis[i].SetValue(this, value);
        }



    }


    [Table("blog", "posts")]
    public class Post 
        : BaseTable
    {
        [IdentityInsert()]
        public int Id { get; set; }

        
        // DB: bla_name
        public int BlaName { get; set; }


        public string _id { get; } // Computed column 

    }



    static class Foobar2000
    {
        

        public static int Ins<T>(this System.Data.IDbConnection cnn
            , T entity
            , System.Data.IDbTransaction transaction = null
            , int? commandTimeout = null
            , System.Data.CommandType? commandType = null)
        {
            int ret = -1;

            // string sql = "INSERT INTO T VALUES (@A, @B)";
            string sql = TableInfoCache<T>.Instance.InsertStatement;
            ret = cnn.Execute(sql, entity);
            return ret;
        }


        public static int Ins<T>(this System.Data.IDbConnection cnn
            , System.Collections.Generic.IEnumerable<T> entities
            , int? commandTimeout = null
            , System.Data.CommandType? commandType = null)
        {
            int ret = -1;
            bool wasOpen = false;

            if (cnn.State != System.Data.ConnectionState.Open)
                cnn.Open();
            else
                wasOpen = true;

            System.Data.IDbTransaction transaction = cnn.BeginTransaction();

            // string sql = "INSERT INTO T VALUES (@A, @B)";
            string sql = TableInfoCache<T>.Instance.InsertStatement;
            try
            {
                ret = cnn.Execute(sql, entities, transaction);
                transaction.Commit();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Commit Exception Type: {0}", ex.GetType().FullName);
                System.Console.WriteLine("  Message: {0}", ex.Message);

                // Attempt to roll back the transaction. 
                try
                {
                    transaction.Rollback();
                }
                catch (System.Exception ex2)
                {
                    // This catch block will handle any errors that may have occurred 
                    // on the server that would cause the rollback to fail, such as a closed connection.
                    System.Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType().FullName);
                    System.Console.WriteLine("  Message: {0}", ex2.Message);
                }

                throw;
            }
            finally
            {
                if (!wasOpen && cnn.State != System.Data.ConnectionState.Closed)
                    cnn.Close();
            }

            return ret;
        }


        public static T GetCustomAttribute<T>(System.Reflection.MemberInfo mi)
        {
            object[] objs = mi.GetCustomAttributes(typeof(T), false);

            if (objs == null || objs.Length < 1)
                return default(T);

            T attr = (T)objs[0];
            return attr;
        }

        public static System.Reflection.MemberInfo GetFieldOrProperty(System.Type t, string memberName, System.Reflection.BindingFlags flags)
        {
            System.Reflection.MemberInfo retValue = t.GetProperty(memberName, flags);
            if (retValue == null)
                retValue = t.GetField(memberName, flags);

            return retValue;
        }

        public static TAttribute GetMemberAttribute<T, TAttribute>(string memberName)
        {
            TAttribute attr = GetCustomAttribute<TAttribute>(
                GetFieldOrProperty(typeof(T), memberName, System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.IgnoreCase)
                );

            return attr;
        }


        public static void CheckAttribute<T>(string memberName)
        {
            KeyAttribute attr = GetMemberAttribute<T, KeyAttribute>(memberName);

            System.Console.WriteLine(attr.Name);
            System.Console.WriteLine(attr.Fields[0]);
        }


        private static bool IsWriteable(System.Reflection.PropertyInfo pi)
        {
            var attributes = pi.GetCustomAttributes(typeof(WriteAttribute), false).AsList();
            if (attributes.Count != 1) return true;

            var writeAttribute = (WriteAttribute)attributes[0];
            return writeAttribute.Write;
        }


        public static bool CheckPropertyForGetterAndSetter<T>(T con) where T : System.Data.IDbConnection
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
                return pg_specific.ReservedKeywords.Contains(objectName);
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

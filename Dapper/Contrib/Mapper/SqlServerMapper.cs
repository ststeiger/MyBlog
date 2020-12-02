
namespace Dapper.Contrib.Mapper
{

    public class SqlServerMapper
          : Mapper
    {

        
        public override void FromAbstract()
        {
            System.Collections.Generic.Dictionary<string, string> dict = new System.Collections.Generic.Dictionary<string, string>(System.StringComparer.InvariantCultureIgnoreCase);

            dict.Add("boolean", "bit");
            dict.Add("int8", "tinyint");
            dict.Add("int16", "smallint");
            dict.Add("int32", "int");
            dict.Add("int64", "bigint");
            dict.Add("int128", "uniqueidentifier");

            dict.Add("float", "real");
            dict.Add("double", "float");

            dict.Add("numeric", "numeric");
            dict.Add("smallmoney", "smallmoney");
            dict.Add("money", "money");

            dict.Add("ansi", "char");
            dict.Add("unicode", "nchar");

            dict.Add("ansi_text", "varchar");
            dict.Add("unicode_text", "nvarchar");
            dict.Add("xml", "xml");


            dict.Add("small_datetime_without_timezone", "smalldatetime");
            dict.Add("datetime_without_timezone", "datetime2");
            dict.Add("datetime_with_timezone", "datetimeoffset");

            dict.Add("date", "date");

            dict.Add("time_without_timezone", "time");
            dict.Add("timestamp", "timestamp");

            dict.Add("byte", "binary");
            dict.Add("bytes", "varbinary");

            dict.Add("filestream", "filestream");
            dict.Add("any", "sql_variant");

        }


        public override void ToAbstract()
        {
            System.Collections.Generic.Dictionary<string, string> dict = new System.Collections.Generic.Dictionary<string, string>(System.StringComparer.InvariantCultureIgnoreCase);

            string sql = @"
SELECT * FROM sys.types 
WHERE schema_id = 4 
ORDER BY name 
";

            dict.Add("bit", "boolean");
            dict.Add("tinyint", "int8");
            dict.Add("smallint", "int16");
            dict.Add("int", "int32");
            dict.Add("bigint", "int64");
            dict.Add("uniqueidentifier", "int128");

            dict.Add("real", "float");
            dict.Add("float", "double");

            dict.Add("numeric", "numeric");
            dict.Add("decimal", "numeric");
            dict.Add("smallmoney", "smallmoney");
            dict.Add("money", "money");

            dict.Add("char", "ansi");
            dict.Add("nchar", "unicode");

            dict.Add("varchar", "ansi_text");
            dict.Add("nvarchar", "unicode_text");
            dict.Add("text", "ansi_text");
            dict.Add("ntext", "unicode_text");
            dict.Add("sysname", "unicode_text");
            dict.Add("xml", "xml");

            // https://www.sqlservercentral.com/articles/datetime-vs-datetime2
            // https://stackoverflow.com/questions/1334143/datetime2-vs-datetime-in-sql-server/1334193
            // datetime: 0.00333 seconds accuracy 
            // datetime2 & datetimeoffset: 100 nanoseconds
            // date: 1 day
            // time: 100 nanoseconds 
            dict.Add("smalldatetime", "small_datetime_without_timezone");
            dict.Add("datetime", "datetime_without_timezone");
            dict.Add("datetime2", "datetime_without_timezone");
            dict.Add("datetimeoffset", "datetime_with_timezone");

            dict.Add("date", "date");

            dict.Add("time", "time");
            dict.Add("timestamp", "timestamp");

            dict.Add("binary", "byte");

            dict.Add("varbinary", "bytes");
            dict.Add("image", "bytes");

            dict.Add("filestream", "filestream");
            dict.Add("sql_variant", "any");

            // geography
            // geometry
            // hierarchyid
        }
    }


}

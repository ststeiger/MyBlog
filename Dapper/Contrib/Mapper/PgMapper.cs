
namespace Dapper.Contrib.Mapper
{


    public class PgMapper
        : Mapper
    {

        public override void FromAbstract()
        {
            System.Collections.Generic.Dictionary<string, string> dict = new System.Collections.Generic.Dictionary<string, string>(System.StringComparer.InvariantCultureIgnoreCase);

            dict.Add("boolean", "boolean");
            dict.Add("int8", "int2"); // can store 1 byte in 2 bytes
            dict.Add("int16", "int2");
            dict.Add("int32", "int");
            dict.Add("int64", "bigint");
            dict.Add("int128", "uuid");

            dict.Add("float", "real");
            dict.Add("double", "double precision");

            dict.Add("numeric", "numeric");
            dict.Add("smallmoney", "numeric");
            dict.Add("money", "numeric");

            dict.Add("ansi", "character");
            dict.Add("unicode", "national character");

            dict.Add("ansi_text", "character varying");
            dict.Add("unicode_text", "national character varying");
            dict.Add("xml", "xml");

            dict.Add("small_datetime_without_timezone", "time without time zone");
            dict.Add("time_without_timezone", "time without time zone");
            dict.Add("time_with_timezone", "time with time zone");
            dict.Add("timestamp", "timestamp");

            dict.Add("date", "date");

            dict.Add("byte", "bytea");
            dict.Add("bytes", "bytea");

            // https://wiki.postgresql.org/wiki/BinaryFilesInDB
            // https://blog.dalibo.com/2015/01/26/Extension_BFILE_pour_PostgreSQL.html
            // https://github.com/darold/external_file
            // https://stackoverflow.com/questions/4386030/how-to-use-blob-datatype-in-postgres
            dict.Add("filestream", "BFILE");
            dict.Add("any", "any");

        }

        public override void ToAbstract()
        {
            System.Collections.Generic.Dictionary<string, string> dict = new System.Collections.Generic.Dictionary<string, string>(System.StringComparer.InvariantCultureIgnoreCase);

            string sql = @"
-- https://www.postgresql.org/docs/9.5/datatype.html
-- https://stackoverflow.com/a/16349665/155077

SELECT 
	 n.nspname AS Schema 
	,pg_catalog.format_type(t.oid, NULL) AS Name 
	,pg_catalog.obj_description(t.oid, 'pg_type') AS Description 
FROM pg_catalog.pg_type AS t 

LEFT JOIN pg_catalog.pg_namespace AS n 
	ON n.oid = t.typnamespace 

WHERE 
(
	t.typrelid = 0 
	OR 
	( 
		SELECT c.relkind = 'c' 
		FROM pg_catalog.pg_class AS c 
		WHERE c.oid = t.typrelid 
	) 
)
AND NOT EXISTS
(
	SELECT 1 FROM pg_catalog.pg_type AS el 
	WHERE el.oid = t.typelem 
	AND el.typarray = t.oid
)
AND pg_catalog.pg_type_is_visible(t.oid)

ORDER BY 1, 2 
; 
";

            dict.Add("any", "any"); // 
            dict.Add("char", "ansi"); // single character
            dict.Add("abstime", ""); // absolute, limited-range date and time (Unix system time)
            dict.Add("aclitem", ""); // access control list
            dict.Add("anyarray", ""); // 
            dict.Add("anyelement", ""); // 
            dict.Add("anyenum", ""); // 
            dict.Add("anynonarray", ""); // 
            dict.Add("anyrange", ""); // 
            dict.Add("bigint", "int64"); // ~18 digit integer, 8-byte storage
            dict.Add("bit", "boolean"); // fixed-length bit string
            dict.Add("bit varying", ""); // variable-length bit string
            dict.Add("boolean", "boolean"); // boolean, 'true'/'false'
            dict.Add("box", ""); // geometric box '(lower left,upper right)'
            dict.Add("bytea", "bytes"); // variable-length string, binary values escaped
            dict.Add("character", "ansi"); // char(length), blank-padded string, fixed storage length
            dict.Add("character varying", "ansi_text"); // varchar(length), non-blank-padded string, variable storage length
            dict.Add("national character", "unicode"); // char(length), blank-padded string, fixed storage length
            dict.Add("national character varying", "unicode_text"); // varchar(length), non-blank-padded string, variable storage length
            dict.Add("cid", ""); // command identifier type, sequence in transaction id
            dict.Add("cidr", ""); // network IP address/netmask, network address
            dict.Add("circle", ""); // geometric circle '(center,radius)'
            dict.Add("cstring", ""); // 

            dict.Add("date", "date"); // date

            dict.Add("daterange", ""); // range of dates
            dict.Add("double precision", "double"); // double-precision floating point number, 8-byte storage
            dict.Add("event_trigger", ""); // 
            dict.Add("fdw_handler", ""); // 
            dict.Add("gtsvector", ""); // GiST index internal text representation for text search
            dict.Add("index_am_handler", ""); // 
            dict.Add("inet", ""); // IP address/netmask, host address, netmask optional
            dict.Add("int2vector", ""); // array of int2, used in system tables
            dict.Add("int4range", ""); // range of integers
            dict.Add("int8range", ""); // range of bigints

            dict.Add("integer", "int32"); // -2 billion to 2 billion integer, 4-byte storage
            
            dict.Add("interval", "internal"); // @ <number> <units>, time interval

            dict.Add("json", ""); // 
            dict.Add("jsonb", ""); // Binary JSON
            dict.Add("language_handler", ""); // 
            dict.Add("line", ""); // geometric line
            dict.Add("lseg", ""); // geometric line segment '(pt1,pt2)'
            dict.Add("macaddr", ""); // XX:XX:XX:XX:XX:XX, MAC address
            dict.Add("money", "money"); // monetary amounts, $d,ddd.cc
            dict.Add("name", ""); // 63-byte type for storing system identifiers
            dict.Add("numeric", "numeric"); // numeric(precision, decimal), arbitrary precision number
            dict.Add("numrange", ""); // range of numerics
            dict.Add("oid", ""); // object identifier(oid), maximum 4 billion
            dict.Add("oidvector", ""); // array of oids, used in system tables
            dict.Add("opaque", ""); // 
            dict.Add("path", ""); // geometric path '(pt1,...)'
            dict.Add("pg_ddl_command", ""); // internal type for passing CollectedCommand
            dict.Add("pg_lsn", ""); // PostgreSQL LSN datatype
            dict.Add("pg_node_tree", ""); // string representing an internal node tree
            dict.Add("point", ""); // geometric point '(x, y)'
            dict.Add("polygon", ""); // geometric polygon '(pt1,...)'
            dict.Add("real", "float"); // single-precision floating point number, 4-byte storage
            dict.Add("record", ""); // 
            dict.Add("refcursor", ""); // reference to cursor (portal name)
            dict.Add("regclass", ""); // registered class
            dict.Add("regconfig", ""); // registered text search configuration
            dict.Add("regdictionary", ""); // registered text search dictionary
            dict.Add("regnamespace", ""); // registered namespace
            dict.Add("regoper", ""); // registered operator
            dict.Add("regoperator", ""); // registered operator (with args)
            dict.Add("regproc", ""); // registered procedure
            dict.Add("regprocedure", ""); // registered procedure (with args)
            dict.Add("regrole", ""); // registered role
            dict.Add("regtype", ""); // registered type
            dict.Add("reltime", ""); // relative, limited-range time interval (Unix delta time)
            dict.Add("smallint", "int16"); // -32 thousand to 32 thousand, 2-byte storage
            dict.Add("smgr", ""); // storage manager
            dict.Add("text", "ansi_text"); // variable-length string, no limit specified
            dict.Add("tid", ""); // (block, offset), physical location of tuple
            dict.Add("time with time zone", "time_with_timezone"); // time of day with time zone
            dict.Add("time without time zone", "time_without_timezone"); // time of day
            dict.Add("timestamp with time zone", "datetime_with_timezone"); // date and time with time zone
            dict.Add("timestamp without time zone", "datetime_without_timezone"); // date and time
            dict.Add("tinterval", ""); // (abstime,abstime), time interval
            dict.Add("trigger", ""); // 
            dict.Add("tsm_handler", ""); // 
            dict.Add("tsquery", ""); // query representation for text search
            dict.Add("tsrange", ""); // range of timestamps without time zone
            dict.Add("tstzrange", ""); // range of timestamps with time zone
            dict.Add("tsvector", ""); // text representation for text search
            dict.Add("txid_snapshot", ""); // txid snapshot
            dict.Add("unknown", ""); // 
            dict.Add("uuid", "int128"); // UUID datatype
            dict.Add("void", "void"); // 
            dict.Add("xid", ""); // transaction id
            dict.Add("xml", "xml"); // XML content

        }
    }


}

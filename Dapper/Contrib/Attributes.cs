
namespace Dapper.Contrib
{


    public enum ColumnTypes
    {
        undefined, 
        bit,
        tinyint,
        smallint,
        @int,
        bigint,
        uniqueidentifier,

        @float,
        real,
        smallmoney,
        money,
        numeric,
        @decimal,

        geography,
        geometry,
        hierarchyid,

        @char,
        nchar,
        text,
        ntext,
        varchar,
        nvarchar,
        sysname,
        xml,

        date,
        smalldatetime,
        datetime,
        datetime2,
        datetimeoffset,
        time,
        timestamp,

        binary,
        varbinary,
        image,
        sql_variant,
    }


    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class WriteAttribute
            : System.Attribute
    {

        /// <summary>
        /// Whether a field is writable in the database.
        /// </summary>
        public bool Write;


        /// <summary>
        /// Specifies whether a field is writable in the database.
        /// </summary>
        /// <param name="write">Whether a field is writable in the database.</param>
        public WriteAttribute(bool write)
        {
            Write = write;
        }

        
    } // End Class WriteAttribute 



    /// <summary>
    /// Specifies that this field is a primary key in the database
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class KeyAttribute
        : System.Attribute
    {


        public string Name;
        public string[] Fields;


        public KeyAttribute(string name, params string[] fields)
        {
            this.Name = name;
            this.Fields = fields;
        }


        public KeyAttribute(string name, [System.Runtime.CompilerServices.CallerMemberName] string field = null)
            : this(name, new string[] { field })
        { }

    } // End Class KeyAttribute 



    /// <summary>
    /// Specifies that this field is a primary key in the database
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class IdentityInsertAttribute
        : System.Attribute
    {

        public string Field;


        // https://stackoverflow.com/questions/4606973/how-to-get-name-of-property-which-our-attribute-is-set
        public IdentityInsertAttribute([System.Runtime.CompilerServices.CallerMemberName] string field = null)
        {
            this.Field = field;
        }

    } // End Class IdentityInsertAttribute 


    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class DefaultValueAttribute 
        : System.Attribute
    {
        
        public string DefaultValue;


        public DefaultValueAttribute(string defaultValue)
        {
            this.DefaultValue = defaultValue;
        }

    } // End Class DefaultValueAttribute 


    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class ColumnAttribute 
        : System.Attribute
    {

        public int OrdinalPosition;
        public string Name;


        public ColumnAttribute(int ordinal_position, string name, string dataType = null, bool nullable = true, int size = -1, int precision = -1, int scale = -1, string abstractType = null, bool isArray = false)
        {
            this.OrdinalPosition = ordinal_position;
            this.Name = name;
        }


        public ColumnAttribute(int ordinal_position, string name, ColumnTypes dataType = ColumnTypes.undefined, bool nullable = true, int size = -1, int precision = -1, int scale = -1, string abstractType = null, bool isArray = false)
            : this(ordinal_position, name, dataType.ToString())
        { }


        public ColumnAttribute(int ordinal_position, string name)
            : this(ordinal_position, name, null)
        { }

    } // End Class ColumnAttribute 


    /// <summary>
    /// Defines the name of a table to use in Dapper.Contrib commands.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class TableAttribute
        : System.Attribute
    {


        /// <summary>
        /// The name of the schema of the table in the database
        /// </summary>
        public string Schema;


        /// <summary>
        /// The name of the table in the database
        /// </summary>
        public string Name;


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
            : this("dbo", tableName)
        { }


    }


}

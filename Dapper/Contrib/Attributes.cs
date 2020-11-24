
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


        public KeyAttribute(string name, [System.Runtime.CompilerServices.CallerMemberName] string field = null)
            : this(name, new string[] { field })
        { }


        public string Name;
        public string[] Fields;

    }


    /// <summary>
    /// Specifies that this field is a primary key in the database
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class IdentityInsertAttribute
        : System.Attribute
    {


        // https://stackoverflow.com/questions/4606973/how-to-get-name-of-property-which-our-attribute-is-set
        public IdentityInsertAttribute([System.Runtime.CompilerServices.CallerMemberName] string field = null)
        {
            this.Field = field;
        }

        public string Field;

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
            : this("dbo", tableName)
        { }


    }


}

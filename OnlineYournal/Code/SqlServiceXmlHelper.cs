
using Dapper;


// namespace Dapper
namespace OnlineYournal.Code
{
    [System.Flags]
    public enum XmlRenderType_t : int
    {
        Default = 0,
        Indented = 1,
        DataTable = 2,
        Array = 4,
        Data_Only = 8,
        Columns_Associative = 16,
        Columns_ObjectArray = 32,
        WithDetail = 64,
        ShortName = 128,
        LongName = 256,
        AssemblyQualifiedName = 512
    }

    
        
        

    public class StringWriterWithEncoding
        : System.IO.StringWriter
    {
        private readonly System.Text.Encoding m_Encoding;


        public StringWriterWithEncoding(System.Text.StringBuilder sb, System.Text.Encoding encoding)
            : base(sb)
        {
            this.m_Encoding = encoding;
        }


        public override System.Text.Encoding Encoding
        {
            get { return this.m_Encoding; }
        }


    } // End Class StringWriterWithEncoding

    
    public static class SqlServiceXmlHelper
    {

        
        private static System.Xml.XmlWriter CreateXmlWriter(System.Text.StringBuilder builder)
        {
            System.Xml.XmlWriterSettings xs = new System.Xml.XmlWriterSettings();
            xs.Indent = true;
            xs.IndentChars = "    ";
            xs.NewLineChars = System.Environment.NewLine;
            xs.OmitXmlDeclaration = false; // // <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
            // xs.Encoding = System.Text.Encoding.UTF8; // doesn't work with pgsql 
            // xs.Encoding = new System.Text.UTF8Encoding(false);
            xs.Encoding = new System.Text.UnicodeEncoding(false, false);


            StringWriterWithEncoding sw = new StringWriterWithEncoding(builder, xs.Encoding);

            // string exportFilename = System.IO.Path.Combine(@"d:\", table_name + ".xml");
            // using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(exportFilename, xs))
            return System.Xml.XmlWriter.Create(sw, xs);
        } // End Function CreateXmlWriter 


        
        private static string QuoteObject(string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                throw new System.ArgumentNullException("objectName");

            return "\"" + objectName.Replace("\"", "\"\"") + "\"";
        }
        
        /*
https://data.services.jetbrains.com/products/download?code=RD&platform=linux&_ga=2.172199712.2087767762.1605895035-1291856558.1605895035

var properties = type.GetProperties().Where(IsWriteable).ToArray();



            var properties = type.GetProperties().Where(IsWriteable).ToArray();
            
                        if (TypeProperties.TryGetValue(type.TypeHandle, out IEnumerable<PropertyInfo> pis))
            {
                return pis.ToList();
            }

            var properties = type.GetProperties().Where(IsWriteable).ToArray();
            TypeProperties[type.TypeHandle] = properties;
            return properties.ToList();
            
            
            private static readonly ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> TypeProperties = new ConcurrentDictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();



         */
        public static async System.Threading.Tasks.Task AnyDataReaderToXml(this System.Data.IDbConnection cnn 
            , string sql 
            , object param = null
            , string tableSchema = null
            , string tableName = null
            , XmlRenderType_t format = XmlRenderType_t.Default
            , Microsoft.AspNetCore.Http.HttpContext context = null
            , System.Data.IDbTransaction transaction = null
            , int? commandTimeout = null
            , System.Data.CommandType? commandType = null)
        {

            if (string.IsNullOrEmpty(sql))
            {
                if (string.IsNullOrEmpty(tableName))
                    throw new System.ArgumentException("Parameter " +nameof(tableName) + " is NULL or empty.");
                
                if (string.IsNullOrEmpty(tableSchema))
                    sql = $"SELECT * FROM " + QuoteObject(tableName) + " ;";
                else
                    sql = $"SELECT * FROM " + QuoteObject(tableSchema) + "." + QuoteObject(tableName) + " ;";
            }
                

        
            
            if (string.IsNullOrEmpty(sql))
                throw new System.ArgumentException("Parameter " +nameof(sql) + " is NULL or empty.");
            
            // DynamicParameters dbArgs = new DynamicParameters();

            System.Text.StringBuilder xmlBuilder = new System.Text.StringBuilder();
            
            using (System.IO.StreamWriter output =
                new System.IO.StreamWriter(context.Response.Body, System.Text.Encoding.UTF8))
            {
                
                using (System.Xml.XmlWriter writer = CreateXmlWriter(xmlBuilder))
                {
                    
                    using (System.Data.Common.DbDataReader dr = await cnn.ExecuteDbReaderAsync(sql, param, transaction, commandTimeout, commandType))
                    {
                        if (context != null)
                        {
                            context.Response.StatusCode = (int) System.Net.HttpStatusCode.OK;
                            // context.Response.ContentType = "application/xml";
                            context.Response.ContentType = "application/xml; charset=utf-8";
                        }
                        
                        // LargeDataToElementXML(table_schema, table_name, writer, dr);  
                        LargeDataToArrributeXML(tableSchema, tableName, writer, dr); 
                    }
                    
                } // End Using writer 
            }
        }
        
    
        
        private static void LargeDataToArrributeXML(
              string table_schema
            , string table_name
            , System.Xml.XmlWriter writer
            , System.Data.IDataReader dr)
        {
            writer.WriteStartDocument(true);
            writer.WriteStartElement("table");
            // writer.WriteStartElement(table_name);
            if(table_schema != null)
                writer.WriteAttributeString(null, "table_schema", null, table_schema);
            
            if(table_name != null)
                writer.WriteAttributeString(null, "table_name", null, table_name);
            
            writer.WriteAttributeString("xmlns", "xsi", null, System.Xml.Schema.XmlSchema.InstanceNamespace);
            // writer.WriteAttributeString("xsi", "schemaLocation", null, System.Xml.Schema.XmlSchema.InstanceNamespace);

            int fc = dr.FieldCount;

            string[] columnNames = new string[fc];
            System.Type[] columnTypes = new System.Type[fc];

            for (int i = 0; i < dr.FieldCount; ++i)
            {
                columnNames[i] = dr.GetName(i);
                columnTypes[i] = dr.GetFieldType(i);
            } // Next i 

            while (dr.Read())
            {
                writer.WriteStartElement("row");

                for (int i = 0; i < fc; ++i)
                {
                    object obj = dr.GetValue(i);

                    if (obj != System.DBNull.Value)
                    {
                        string value = null;

                        if (object.ReferenceEquals(columnTypes[i], typeof(System.DateTime)))
                        {
                            System.DateTime dt = (System.DateTime) obj;
                            value = dt.ToString("yyyy-MM-dd'T'HH':'mm':'ss'.'fff",
                                System.Globalization.CultureInfo.InvariantCulture));
                        }
                        else
                            value = System.Convert.ToString(obj, System.Globalization.CultureInfo.InvariantCulture);
                        
                        writer.WriteAttributeString(null, columnNames[i], null, value);
                    }
                    
                } // Next i

                writer.WriteEndElement();
            } // Whend
            
            writer.WriteEndElement();
        } // End Sub LargeDataToArrributeXML 

        
        private static void LargeDataToElementXML(
              string table_schema
            , string table_name
            , System.Xml.XmlWriter writer
            , System.Data.IDataReader dr)
        {
            writer.WriteStartDocument(true);
            writer.WriteStartElement("table");
            // writer.WriteStartElement(table_name);
            if(table_schema != null)
                writer.WriteAttributeString(null, "table_schema", null, table_schema);
            
            if(table_name != null)
                writer.WriteAttributeString(null, "table_name", null, table_name);
            
            writer.WriteAttributeString("xmlns", "xsi", null, System.Xml.Schema.XmlSchema.InstanceNamespace);
            // writer.WriteAttributeString("xsi", "schemaLocation", null, System.Xml.Schema.XmlSchema.InstanceNamespace);

            int fc = dr.FieldCount;

            string[] columnNames = new string[fc];
            System.Type[] columnTypes = new System.Type[fc];

            for (int i = 0; i < dr.FieldCount; ++i)
            {
                columnNames[i] = dr.GetName(i);
                columnTypes[i] = dr.GetFieldType(i);
            } // Next i 

            while (dr.Read())
            {
                writer.WriteStartElement("row");

                for (int i = 0; i < fc; ++i)
                {
                    writer.WriteStartElement(columnNames[i]);
                    object obj = dr.GetValue(i);

                    if (obj != System.DBNull.Value)
                    {
                        if (object.ReferenceEquals(columnTypes[i], typeof(System.DateTime)))
                        {
                            System.DateTime dt = (System.DateTime)obj;
                            writer.WriteValue(dt.ToString("yyyy-MM-dd'T'HH':'mm':'ss'.'fff",
                                System.Globalization.CultureInfo.InvariantCulture));
                        }
                        else
                            writer.WriteValue(System.Convert.ToString(obj, System.Globalization.CultureInfo.InvariantCulture));
                    }
                    else
                        writer.WriteAttributeString("xsi", "nil", System.Xml.Schema.XmlSchema.InstanceNamespace, "true");

                    writer.WriteEndElement();
                } // Next i

                writer.WriteEndElement();
            } // Whend 

            writer.WriteEndElement();
        } // End Sub LargeDataToElementXML 
        
        
    } // End Class SqlMapper 
    
    
} // End Namespace Dapper 

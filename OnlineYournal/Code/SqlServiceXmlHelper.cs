
using Dapper;


namespace OnlineYournal
{

    // wcf host in .net core
    // https://github.com/DigDes/SoapCore
    // https://devblogs.microsoft.com/dotnet/custom-asp-net-core-middleware-example/
    
    // WCF: No connection could be made because the target machine actively refused
    // https://stackoverflow.com/a/25370157/155077
    // bypassonlocal?
    // Well, I got this error message when I forgot to install necessary components. see link Configuring WCF Service with netTcpBinding
    // Go to "Programs and Features" (usually in control panel)
    // Go to "Turn Windows features on or off"
    // (assuming VS2012) Go to ".NET Framework 4.5 Advanced Services"->"WCF Services"
    // Enable "TCP Activation"
    // https://rohitguptablog.wordpress.com/2011/06/16/configuring-wcf-service-with-nettcpbinding/
    
    // https://docs.microsoft.com/en-us/aspnet/core/grpc/why-migrate-wcf-to-dotnet-grpc?view=aspnetcore-5.0
    // https://stackoverflow.com/questions/8514766/how-to-run-wcf-service-on-a-specific-port
    

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


        private static System.Xml.XmlWriter CreateXmlWriter(System.Text.StringBuilder builder, XmlRenderType_t renderType)
        {
            return CreateXmlWriter(builder, null, renderType);
        }


        private static System.Xml.XmlWriter CreateXmlWriter(System.IO.StreamWriter writer, XmlRenderType_t renderType)
        {
            return CreateXmlWriter(null, writer, renderType);
        }


        private static System.Xml.XmlWriter CreateXmlWriter(System.Text.StringBuilder builder, System.IO.StreamWriter writer, XmlRenderType_t renderType)
        {
            System.Xml.XmlWriterSettings xs = new System.Xml.XmlWriterSettings();
            
            if(renderType.HasFlag(XmlRenderType_t.Indented))
                xs.Indent = true;
            else
                xs.Indent = false;

            xs.Async = true;
            xs.IndentChars = "    ";
            xs.NewLineChars = System.Environment.NewLine;
            xs.OmitXmlDeclaration = false; // // <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
            // xs.Encoding = System.Text.Encoding.UTF8; // doesn't work with pgsql 
            // xs.Encoding = new System.Text.UTF8Encoding(false);
            xs.Encoding = new System.Text.UnicodeEncoding(false, false);

            // string exportFilename = System.IO.Path.Combine(@"d:\", table_name + ".xml");
            // using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(exportFilename, xs))

            if (writer != null)
                return System.Xml.XmlWriter.Create(writer, xs);

            StringWriterWithEncoding sw = new StringWriterWithEncoding(builder, xs.Encoding);

            return System.Xml.XmlWriter.Create(sw, xs);
        } // End Function CreateXmlWriter 


        
        private static string QuoteObject(string objectName)
        {
            if (string.IsNullOrEmpty(objectName))
                throw new System.ArgumentNullException("objectName");

            return "\"" + objectName.Replace("\"", "\"\"") + "\"";
        }

        // https://data.services.jetbrains.com/products/download?code=RD&platform=linux&_ga=2.172199712.2087767762.1605895035-1291856558.1605895035

        private static readonly System.Collections.Concurrent.ConcurrentDictionary
            <System.RuntimeTypeHandle, System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo>> 
            TypeProperties = new System.Collections.Concurrent.ConcurrentDictionary
            <System.RuntimeTypeHandle, System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo>>();

        public static System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo> foo(System.Type type)
        {
            if (TypeProperties.TryGetValue(type.TypeHandle, out System.Collections.Generic.IEnumerable<
                System.Reflection.PropertyInfo> pis))
            {
                return System.Linq.Enumerable.ToList(pis);
            }
            
            System.Reflection.PropertyInfo[] properties = System.Linq.Enumerable.ToArray(
                System.Linq.Enumerable.Where(type.GetProperties(), pi => pi.CanWrite)
            ); 
            
            TypeProperties[type.TypeHandle] = properties;
            return System.Linq.Enumerable.ToList(properties);
        }


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

                // using (System.Xml.XmlWriter writer = CreateXmlWriter(xmlBuilder, format))
                using (System.Xml.XmlWriter writer = CreateXmlWriter(output, format))
                {

                    using (System.Data.Common.DbDataReader dr = await cnn.ExecuteDbReaderAsync(sql, param, transaction, commandTimeout, commandType))
                    {
                        if (context != null)
                        {
                            context.Response.StatusCode = (int) System.Net.HttpStatusCode.OK;
                            // context.Response.ContentType = "application/xml";
                            context.Response.ContentType = "application/xml; charset=utf-8";
                        }
                        
                        await LargeDataToElementXML(tableSchema, tableName, writer, dr);
                        // await LargeDataToArrributeXML(tableSchema, tableName, writer, dr); 
                    }

                    await writer.FlushAsync();
                } // End Using writer 
            }
        }
        
    
        
        private static async System.Threading.Tasks.Task LargeDataToArrributeXML(
              string table_schema
            , string table_name
            , System.Xml.XmlWriter writer
            , System.Data.IDataReader dr)
        {
            await writer.WriteStartDocumentAsync(true);
            await writer.WriteStartElementAsync(null, "table", null);
            // await writer.WriteStartElementAsync(null, table_name, null);

            if (table_schema != null)
                await writer.WriteAttributeStringAsync(null, "table_schema", null, table_schema);
            
            if(table_name != null)
                await writer.WriteAttributeStringAsync(null, "table_name", null, table_name);
            
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
                await writer.WriteStartElementAsync(null, "row", null);

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
                                System.Globalization.CultureInfo.InvariantCulture);
                        }
                        else
                            value = System.Convert.ToString(obj, System.Globalization.CultureInfo.InvariantCulture);
                        
                        await writer.WriteAttributeStringAsync(null, columnNames[i], null, value);
                    }
                    
                } // Next i

                await writer.WriteEndElementAsync();
            } // Whend
            
            await writer.WriteEndElementAsync();
            await writer.FlushAsync();
        } // End Sub LargeDataToArrributeXML 

        
        private static async System.Threading.Tasks.Task LargeDataToElementXML(
              string table_schema
            , string table_name
            , System.Xml.XmlWriter writer
            , System.Data.IDataReader dr)
        {
            await writer.WriteStartDocumentAsync(true);
            await writer.WriteStartElementAsync(null, "table", null);
            // await writer.WriteStartElementAsync(null, table_name, null);

            if (table_schema != null)
                await writer.WriteAttributeStringAsync(null, "table_schema", null, table_schema);
            
            if(table_name != null)
                await writer.WriteAttributeStringAsync(null, "table_name", null, table_name);
            
            await writer.WriteAttributeStringAsync("xmlns", "xsi", null, System.Xml.Schema.XmlSchema.InstanceNamespace);
            
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
                await writer.WriteStartElementAsync(null, "row", null);

                for (int i = 0; i < fc; ++i)
                {
                    await writer.WriteStartElementAsync(null, columnNames[i], null);
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
                        await writer.WriteAttributeStringAsync("xsi", "nil", System.Xml.Schema.XmlSchema.InstanceNamespace, "true");

                    await writer.WriteEndElementAsync();
                } // Next i

                await writer.WriteEndElementAsync();
            } // Whend 

            await writer.WriteEndElementAsync();
        } // End Sub LargeDataToElementXML 
        
        
    } // End Class SqlMapper 
    
    
} // End Namespace Dapper 

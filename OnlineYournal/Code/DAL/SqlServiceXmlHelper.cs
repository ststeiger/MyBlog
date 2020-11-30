
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
        DataInAttributes = 4,
        WithColumnDefinition = 8,
        WithDetail = 16,
        LongName = 32,
        AssemblyQualifiedName = 64
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
            return CreateXmlWriter(builder, null, System.Text.Encoding.Unicode, renderType);
        }


        private static System.Xml.XmlWriter CreateXmlWriter(System.Text.StringBuilder builder, System.Text.Encoding encoding, XmlRenderType_t renderType)
        {
            return CreateXmlWriter(builder, null, encoding, renderType);
        }


        private static System.Xml.XmlWriter CreateXmlWriter(System.IO.StreamWriter writer, XmlRenderType_t renderType)
        {
            return CreateXmlWriter(null, writer, writer.Encoding, renderType);
        }

        private static System.Xml.XmlWriter CreateXmlWriter(System.IO.Stream stream, System.Text.Encoding encoding, XmlRenderType_t renderType)
        {
            System.IO.TextWriter tw = new System.IO.StreamWriter(stream, encoding);

            return CreateXmlWriter(null, tw, encoding, renderType);
        }

        private static System.Xml.XmlWriter CreateXmlWriter(Microsoft.AspNetCore.Http.HttpContext context, System.Text.Encoding encoding, XmlRenderType_t renderType)
        {
            return CreateXmlWriter(context.Response.Body, encoding, renderType);
        }
        
        private static System.Xml.XmlWriter CreateXmlWriter(System.Text.StringBuilder builder, System.IO.TextWriter writer, System.Text.Encoding encoding, XmlRenderType_t renderType)
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
            xs.Encoding = encoding;
            xs.CloseOutput = true;

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
            , System.Text.Encoding encoding = null
            , XmlRenderType_t format = XmlRenderType_t.Default 
            , Microsoft.AspNetCore.Http.HttpContext context = null 
            , System.Data.IDbTransaction transaction = null 
            , int? commandTimeout = null 
            , System.Data.CommandType? commandType = null)
        {
            if (encoding == null)
                encoding = System.Text.Encoding.UTF8;

            if (string.IsNullOrEmpty(sql))
            {
                if (string.IsNullOrEmpty(tableName))
                    throw new System.ArgumentException("Parameter " +nameof(tableName) + " is NULL or empty.");
                
                if (string.IsNullOrEmpty(tableSchema))
                    sql = $"SELECT * FROM " + QuoteObject(tableName) + " ;";
                else
                    sql = $"SELECT * FROM " + QuoteObject(tableSchema) + "." + QuoteObject(tableName) + " ;";
            } // End if (string.IsNullOrEmpty(sql))

            if (string.IsNullOrEmpty(sql))
                throw new System.ArgumentException("Parameter " +nameof(sql) + " is NULL or empty.");
            
            // DynamicParameters dbArgs = new DynamicParameters();

            System.Text.StringBuilder xmlBuilder = new System.Text.StringBuilder();
            
            using (System.IO.StreamWriter output =
                new System.IO.StreamWriter(context.Response.Body, encoding))
            {
                
                // using (System.Xml.XmlWriter writer = CreateXmlWriter(xmlBuilder, format))
                using (System.Xml.XmlWriter writer = CreateXmlWriter(output, format))
                {
                    try
                    {

                        using (System.Data.Common.DbDataReader dr = await cnn.ExecuteDbReaderAsync(sql, param, transaction, commandTimeout, commandType))
                        {
                            if (context != null)
                            {
                                context.Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                                context.Response.ContentType = "application/xml; charset=" + encoding.WebName;
                            } // End if (context != null) 

                            await WriteAsXmlAsync(tableSchema, tableName, format, writer, dr);
                        } // End Using dr 

                    }
                    catch (System.Exception ex)
                    {
                        context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/xml; charset=" + encoding.WebName;

                        bool dataAsAttributes = format.HasFlag(XmlRenderType_t.DataInAttributes);


                        await writer.WriteStartDocumentAsync(true);

                        await writer.WriteStartElementAsync(null, "error", null);
                        if (dataAsAttributes)
                            await writer.WriteAttributeStringAsync(null, "Message", null, ex.Message);
                        else
                        {
                            await writer.WriteStartElementAsync(null, "Message", null);
                            writer.WriteValue(ex.Message);
                            await writer.WriteEndElementAsync(); // Message
                        }

                        if (dataAsAttributes)
                            await writer.WriteAttributeStringAsync(null, "StackTrace", null, ex.StackTrace);
                        else
                        {
                            await writer.WriteStartElementAsync(null, "StackTrace", null);
                            writer.WriteValue(ex.StackTrace);
                            await writer.WriteEndElementAsync(); // StackTrace
                        }
                        
                        await writer.WriteEndElementAsync(); // error
                    }

                    await writer.FlushAsync();
                } // End Using writer 

            }// Wnd Using output 

        } // End Task AnyDataReaderToXml 


        private static string GetAssemblyQualifiedNoVersionName(string input)
        {
            int i = 0;
            bool isNotFirst = false;
            while (i < input.Length)
            {
                if (input[i] == ',')
                {
                    if (isNotFirst)
                        break;

                    isNotFirst = true;
                }

                i += 1;
            }

            return input.Substring(0, i);
        } // GetAssemblyQualifiedNoVersionName


        private static string GetAssemblyQualifiedNoVersionName(System.Type type)
        {
            if (type == null)
                return null;

            return GetAssemblyQualifiedNoVersionName(type.AssemblyQualifiedName);
        } // GetAssemblyQualifiedNoVersionName


        private static string GetTypeName(System.Type type, XmlRenderType_t renderType)
        {
            if (type == null)
                return null;

            if (renderType.HasFlag(XmlRenderType_t.AssemblyQualifiedName))
                return GetAssemblyQualifiedNoVersionName(type);

            if (renderType.HasFlag(XmlRenderType_t.LongName))
                return type.FullName;

            return type.Name;
        } // GetAssemblyQualifiedNoVersionName

        
        private static async System.Threading.Tasks.Task WriteColumnDefinition(
               System.Xml.XmlWriter writer
             , System.Data.IDataReader dr 
            ,  XmlRenderType_t renderType)
        {
            await writer.WriteStartElementAsync(null, "columns", null);

            for (int i = 0; i <= dr.FieldCount - 1; i++)
            {
                await writer.WriteStartElementAsync(null, "column", null);

                await writer.WriteAttributeStringAsync(null, "name", null, dr.GetName(i));
                await writer.WriteAttributeStringAsync(null, "index", null, i.ToString(System.Globalization.CultureInfo.InvariantCulture));

                if (renderType.HasFlag(XmlRenderType_t.WithDetail))
                {
                    await writer.WriteAttributeStringAsync(null, "fieldType", null, GetTypeName(dr.GetFieldType(i), renderType));
                }

                await writer.WriteEndElementAsync(); // column
            }

            await writer.WriteEndElementAsync(); // columns 
        } // WriteColumnDefinition



        private static async System.Threading.Tasks.Task WriteAsXmlAsync(
              string table_schema
            , string table_name
            , XmlRenderType_t format
            , System.Xml.XmlWriter writer
            , System.Data.IDataReader dr)
        {
            await writer.WriteStartDocumentAsync(true);
            bool dataAsAttributes = format.HasFlag(XmlRenderType_t.DataInAttributes);


            bool dataTableOnly = format.HasFlag(XmlRenderType_t.DataTable);

            if (!dataTableOnly)
                await writer.WriteStartElementAsync(null, "dataset", null);

            do
            {
                await writer.WriteStartElementAsync(null, "table", null);

                if (dataTableOnly)
                {
                    if (table_schema != null)
                        await writer.WriteAttributeStringAsync(null, "table_schema", null, table_schema);

                    if (table_name != null)
                        await writer.WriteAttributeStringAsync(null, "table_name", null, table_name);
                }

                if (!dataAsAttributes)
                    await writer.WriteAttributeStringAsync("xmlns", "xsi", null, System.Xml.Schema.XmlSchema.InstanceNamespace);

                int fc = dr.FieldCount;

                if (format.HasFlag(XmlRenderType_t.WithColumnDefinition))
                    await WriteColumnDefinition(writer, dr, format);

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
                        if (!dataAsAttributes)
                            await writer.WriteStartElementAsync(null, columnNames[i], null);

                        object obj = dr.GetValue(i);

                        if (obj != System.DBNull.Value)
                        {
                            string value = null;

                            if (object.ReferenceEquals(columnTypes[i], typeof(System.DateTime)))
                            {
                                System.DateTime dt = (System.DateTime)obj;
                                value = dt.ToString("yyyy-MM-dd'T'HH':'mm':'ss'.'fff", System.Globalization.CultureInfo.InvariantCulture);
                            }
                            else
                                value = System.Convert.ToString(obj, System.Globalization.CultureInfo.InvariantCulture);

                            if (dataAsAttributes)
                                await writer.WriteAttributeStringAsync(null, columnNames[i], null, value);
                            else
                                writer.WriteValue(value);
                        } // End if (obj != System.DBNull.Value) 
                        else
                        {
                            if (!dataAsAttributes)
                                await writer.WriteAttributeStringAsync("xsi", "nil", System.Xml.Schema.XmlSchema.InstanceNamespace, "true");
                        }

                        if (!dataAsAttributes)
                            await writer.WriteEndElementAsync(); // column 
                    } // Next i

                    await writer.WriteEndElementAsync(); // row 
                } // Whend 

                await writer.WriteEndElementAsync(); // table 
                await writer.FlushAsync();

                if (dataTableOnly)
                    break;
            } while (dr.NextResult());

            if (!dataTableOnly)
            {
                await writer.WriteEndElementAsync(); // dataset 
                await writer.FlushAsync();
            }

        } // End Sub WriteAsXmlAsync 


    } // End Class SqlMapper 
    
    
} // End Namespace Dapper 

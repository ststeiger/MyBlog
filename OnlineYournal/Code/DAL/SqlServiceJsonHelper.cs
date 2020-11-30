
using Dapper;


namespace AnySqlWebAdmin
{


    [System.Flags]
    public enum RenderType_t : int
    {
        Default = 0,
        Indented = 1,
        DataTable = 2,
        Array = 4,
        Data_Only = 8,
        Columns_Associative = 16,
        Columns_ObjectArray = 32,
        WithDetail = 64,
        
        LongName = 128,
        AssemblyQualifiedName = 256
    }


    public class SqlServiceJsonHelper
    {
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


        private static string GetTypeName(System.Type type, RenderType_t renderType)
        {
            if (type == null)
                return null;

            if (renderType.HasFlag(RenderType_t.AssemblyQualifiedName))
                return GetAssemblyQualifiedNoVersionName(type);

            if (renderType.HasFlag(RenderType_t.LongName))
                return type.FullName;

            return type.Name;
        } // GetAssemblyQualifiedNoVersionName


        private static async System.Threading.Tasks.Task WriteAssociativeColumnsArray(
            Newtonsoft.Json.JsonTextWriter jsonWriter
            , System.Data.Common.DbDataReader dr, RenderType_t renderType)
        {
            //await jsonWriter.WriteStartObjectAsync();
            await jsonWriter.WriteStartObjectAsync();

            for (int i = 0; i <= dr.FieldCount - 1; i++)
            {
                await jsonWriter.WritePropertyNameAsync(dr.GetName(i));
                await jsonWriter.WriteStartObjectAsync();

                await jsonWriter.WritePropertyNameAsync("index");
                await jsonWriter.WriteValueAsync(i);

                if (renderType.HasFlag(RenderType_t.WithDetail))
                {
                    await jsonWriter.WritePropertyNameAsync("fieldType");
                    // await jsonWriter.WriteValueAsync(GetAssemblyQualifiedNoVersionName(dr.GetFieldType(i)));
                    await jsonWriter.WriteValueAsync(GetTypeName(dr.GetFieldType(i), renderType));
                }

                await jsonWriter.WriteEndObjectAsync();
            }

            await jsonWriter.WriteEndObjectAsync();
        } // WriteAssociativeArray


        private static async System.Threading.Tasks.Task WriteComplexArray(Newtonsoft.Json.JsonTextWriter jsonWriter,
            System.Data.Common.DbDataReader dr, RenderType_t renderType)
        {
            //await jsonWriter.WriteStartObjectAsync();
            await jsonWriter.WriteStartArrayAsync();

            for (int i = 0; i <= dr.FieldCount - 1; i++)
            {
                await jsonWriter.WriteStartObjectAsync();

                await jsonWriter.WritePropertyNameAsync("name");
                await jsonWriter.WriteValueAsync(dr.GetName(i));

                await jsonWriter.WritePropertyNameAsync("index");
                await jsonWriter.WriteValueAsync(i);

                if (renderType.HasFlag(RenderType_t.WithDetail))
                {
                    await jsonWriter.WritePropertyNameAsync("fieldType");
                    //await jsonWriter.WriteValueAsync(GetAssemblyQualifiedNoVersionName(dr.GetFieldType(i)));
                    await jsonWriter.WriteValueAsync(GetTypeName(dr.GetFieldType(i), renderType));
                }

                await jsonWriter.WriteEndObjectAsync();
            }

            // await jsonWriter.WriteEndObjectAsync();
            await jsonWriter.WriteEndArrayAsync();
        } // WriteAssociativeArray


        private static async System.Threading.Tasks.Task WriteArray(Newtonsoft.Json.JsonTextWriter jsonWriter,
            System.Data.Common.DbDataReader dr)
        {
            await jsonWriter.WriteStartArrayAsync();
            for (int i = 0; i <= dr.FieldCount - 1; i++)
                await jsonWriter.WriteValueAsync(dr.GetName(i));

            await jsonWriter.WriteEndArrayAsync();
        } // End Sub WriteArray 


        public static async System.Threading.Tasks.Task AnyDataReaderToJson(
              System.Data.Common.DbConnection cnn 
            , string sql
            , RenderType_t format
            , Microsoft.AspNetCore.Http.HttpContext context
            , System.Text.Encoding encoding 
            , object parameters = null
            , System.Data.IDbTransaction transaction = null
            , int? commandTimeout = null
            , System.Data.CommandType? commandType = null
            )
        {
            // DynamicParameters dbArgs = new DynamicParameters();


            using (System.IO.StreamWriter responseWriter = new System.IO.StreamWriter(context.Response.Body, encoding))
            {
                using (Newtonsoft.Json.JsonTextWriter jsonWriter =
                    new Newtonsoft.Json.JsonTextWriter(responseWriter))
                {
                    if (format.HasFlag(RenderType_t.Indented))
                        jsonWriter.Formatting = Newtonsoft.Json.Formatting.Indented;


                    try
                    {

                        using (System.Data.Common.DbDataReader dr = await cnn.ExecuteDbReaderAsync(sql, parameters, transaction, commandTimeout, commandType))
                        {
                            context.Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                            context.Response.ContentType = "application/json; charset=" + encoding.WebName;

                            await jsonWriter.WriteStartObjectAsync();

                            await jsonWriter.WritePropertyNameAsync("tables");
                            await jsonWriter.WriteStartArrayAsync();

                            do
                            {
                                if (!format.HasFlag(RenderType_t.Data_Only) &&
                                    !format.HasFlag(RenderType_t.DataTable))
                                {
                                    await jsonWriter.WriteStartObjectAsync();
                                    await jsonWriter.WritePropertyNameAsync("columns");

                                    if (format.HasFlag(RenderType_t.Columns_Associative))
                                    {
                                        await WriteAssociativeColumnsArray(jsonWriter, dr, format);
                                    }
                                    else if (format.HasFlag(RenderType_t.Columns_ObjectArray))
                                    {
                                        await WriteComplexArray(jsonWriter, dr, format);
                                    }
                                    else // (format.HasFlag(RenderType_t.Array))
                                    {
                                        await WriteArray(jsonWriter, dr);
                                    }
                                } // End if (!format.HasFlag(RenderType_t.Data_Only)) 


                                if (!format.HasFlag(RenderType_t.Data_Only) &&
                                    !format.HasFlag(RenderType_t.DataTable))
                                {
                                    await jsonWriter.WritePropertyNameAsync("rows");
                                } // End if (!format.HasFlag(RenderType_t.Data_Only))

                                await jsonWriter.WriteStartArrayAsync();

                                string[] columns = null;
                                if (format.HasFlag(RenderType_t.DataTable))
                                {
                                    columns = new string[dr.FieldCount];
                                    for (int i = 0; i < dr.FieldCount; i++)
                                    {
                                        columns[i] = dr.GetName(i);
                                    } // Next i 
                                } // End if (format.HasFlag(RenderType_t.DataTable)) 

                                while (await dr.ReadAsync())
                                {
                                    if (format.HasFlag(RenderType_t.DataTable))
                                        await jsonWriter.WriteStartObjectAsync();
                                    else
                                        await jsonWriter.WriteStartArrayAsync();

                                    for (int i = 0; i <= dr.FieldCount - 1; i++)
                                    {
                                        object obj = await dr.GetFieldValueAsync<object>(i);
                                        if (obj == System.DBNull.Value)
                                            obj = null;

                                        if (columns != null && format.HasFlag(RenderType_t.DataTable))
                                        {
                                            await jsonWriter.WritePropertyNameAsync(columns[i]);
                                        }

                                        await jsonWriter.WriteValueAsync(obj);
                                    } // Next i 

                                    if (format.HasFlag(RenderType_t.DataTable))
                                        await jsonWriter.WriteEndObjectAsync();
                                    else
                                        await jsonWriter.WriteEndArrayAsync();
                                } // Whend 

                                await jsonWriter.WriteEndArrayAsync();

                                if (!format.HasFlag(RenderType_t.Data_Only) &&
                                    !format.HasFlag(RenderType_t.DataTable))
                                {
                                    await jsonWriter.WriteEndObjectAsync();
                                } // End if (!format.HasFlag(RenderType_t.Data_Only)) 

                                await jsonWriter.FlushAsync();
                                await responseWriter.FlushAsync();
                            } while (await dr.NextResultAsync());

                            await jsonWriter.WriteEndArrayAsync();
                            await jsonWriter.WriteEndObjectAsync();

                            await jsonWriter.FlushAsync();
                            await responseWriter.FlushAsync();


                        } // End Using dr 

                    }
                    catch (System.Exception ex)
                    {
                        context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json; charset=" + encoding.WebName;

                        await jsonWriter.WriteStartObjectAsync();

                        await jsonWriter.WritePropertyNameAsync("Error");

                        await jsonWriter.WriteStartObjectAsync();
                        await jsonWriter.WritePropertyNameAsync("Message");
                        await jsonWriter.WriteValueAsync(ex.Message);

                        await jsonWriter.WritePropertyNameAsync("StackTrace");
                        await jsonWriter.WriteValueAsync(ex.StackTrace);
                        await jsonWriter.WriteEndObjectAsync();

                        await jsonWriter.WriteEndObjectAsync();
                    }

                    await jsonWriter.FlushAsync();
                } // End Using jsonWriter 

                await responseWriter.FlushAsync();
            } // End Using responseWriter 

        } // End Sub AnyDataReaderToJson 
        
        
    } // End Class 
    
    
} // End Namespace 
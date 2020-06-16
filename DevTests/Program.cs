using Dapper;


namespace DevTests
{


    // https://github.com/atachimiko/AtachiWiki
    // https://github.com/windows-toolkit/ColorCode-Universal
    // https://github.com/stephanjohnson/commonplex
    class Program
    {


        public static string WordScrambler(System.Text.RegularExpressions.Match match)
        {
            // string a = match.Captures[0].Value;

            string a = match.Groups[1].Value
            + match.Groups[2].Value.ToLowerInvariant()
            + match.Groups[3].Value;

            return a;
        }


        public static void IsEmptyColorBlack()
        {
            string empty = System.Drawing.ColorTranslator.ToHtml(System.Drawing.Color.Empty);

            System.Console.WriteLine(System.Drawing.Color.Empty.A); // 0 
            System.Console.WriteLine(System.Drawing.Color.Empty.R); // 0 
            System.Console.WriteLine(System.Drawing.Color.Empty.G); // 0 
            System.Console.WriteLine(System.Drawing.Color.Empty.B); // 0 

            System.Console.WriteLine(empty);
        }


        public static void ReplaceStylesheet()
        {
            string fileName = @"C:\Users\username\Documents\Visual Studio 2019\Projects\MyBlog\WikiPlex\Legacy\Colorizer\DefaultStyleSheet.cs";

            string content = System.IO.File.ReadAllText(fileName, System.Text.Encoding.UTF8);

            string newContent = System.Text.RegularExpressions.Regex.Replace(content, "(Background = \")(.*)(\",)", new System.Text.RegularExpressions.MatchEvaluator(WordScrambler));
            System.Console.WriteLine(newContent);
        }



        public static void ReplaceSQL()
        {
            string fileName = @"D:\username\Desktop\Reports\DIN\Alt\01_tfu_RPT_FM_DATA_DIN277_Grob_Raum.sql";
            fileName = @"D:\username\Desktop\Reports\DIN\Alt\02_tfu_RPT_FM_DATA_DIN277_Grob_Parkplaetze.sql";

            string content = System.IO.File.ReadAllText(fileName, System.Text.Encoding.UTF8);

            string pattern = @"THEN\s+ISNULL\s*\(\s*T_VWS_SVGElement.SVE_areaInSquaremeters\s*--\s*decimal\(12,\s*2\)\s*,ROUND\(\s*T_ZO_AP_Raum_Flaeche.ZO_RMFlaeche_Flaeche\s*,\s*2\)\s*--\s*float\s*\)";
            string rep = @"THEN ROUND(T_ZO_AP_Raum_Flaeche.ZO_RMFlaeche_Flaeche, 2) -- float ";

            pattern = @"THEN\s+ISNULL\s*\(\s*T_VWS_SVGElement.SVE_areaInSquaremeters\s*--\s*decimal\(12,\s*2\)\s*,ROUND\(\s*T_ZO_AP_Parkplatz_Flaeche.ZO_PPFlaeche_Flaeche\s*,\s*2\)\s*--\s*float\s*\)";
            rep = @"THEN ROUND(T_ZO_AP_Parkplatz_Flaeche.ZO_PPFlaeche_Flaeche, 2) -- float ";

            string newContent = System.Text.RegularExpressions.Regex.Replace(content, pattern, rep, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            string outputFileName = @"D:\username\Desktop\Reports\DIN\Alt\01a_tfu_RPT_FM_DATA_DIN277_Grob_Raum.sql";
            outputFileName = @"D:\username\Desktop\Reports\DIN\Alt\02a_tfu_RPT_FM_DATA_DIN277_Grob_Parkplaetze.sql";
            System.IO.File.WriteAllText(outputFileName, newContent, System.Text.Encoding.UTF8);
            System.Console.WriteLine(newContent);
        }



        public static string GetCS()
        {
            Npgsql.NpgsqlConnectionStringBuilder csb = new Npgsql.NpgsqlConnectionStringBuilder();
            csb.Database = "blogz";
            csb.Host = "localhost";
            csb.Port = 5432;
            csb.IntegratedSecurity = true;
            csb.Username = "postgres";
            // csb.Password = "";

            return csb.ConnectionString;
        }

        public static void DataToXML()
        {
            string table_schema = "geoip";
            string table_name = "geoip_blocks_temp";

            table_schema = "public";
            table_name = "t_sys_language_monthnames";


            using (System.Data.DataTable dt = new System.Data.DataTable())
            {
                dt.TableName = "record";

                using (System.Data.DataSet ds = new System.Data.DataSet(table_name))
                {
                    ds.Tables.Add(dt);
                    // dt.Namespace = "foo";

                    using (System.Data.Common.DbConnection con = Npgsql.NpgsqlFactory.Instance.CreateConnection())
                    {
                        con.ConnectionString = GetCS();

                        using (System.Data.Common.DbCommand cmd = con.CreateCommand())
                        {
                            cmd.CommandText = "SELECT * FROM " + table_schema + "." + table_name;

                            using (System.Data.Common.DbDataAdapter da = Npgsql.NpgsqlFactory.Instance.CreateDataAdapter())
                            {
                                da.SelectCommand = cmd;

                                if (con.State != System.Data.ConnectionState.Open)
                                    con.Open();

                                da.Fill(dt);

                                if (con.State != System.Data.ConnectionState.Open)
                                    con.Close();
                            } // End Using da 

                        } // End Using cmd 

                    } // End Using con 

                    string exportFilename = System.IO.Path.Combine(@"d:\", table_name + ".xml");


                    //using (System.IO.Stream fs = System.IO.File.OpenWrite(exportFilename))
                    //{
                    //    using (System.IO.TextWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8))
                    //    {
                    //        // System.IO.StringWriter sw = new System.IO.StringWriter();
                    //        // dt.WriteXml(sw, System.Data.XmlWriteMode.IgnoreSchema);
                    //        dt.WriteXml(sw, System.Data.XmlWriteMode.IgnoreSchema);
                    //    } // End Using sw 

                    //} // End Using fs 


                    System.Xml.XmlWriterSettings xs = new System.Xml.XmlWriterSettings();
                    xs.Indent = true;
                    xs.IndentChars = "    ";
                    xs.NewLineChars = System.Environment.NewLine;
                    xs.OmitXmlDeclaration = false;
                    // xs.Encoding = System.Text.Encoding.UTF8; // doesn't work with pgsql 
                    xs.Encoding = new System.Text.UTF8Encoding(false);

                    // <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
                    using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(exportFilename, xs))
                    {
                        dt.WriteXml(writer, System.Data.XmlWriteMode.IgnoreSchema);
                    } // End Using writer 

                    System.Console.WriteLine(dt.Rows.Count);
                } // End Using ds 

            } // End Using dt 

        } // End Sub DataToXML 



        public static void LargeDataToXML()
        {
            string table_schema = "geoip";
            string table_name = "geoip_blocks_temp";

            // table_schema = "public";
            // table_name = "t_sys_language_monthnames";


            System.Xml.XmlWriterSettings xs = new System.Xml.XmlWriterSettings();
            xs.Indent = true;
            xs.IndentChars = "    ";
            xs.NewLineChars = System.Environment.NewLine;
            xs.OmitXmlDeclaration = false; // // <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
            // xs.Encoding = System.Text.Encoding.UTF8; // doesn't work with pgsql 
            xs.Encoding = new System.Text.UTF8Encoding(false);

            string exportFilename = System.IO.Path.Combine(@"d:\", table_name + ".xml");

            using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(exportFilename, xs))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement(table_name);

                writer.WriteAttributeString("xmlns", "xsi", null, System.Xml.Schema.XmlSchema.InstanceNamespace);
                // writer.WriteAttributeString("xsi", "schemaLocation", null, System.Xml.Schema.XmlSchema.InstanceNamespace);



                using (System.Data.Common.DbConnection con = Npgsql.NpgsqlFactory.Instance.CreateConnection())
                {
                    con.ConnectionString = GetCS();

                    using (System.Data.Common.DbCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = "SELECT * FROM " + table_schema + "." + table_name;


                        if (con.State != System.Data.ConnectionState.Open)
                            con.Open();

                        using (System.Data.Common.DbDataReader dr = cmd.ExecuteReader(System.Data.CommandBehavior.SequentialAccess))
                        {

                            if (dr.HasRows)
                            {
                                int fc = dr.FieldCount;

                                string[] columnNames = new string[fc];
                                // System.Type[] columnTypes = new System.Type[fc];

                                for (int i = 0; i < dr.FieldCount; ++i)
                                {
                                    columnNames[i] = dr.GetName(i);
                                    // columnTypes[i] = dr.GetFieldType(i);
                                } // Next i 

                                while (dr.Read())
                                {
                                    // object[] thisRow = new object[dr.FieldCount];

                                    writer.WriteStartElement("record");

                                    for (int i = 0; i < fc; ++i)
                                    {
                                        writer.WriteStartElement(columnNames[i]);
                                        object obj = dr.GetValue(i);

                                        if (obj != System.DBNull.Value)
                                        {
                                            writer.WriteValue(obj);
                                        }
                                        else
                                            writer.WriteAttributeString("xsi", "nil", System.Xml.Schema.XmlSchema.InstanceNamespace, "true");

                                        writer.WriteEndElement();
                                    } // Next i

                                    writer.WriteEndElement();

                                } // Whend 

                            } // End if (dr.HasRows) 

                        } // End Using dr 

                        if (con.State != System.Data.ConnectionState.Open)
                            con.Close();
                    } // End Using cmd 

                } // End Using con 

                writer.WriteEndElement();
            } // ENd Using writer 

        } // End Sub LargeDataToXML 



        public static void TestFactory()
        {
            System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();
            csb.DataSource = "localhost";
            csb.InitialCatalog = "Blogz";
            csb.IntegratedSecurity = true;

            csb.PersistSecurityInfo = false;
            csb.PacketSize = 4096;
            csb.WorkstationID = System.Environment.MachineName;


            // DB.SqlFactory factory = new DB.SqlFactory(System.Data.SqlClient.SqlClientFactory.Instance, csb.ConnectionString);
            DB.SqlFactory factory = new DB.SqlFactory(Npgsql.NpgsqlFactory.Instance, GetCS());


            int count = factory.Connection.ExecuteScalar<int>("SELECT COUNT(*) FROM information_schema.tables");
            System.Console.WriteLine(count);
        }


        // https://www.gaijin.at/en/infos/ascii-ansi-character-table#:~:text=Overview,the%20unchanged%20ASCII%20character%20set.

        public static System.Text.Encoding GetSystemEncoding()
        {
            // The OEM code page for use by legacy console applications
            // int oem = System.Globalization.CultureInfo.CurrentCulture.TextInfo.OEMCodePage;

            // The ANSI code page for use by legacy GUI applications
            // int ansi = System.Globalization.CultureInfo.InstalledUICulture.TextInfo.ANSICodePage; // Machine 
            int ansi = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ANSICodePage; // User 

            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                System.Text.Encoding enc = System.Text.Encoding.GetEncoding(ansi);
                return enc;
            }
            catch (System.Exception)
            { }


            try
            {

                foreach (System.Text.EncodingInfo ei in System.Text.Encoding.GetEncodings())
                {
                    System.Text.Encoding e = ei.GetEncoding();

                    // 20'127 US-ASCII 
                    if (e.WindowsCodePage == ansi && e.CodePage != 20127)
                    {
                        return e;
                    }

                }
            }
            catch (System.Exception)
            { }

            // return System.Text.Encoding.GetEncoding("iso-8859-1");
            return System.Text.Encoding.UTF8;
        }


        public static void BrokenEncoding()
        {
            // https://stackoverflow.com/questions/700187/unicode-utf-ascii-ansi-format-differences

            System.Text.Encoding enc = System.Text.Encoding.GetEncoding("iso-8859-1");
            
            string fileName = @"D:\username\Documents\Visual Studio 2017\GitLab\COR-Basic-V4\Portal\Portal_Share\0\VWS.Legend.Load.sql";

            string content = System.IO.File.ReadAllText(fileName, System.Text.Encoding.UTF8);
            
            // Text encoded as iso
            byte[] bca = enc.GetBytes(content);
            // text wrongly decoded as utf8 
            content = new System.Text.UTF8Encoding(false).GetString(bca);


            // expected reversal 
            // bca = new System.Text.UTF8Encoding(false).GetBytes(content);
            // content = enc.GetString(bca);


            // This happens on browser:
            bca = enc.GetBytes(content);
            content = new System.Text.UTF8Encoding(false).GetString(bca);

            System.Console.WriteLine(content);
        }

        public static string XmlBeautifier(string xml)
        {
            string result = "";

            try
            {
                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.XmlResolver = null;

                // Load the XmlDocument with the XML.
                document.LoadXml(xml);


                using (System.IO.MemoryStream mStream = new System.IO.MemoryStream())
                {

                    using (System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(mStream, System.Text.Encoding.Unicode))
                    {
                        writer.Formatting = System.Xml.Formatting.Indented;

                        // Write the XML into a formatting XmlTextWriter
                        document.WriteContentTo(writer);
                        writer.Flush();
                        mStream.Flush();


                        // Have to rewind the MemoryStream in order to read
                        // its contents.
                        mStream.Position = 0;

                        // Read MemoryStream contents into a StreamReader.
                        using (System.IO.StreamReader sReader = new System.IO.StreamReader(mStream))
                        {
                            // Extract the text from the StreamReader.
                            result = sReader.ReadToEnd();
                        } // End Using sReader 

                    } // End Using writer 

                } // End Using mStream 

            }
            catch (System.Xml.XmlException)
            {
                // Handle the exception
            }

            return result;
        } // End Function XmlBeautifier 


        public static void SaveXml(string fileName, string outputFileName)
        {
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.XmlResolver = null;
            doc.Load(fileName);


            System.Xml.XmlWriterSettings settings = new System.Xml.XmlWriterSettings();
            settings.Encoding = System.Text.Encoding.UTF8;
            settings.Indent = true;
            settings.IndentChars = "  ";
            // settings.NewLineChars = System.Environment.NewLine;
            settings.NewLineChars = "\r\n";
            settings.OmitXmlDeclaration = true;


            using (System.Xml.XmlWriter xtw = System.Xml.XmlWriter.Create(outputFileName, settings))
            {
                doc.Save(xtw);
            } // End Using xtw 


            // using (System.Xml.XmlTextWriter xtwXMLwriter = new System.Xml.XmlTextWriter(outputFileName, System.Text.Encoding.UTF8))
            // {
            //     xtwXMLwriter.Formatting = System.Xml.Formatting.Indented;
            //     doc.Save(xtwXMLwriter);
            // }

        } // End Sub SaveXml 


        public static void XmlFileBeautifier()
        {
            string inputFile = @"D:\Stefan.Steiger\Desktop\3ba33caf-4d3a-4681-8231-dd8f8999467f.svg";
            string outputFile = @"D:\Stefan.Steiger\Desktop\333.svg";

            // string xml = System.IO.File.ReadAllText(inputFile, System.Text.Encoding.UTF8);
            // string result = XmlBeautifier(xml);
            // System.IO.File.WriteAllText(outputFile, result, System.Text.Encoding.UTF8);

            SaveXml(inputFile, outputFile);
        } // End Sub XmlFileBeautifier 


        static void Main(string[] args)
        {
            TestFactory();

            // LargeDataToXML();
            // DataToXML();

            string fileName = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Program.cs");
            fileName = System.IO.Path.GetFullPath(fileName);
            System.Console.WriteLine(fileName);


            string content = System.IO.File.ReadAllText(fileName, System.Text.Encoding.UTF8);
            // content = "public void Method()\n{\n}";


            // System.Threading.ReaderWriterLockSlim rlock = new System.Threading.ReaderWriterLockSlim();
            //var languageParser = new ColorCode.Parsing.LanguageParser(
            //    new ColorCode.Compilation.LanguageCompiler(
            //    ColorCode.Languages.CompiledLanguages, rlock)
            //    , ColorCode.Languages.LanguageRepository    
            //);



            {
                ColorCode.HtmlClassFormatter formatter = new ColorCode.HtmlClassFormatter();
                string html = formatter.GetHtmlString(content, ColorCode.Languages.CSharp);
                string css = formatter.GetCSSString();
                System.Console.WriteLine(html);
                System.Console.WriteLine(css);
            }


            {
                string html = new WikiPlex.Legacy.Colorizer.CodeColorizer().Colorize(content, ColorCode.Languages.CSharp);
                System.Console.WriteLine(html);
            }


            System.Console.WriteLine(System.Environment.NewLine);
            System.Console.WriteLine(" --- Press any key to continue --- ");
            System.Console.ReadKey();
        } // End Sub Main 


    } // End Class Program 


} // End Namespace DevTests 

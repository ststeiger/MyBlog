
namespace DevTests
{


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


        public static void foo()
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


        static void Main(string[] args)
        {
            string fileName = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,"..","..","..", "Program.cs");
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

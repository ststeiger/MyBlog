
namespace MyBlog
{


    public class RenderUtils
    {


        public RenderUtils()
        { }


        public static void PartialRenderView()
        {
            // System.Web.Mvc.Html.PartialExtensions  )
            // public static MvcHtmlString Partial(this HtmlHelper htmlHelper, string partialViewName);
            // public static MvcHtmlString Partial(this HtmlHelper htmlHelper, string partialViewName, object model);
            // public static MvcHtmlString Partial(this HtmlHelper htmlHelper, string partialViewName, ViewDataDictionary viewData);
            // public static MvcHtmlString Partial(this HtmlHelper htmlHelper, string partialViewName, object model, ViewDataDictionary viewData);
        }


        public static string RenderMediaWikiMarkup(string markup)
        {
            // TODO:
            return markup; 
            // return MediaWikiConverter.MediaWikiToXHTML(markup);
        }


        public static string RenderWikiPlexMarkup(string input)
        {
            // string input = "This is my wiki source!";
            WikiPlex.WikiEngine engine = new WikiPlex.WikiEngine();
            string HTML = engine.Render(input);

            return HTML;
        } // End Function RenderWikiPlexMarkup


        public static string RenderBbCode(string input)
        {
            // string strContent = Server.MapPath("~/DnsPost.txt");
            // strContent = System.IO.File.ReadAllText(strContent, System.Text.Encoding.UTF8);

            // strContent = "MonoPost.txt";

            // http://bbcode.codeplex.com/
            // strContent = "[url=http://codekicker.de]codekicker[url]";

            string strContent = input;

            CodeKicker.BBCode.BBCodeParser bbCodeParser = RenderUtils.InitBbCodeParser();


            strContent = strContent == null ? "" : strContent;
            //return bbCodeParser.ToHtml(str.NullToEmpty()).Replace("\r", "").Replace("\n", "<br/>");
            string str = bbCodeParser.ToHtml(strContent).Replace("\r", "").Replace("\n", "<br/>");


            string strHTML = @"<!doctype html>
<html itemscope="""" itemtype=""http://schema.org/WebPage"" lang=""en"">
<head>
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1"" />
    <meta charset=""utf-8"" />
    <title>Markdown Sample Rendering</title>
    <meta name=""viewport"" content=""width=device-width"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
    <link rel=""icon"" href=""/favicon.ico"" type=""image/x-icon"" />

    <style type=""text/css"" media=""all"">
    </style>
</head>
<body>
	{0}
</body>
</html>
";

            strHTML = string.Format(strHTML, str);

            return strHTML;
        } // End Function RenderBbCode


        public static string PageTemplate
        {
            get
            {
                return @"<!doctype html>
<html itemscope="""" itemtype=""http://schema.org/WebPage"" lang=""en"">
<head>
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1"" />
    <meta charset=""utf-8"" />
    <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
    <meta http-equiv=""cache-control"" content=""max-age=0"" />
    <meta http-equiv=""cache-control"" content=""no-cache"" />
    <meta http-equiv=""expires"" content=""0"" />
    <meta http-equiv=""expires"" content=""Tue, 01 Jan 1980 1:00:00 GMT"" />
    <meta http-equiv=""pragma"" content=""no-cache"" />
    
    <title>Markdown Sample Rendering</title>
    <meta name=""viewport"" content=""width=device-width"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
    <link rel=""icon"" href=""/favicon.ico"" type=""image/x-icon"" />

    <style type=""text/css"" media=""all"">
    </style>
</head>
<body>
	{0}
</body>
</html>
";

            }
        }


        public static string RenderMarkdown(string input)
        {
            // string input = "code sample:\n\n    <head>\n    <title>page title</title>\n    </head>\n";
            MarkdownSharp.Markdown m = new MarkdownSharp.Markdown();
            string strHTML = m.Transform(input);
            // strHTML = string.Format(PageTemplate, strHTML);
            m = null;

            return strHTML;
        } // End Function RenderMarkdown 


        private static string PrettifyEncode(string strContent)
        {
            if (string.IsNullOrEmpty(strContent))
                return "";

            strContent = strContent.Replace("\t", "      ").Replace(" ", "&nbsp;").Replace("\"", "&quot;").Replace("'", "&#39;");
            return strContent;
        } // End Function PrettifyEncode


        private static string HtmlEncode(string text)
        {
            // System.Web.HttpUtility.HtmlEncode("");
            // System.Net.WebUtility.HtmlEncode("test"); // (Since .NET Standard 1.0) :
            // System.Text.Encodings.Web.HtmlEncoder.Default.Encode(""); // in Net Core 2.0
            // System.Text.Encodings.Web.HtmlEncoder.Default.EncodeUtf8()
            return System.Text.Encodings.Web.HtmlEncoder.Default.Encode(""); // in Net Core 2.0
        }

        private static string HtmlDecode(string text)
        {
            return System.Net.WebUtility.HtmlDecode(text);
        }


        private static CodeKicker.BBCode.BBCodeParser InitBbCodeParser()
        {
            // ColorCode.Languages.r
            //  public BBTag(string name, string openTagTemplate, string closeTagTemplate, bool autoRenderContent, bool requireClosingTag, Func<string, string> contentTransformer, params BBAttribute[] attributes)


            System.Func<string, string> writeAsax = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.Asax));
            };

            System.Func<string, string> writeAshx = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.Ashx));
            };

            System.Func<string, string> writeAspx = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.Aspx));
            };


            System.Func<string, string> writeAspxCs = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.AspxCs));
            };

            System.Func<string, string> writeAspxVb = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.AspxVb));
            };

            System.Func<string, string> writeCpp = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.Cpp));
            };

            System.Func<string, string> writeCSharp = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.CSharp));
            };

            System.Func<string, string> writeCss = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.Css));
            };

            System.Func<string, string> writeFortran = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.Fortran));
            };


            System.Func<string, string> writeFSharp = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.FSharp));
            };

            System.Func<string, string> writeHaskell = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.Haskell));
            };

            System.Func<string, string> writeHtml = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.Html));
            };

            System.Func<string, string> writeJava = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.Java));
            };

            System.Func<string, string> writeJavaScript = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.JavaScript));
            };

            System.Func<string, string> writeKoka = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.Koka));
            };


            System.Func<string, string> writeMarkdown = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.Markdown));
            };

            System.Func<string, string> writePhp = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.Php));
            };


            System.Func<string, string> writePowerShell = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.PowerShell));
            };


            System.Func<string, string> writeSql = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.Sql));
            };


            System.Func<string, string> writeTypescript = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.Typescript));
            };


            System.Func<string, string> writeVbDotNet = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.VbDotNet));
            };


            System.Func<string, string> writeXml = delegate (string sourceCode) {
                ColorCode.HtmlFormatter hf = new ColorCode.HtmlFormatter(ColorCode.Styling.StyleDictionary.DefaultLight);
                return HtmlDecode(hf.GetHtmlString(sourceCode, ColorCode.Languages.Xml));
            };



            // http://codekicker.de/fragen/Codekicker.BBCode-BBCode-to-HTML/418
            CodeKicker.BBCode.BBCodeParser bbCodeParser = new CodeKicker.BBCode.BBCodeParser(CodeKicker.BBCode.ErrorMode.ErrorFree, null, new[]
            {
                new CodeKicker.BBCode.BBTag("b", "<b>", "</b>"),
                new CodeKicker.BBCode.BBTag("i", "<span style=\"font-style:italic;\">", "</span>"),
                new CodeKicker.BBCode.BBTag("u", "<span style=\"text-decoration:underline;\">", "</span>"),
                new CodeKicker.BBCode.BBTag("code", "<pre class=\"InsetBox\">", "</pre>", true, true, content => content.Trim()),
                new CodeKicker.BBCode.BBTag("img", "<img src=\"${content}\" />", "", false, true),
                new CodeKicker.BBCode.BBTag("quote", "<blockquote>", "</blockquote>", true, true, content => content.Trim()),
				// http://wikiplex.codeplex.com/wikipage?title=Syntax%20Highlighting&referringTitle=Documentation


				//new CodeKicker.BBCode.BBTag(System.Text.RegularExpressions.Regex.Escape("csharp"), "<div>", "</div>", true, true, content => Server.HtmlDecode(engine.Render("{code:c#}" + content + "{code:c#}"))),
			


                // new CodeKicker.BBCode.BBTag("asax", "<div>", "</div>", true, true, content => HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Asax))),
                new CodeKicker.BBCode.BBTag("asax", "<div>", "</div>", true, true, writeAsax),
                //new CodeKicker.BBCode.BBTag("ashx", "<div>", "</div>", true, true, content => HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Ashx))),
                new CodeKicker.BBCode.BBTag("ashx", "<div>", "</div>", true, true, writeAshx),
                // new CodeKicker.BBCode.BBTag("aspx", "<div>", "</div>", true, true, content => HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Aspx))),
                new CodeKicker.BBCode.BBTag("aspx", "<div>", "</div>", true, true, writeAspx),
                //new CodeKicker.BBCode.BBTag("aspxcs", "<div>", "</div>", true, true, content => HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.AspxCs))),
                new CodeKicker.BBCode.BBTag("aspxcs", "<div>", "</div>", true, true, writeAspxCs),
                //new CodeKicker.BBCode.BBTag("aspxvb", "<div>", "</div>", true, true, content => HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.AspxVb))),
                new CodeKicker.BBCode.BBTag("aspxvb", "<div>", "</div>", true, true, writeAspxVb),
                //new CodeKicker.BBCode.BBTag("CPP", "<div>", "</div>", true, true, content => HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Cpp))),
                new CodeKicker.BBCode.BBTag("CPP", "<div>", "</div>", true, true, writeCpp),
                //new CodeKicker.BBCode.BBTag("csharp", "<div>", "</div>", true, true, content => HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.CSharp))),
                new CodeKicker.BBCode.BBTag("csharp", "<div>", "</div>", true, true, writeCSharp),
                //new CodeKicker.BBCode.BBTag("CSS", "<div>", "</div>", true, true, content => HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Css))),
                new CodeKicker.BBCode.BBTag("CSS", "<div>", "</div>", true, true, writeCss),
                new CodeKicker.BBCode.BBTag("Fortran", "<div>", "</div>", true, true, writeFortran),
                new CodeKicker.BBCode.BBTag("FSharp", "<div>", "</div>", true, true, writeFSharp),
                new CodeKicker.BBCode.BBTag("Haskell", "<div>", "</div>", true, true, writeHaskell),
                //new CodeKicker.BBCode.BBTag("HTML", "<div>", "</div>", true, true, content => HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Html))),
                new CodeKicker.BBCode.BBTag("HTML", "<div>", "</div>", true, true, writeHtml),
                // new CodeKicker.BBCode.BBTag("Java", "<div>", "</div>", true, true, content => HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Java))),
                new CodeKicker.BBCode.BBTag("Java", "<div>", "</div>", true, true, writeJava),
                // new CodeKicker.BBCode.BBTag("JavaScript", "<div>", "</div>", true, true, content => HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.JavaScript))),
                new CodeKicker.BBCode.BBTag("JavaScript", "<div>", "</div>", true, true, writeJavaScript),
                // new CodeKicker.BBCode.BBTag("PHP", "<div>", "</div>", true, true, content => HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Php))),
                new CodeKicker.BBCode.BBTag("PHP", "<div>", "</div>", true, true, writePhp),
                // new CodeKicker.BBCode.BBTag("PowerShell", "<div>", "</div>", true, true, content => HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.PowerShell))),
                new CodeKicker.BBCode.BBTag("PowerShell", "<div>", "</div>", true, true, writePowerShell),
				// new CodeKicker.BBCode.BBTag("SQL", "<code>", "</code>", true, true, content => HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Sql))),
                new CodeKicker.BBCode.BBTag("SQL", "<code>", "</code>", true, true, writeSql),
				// new CodeKicker.BBCode.BBTag("VB", "<div>", "</div>", true, true, content => HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.VbDotNet))),
                new CodeKicker.BBCode.BBTag("VB", "<div>", "</div>", true, true, writeVbDotNet),
                // new CodeKicker.BBCode.BBTag("XML", "<div>", "</div>", true, true, content => HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Xml))),
                new CodeKicker.BBCode.BBTag("XML", "<div>", "</div>", true, true, writeXml),
                // new CodeKicker.BBCode.BBTag("XML", "<div>", "</div>", true, true, content => HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Xml))),


				// new CodeKicker.BBCode.BBTag("sql", "<pre class=\"prettyprint linenums lang-sql\">", "</pre>", true, true, content => Server.HtmlEncode(content).Replace("\t", "      ").Replace(" ", "&nbsp;")),
				// new CodeKicker.BBCode.BBTag("python", "<pre class=\"prettyprint linenums lang-py\">", "</pre>", true, true, content => Server.HtmlEncode(content).Replace("\t", "      ").Replace(" ", "&nbsp;")),

				// http://stackoverflow.com/questions/1219860/javascript-jquery-html-encoding
				// new CodeKicker.BBCode.BBTag("sql", "<pre class=\"prettyprint linenums lang-sql\">", "</pre>", true, true, content => PrettifyEncode(content)),
				new CodeKicker.BBCode.BBTag("python", "<pre class=\"prettyprint linenums lang-py\">", "</pre>", true, true, content => PrettifyEncode(content)),

				//new CodeKicker.BBCode.BBTag("csharp", "<div>", "</div>", true, true, content => new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.CSharp), new CodeKicker.BBCode.BBAttribute[] { CodeKicker.BBCode.HtmlEncodingMode.UnsafeDontEncode} ),

				new CodeKicker.BBCode.BBTag("list", "<ul>", "</ul>", true, true),
                new CodeKicker.BBCode.BBTag("*", "<li>", "</li>", true, false),
                new CodeKicker.BBCode.BBTag("url", "<a href=\"${href}\" rel=\"nofollow\">", "</a>", new CodeKicker.BBCode.BBAttribute("href", ""), new CodeKicker.BBCode.BBAttribute("href", "href")),
            });

            return bbCodeParser;
        } // End Function InitBbCodeParser


    } // End Class RenderUtils 


} // End Namespace MyBlog 

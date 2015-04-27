
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
            return MediaWikiConverter.MediaWikiToXHTML(markup);
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

			CodeKicker.BBCode.BBCodeParser bbCodeParser = RenderUtils.InitBbCodeParser ();


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


		private static CodeKicker.BBCode.BBCodeParser InitBbCodeParser()
		{
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

				new CodeKicker.BBCode.BBTag("asax", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Asax))),
				new CodeKicker.BBCode.BBTag("ashx", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Ashx))),
				new CodeKicker.BBCode.BBTag("aspx", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Aspx))),
				new CodeKicker.BBCode.BBTag("aspxcs", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.AspxCs))),
				new CodeKicker.BBCode.BBTag("aspxvb", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.AspxVb))),
				new CodeKicker.BBCode.BBTag("CPP", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Cpp))),
				new CodeKicker.BBCode.BBTag("csharp", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.CSharp))),
				new CodeKicker.BBCode.BBTag("CSS", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Css))),
				new CodeKicker.BBCode.BBTag("HTML", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Html))),
				new CodeKicker.BBCode.BBTag("Java", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Java))),
				new CodeKicker.BBCode.BBTag("JavaScript", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.JavaScript))),
				new CodeKicker.BBCode.BBTag("PHP", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Php))),
				new CodeKicker.BBCode.BBTag("PowerShell", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.PowerShell))),
				// new CodeKicker.BBCode.BBTag("SQL", "<code>", "</code>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Sql))),
				new CodeKicker.BBCode.BBTag("VB", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.VbDotNet))),
				new CodeKicker.BBCode.BBTag("XML", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Xml))),
                // new CodeKicker.BBCode.BBTag("XML", "<div>", "</div>", true, true, content => Server.HtmlDecode(new ColorCode.CodeColorizer().Colorize(content, ColorCode.Languages.Xml))),


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

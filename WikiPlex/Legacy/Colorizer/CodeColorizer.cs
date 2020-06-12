using System.IO;
using System.Reflection;

namespace ColorCode
{
    /// <summary>
    /// Defines the contract for a style sheet.
    /// </summary>
    public interface IStyleSheet
    {
        /// <summary>
        /// Gets the dictionary of styles for the style sheet.
        /// </summary>
        ColorCode.Styling.StyleDictionary Styles { get; }
    }

    /// <summary>
    /// Defines the contract for a source code formatter.
    /// </summary>
    public interface IFormatter
    {
        /// <summary>
        /// Writes the parsed source code to the ouput using the specified style sheet.
        /// </summary>
        /// <param name="parsedSourceCode">The parsed source code to format and write to the output.</param>
        /// <param name="scopes">The captured scopes for the parsed source code.</param>
        /// <param name="styleSheet">The style sheet according to which the source code will be formatted.</param>
        /// <param name="textWriter">The text writer to which the formatted source code will be written.</param>
        void Write(string parsedSourceCode,
            System.Collections.Generic.IList<ColorCode.Parsing.Scope> scopes,
            IStyleSheet styleSheet,
            System.IO.TextWriter textWriter);

        /// <summary>
        /// Generates and writes the footer to the output.
        /// </summary>
        /// <param name="styleSheet">The style sheet according to which the footer will be generated.</param>
        /// <param name="language">The language that is having its footer written.</param>
        /// <param name="textWriter">The text writer to which footer will be written.</param>
        void WriteFooter(IStyleSheet styleSheet,
            ILanguage language,
            System.IO.TextWriter textWriter);

        /// <summary>
        /// Generates and writes the header to the output.
        /// </summary>
        /// <param name="styleSheet">The style sheet according to which the header will be generated.</param>
        /// <param name="language">The language that is having its header written.</param>
        /// <param name="textWriter">The text writer to which header will be written.</param>
        void WriteHeader(IStyleSheet styleSheet,
            ILanguage language,
            System.IO.TextWriter textWriter);
    }


    public interface ICodeColorizer
    {
        string Colorize(string sourceCode, ILanguage language);

        void Colorize(string sourceCode, ILanguage language, System.IO.TextWriter textWriter);


        void Colorize(
            string sourceCode,
            ILanguage language,
            IFormatter formatter,
            IStyleSheet styleSheet,
            System.IO.TextWriter textWriter
        );
    }


    /// <summary>
    /// Provides easy access to ColorCode's built-in style sheets.
    /// </summary>
    public static class StyleSheets
    {
        /// <summary>
        /// Gets the default style sheet.
        /// </summary>
        /// <remarks>
        /// The default style sheet mimics the default colorization scheme used by Visual Studio 2008 to the extent possible.
        /// </remarks>
        public static IStyleSheet Default
        {
            get { return new ColorCode.Styling.StyleSheets.DefaultStyleSheet(); }
        }
    }

    /// <summary>
    /// Provides easy access to ColorCode's built-in formatters.
    /// </summary>
    public static class Formatters
    {
        /// <summary>
        /// Gets the default formatter.
        /// </summary>
        /// <remarks>
        /// The default formatter produces HTML with inline styles.
        /// </remarks>
        public static IFormatter Default
        {
            get { return new ColorCode.Formatting.HtmlFormatter(); }
        }
    }


    public class CodeColorizer
        : ICodeColorizer
    {
        private readonly ColorCode.Parsing.ILanguageParser languageParser;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeColorizer"/> class.
        /// </summary>
        public CodeColorizer()
        {
            System.Collections.Generic.Dictionary<string, ColorCode.Compilation.CompiledLanguage> compiledLanguages =
                (System.Collections.Generic.Dictionary<string, ColorCode.Compilation.CompiledLanguage>)
                typeof(Languages).GetField("CompiledLanguage", BindingFlags.Static | BindingFlags.NonPublic)
                    .GetValue(null);
            ColorCode.Common.LanguageRepository languageRepository =
                (ColorCode.Common.LanguageRepository) typeof(Languages)
                    .GetField("LanguageRepository", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);


            languageParser = new ColorCode.Parsing.LanguageParser(
                new ColorCode.Compilation.LanguageCompiler(
                compiledLanguages) // Languages.CompiledLanguages)
                , languageRepository // Languages.LanguageRepository
                
            );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeColorizer"/> class.
        /// </summary>
        /// <param name="languageParser">The language parser that the <see cref="CodeColorizer"/> instance will use for its lifetime.</param>
        public CodeColorizer(ColorCode.Parsing.ILanguageParser languageParser)
        {
            ColorCode.Common.Guard.ArgNotNull(languageParser, "languageParser");

            this.languageParser = languageParser;
        }


        /// <summary>
        /// Colorizes source code using the specified language, the default formatter, and the default style sheet.
        /// </summary>
        /// <param name="sourceCode">The source code to colorize.</param>
        /// <param name="language">The language to use to colorize the source code.</param>
        /// <returns>The colorized source code.</returns>
        public string Colorize(string sourceCode, ILanguage language)
        {
            System.Text.StringBuilder buffer = new System.Text.StringBuilder(sourceCode.Length * 2);

            using (TextWriter writer = new StringWriter(buffer))
            {
                Colorize(sourceCode, language, writer);
                writer.Flush();
            }

            return buffer.ToString();
        }


        /// <summary>
        /// Colorizes source code using the specified language, the default formatter, and the default style sheet.
        /// </summary>
        /// <param name="sourceCode">The source code to colorize.</param>
        /// <param name="language">The language to use to colorize the source code.</param>
        /// <param name="textWriter">The text writer to which the colorized source code will be written.</param>
        public void Colorize(string sourceCode, ILanguage language, TextWriter textWriter)
        {
            Colorize(sourceCode, language, Formatters.Default, StyleSheets.Default, textWriter);
        }


        /// <summary>
        /// Colorizes source code using the specified language, formatter, and style sheet.
        /// </summary>
        /// <param name="sourceCode">The source code to colorize.</param>
        /// <param name="language">The language to use to colorize the source code.</param>
        /// <param name="formatter">The formatter to use to colorize the source code.</param>
        /// <param name="styleSheet">The style sheet to use to colorize the source code.</param>
        /// <param name="textWriter">The text writer to which the colorized source code will be written.</param>
        public void Colorize(string sourceCode,
            ILanguage language,
            IFormatter formatter,
            IStyleSheet styleSheet,
            TextWriter textWriter)
        {
            ColorCode.Common.Guard.ArgNotNull(language, "language");
            ColorCode.Common.Guard.ArgNotNull(formatter, "formatter");
            ColorCode.Common.Guard.ArgNotNull(styleSheet, "styleSheet");
            ColorCode.Common.Guard.ArgNotNull(textWriter, "textWriter");

            formatter.WriteHeader(styleSheet, language, textWriter);

            languageParser.Parse(sourceCode, language,
                (parsedSourceCode, captures) => formatter.Write(parsedSourceCode, captures, styleSheet, textWriter));

            formatter.WriteFooter(styleSheet, language, textWriter);
        }
    }
}
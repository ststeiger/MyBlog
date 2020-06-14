
namespace WikiPlex.Legacy.Colorizer
{


    public class CodeColorizer
        : ICodeColorizer
    {
        private readonly ColorCode.Parsing.ILanguageParser languageParser;


        public CodeColorizer()
        {
            // NonInternalCodeColorizer();

            // ColorCodeCore: [assembly: InternalsVisibleTo("WikiPlex")]
            this.languageParser = new ColorCode.Parsing.LanguageParser(
                 new ColorCode.Compilation.LanguageCompiler(ColorCode.Languages.CompiledLanguages, ColorCode.Languages.CompileLock) 
                ,ColorCode.Languages.LanguageRepository
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
        /// Initializes a new instance of the <see cref="CodeColorizer"/> class.
        /// </summary>
        private static void NonInternalCodeColorizer()
        {
            System.Collections.Generic.Dictionary<string, ColorCode.Compilation.CompiledLanguage> compiledLanguages =
                (System.Collections.Generic.Dictionary<string, ColorCode.Compilation.CompiledLanguage>)
                typeof(ColorCode.Languages).GetField("CompiledLanguages", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                    .GetValue(null);
            ColorCode.Common.LanguageRepository languageRepository =
                (ColorCode.Common.LanguageRepository)typeof(ColorCode.Languages)
                    .GetField("LanguageRepository", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).GetValue(null);



            System.Threading.ReaderWriterLockSlim compileLock =
                (System.Threading.ReaderWriterLockSlim)typeof(ColorCode.Languages)
                    .GetField("CompileLock", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).GetValue(null);


            //this.
            var    languageParser = new ColorCode.Parsing.LanguageParser(
                new ColorCode.Compilation.LanguageCompiler(
                compiledLanguages, compileLock) // ColorCode.Languages.CompiledLanguages) 
                , languageRepository // ColorCode.Languages.LanguageRepository

            );


        }



        /// <summary>
        /// Colorizes source code using the specified language, the default formatter, and the default style sheet.
        /// </summary>
        /// <param name="sourceCode">The source code to colorize.</param>
        /// <param name="language">The language to use to colorize the source code.</param>
        /// <returns>The colorized source code.</returns>
        public string Colorize(string sourceCode, ColorCode.ILanguage language)
        {
            System.Text.StringBuilder buffer = new System.Text.StringBuilder(sourceCode.Length * 2);

            using (System.IO.TextWriter writer = new System.IO.StringWriter(buffer))
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
        public void Colorize(string sourceCode, ColorCode.ILanguage language, System.IO.TextWriter textWriter)
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
            ColorCode.ILanguage language,
            IFormatter formatter,
            IStyleSheet styleSheet,
            System.IO.TextWriter textWriter)
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
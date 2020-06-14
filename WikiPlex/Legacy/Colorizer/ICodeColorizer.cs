
namespace WikiPlex.Legacy.Colorizer
{


    public interface ICodeColorizer
    {
        string Colorize(string sourceCode, ColorCode.ILanguage language);

        void Colorize(string sourceCode, ColorCode.ILanguage language, System.IO.TextWriter textWriter);


        void Colorize(
            string sourceCode,
            ColorCode.ILanguage language,
            IFormatter formatter,
            IStyleSheet styleSheet,
            System.IO.TextWriter textWriter
        );
    }


}

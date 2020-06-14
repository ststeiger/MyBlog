
namespace WikiPlex.Legacy.Colorizer
{


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
            ColorCode.ILanguage language,
            System.IO.TextWriter textWriter);

        /// <summary>
        /// Generates and writes the header to the output.
        /// </summary>
        /// <param name="styleSheet">The style sheet according to which the header will be generated.</param>
        /// <param name="language">The language that is having its header written.</param>
        /// <param name="textWriter">The text writer to which header will be written.</param>
        void WriteHeader(IStyleSheet styleSheet,
            ColorCode.ILanguage language,
            System.IO.TextWriter textWriter);
    }


}

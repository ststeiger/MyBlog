
namespace WikiPlex.Legacy.Colorizer
{

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
        public static IFormatter Default { get { return new HtmlFormatter(); } }
    }


}

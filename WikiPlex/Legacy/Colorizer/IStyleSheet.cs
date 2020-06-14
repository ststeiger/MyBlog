
namespace WikiPlex.Legacy.Colorizer
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


}

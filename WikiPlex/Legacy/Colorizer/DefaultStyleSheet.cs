
// Copyright (c) Microsoft Corporation.  All rights reserved.


namespace WikiPlex.Legacy.Colorizer
{


    public class DefaultStyleSheet 
        : IStyleSheet
    {
        //public static readonly string DullRed = "rgb(163, 21, 21)"; // FromArgb(163, 21, 21);
        public static readonly string DullRed = "#A31515";
        private static readonly ColorCode.Styling.StyleDictionary styles;

        static DefaultStyleSheet()
        {
            styles = new ColorCode.Styling.StyleDictionary
                         {
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.PlainText)
                                 {
                                     Foreground = "black",
                                     Background = "white",
                                     ReferenceName = "plainText"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.HtmlServerSideScript)
                                 {
                                     Background = "yellow",
                                     ReferenceName = "htmlServerSideScript"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.HtmlComment)
                                 {
                                     Foreground = "green",
                                     ReferenceName = "htmlComment"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.HtmlTagDelimiter)
                                 {
                                     Foreground = "blue",
                                     ReferenceName = "htmlTagDelimiter"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.HtmlElementName)
                                 {
                                     Foreground = "dullred",
                                     ReferenceName = "htmlElementName"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.HtmlAttributeName)
                                 {
                                     Foreground = "red",
                                     ReferenceName = "htmlAttributeName"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.HtmlAttributeValue)
                                 {
                                     Foreground = "blue",
                                     ReferenceName = "htmlAttributeValue"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.HtmlOperator)
                                 {
                                     Foreground = "blue",
                                     ReferenceName = "htmlOperator"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.Comment)
                                 {
                                     Foreground = "green",
                                     ReferenceName = "comment"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.XmlDocTag)
                                 {
                                     Foreground = "gray",
                                     ReferenceName = "xmlDocTag"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.XmlDocComment)
                                 {
                                     Foreground = "green",
                                     ReferenceName = "xmlDocComment"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.String)
                                 {
                                     Foreground = "dullred",
                                     ReferenceName = "string"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.StringCSharpVerbatim)
                                 {
                                     Foreground = "dullred",
                                     ReferenceName = "stringCSharpVerbatim"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.Keyword)
                                 {
                                     Foreground = "blue",
                                     ReferenceName = "keyword"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.PreprocessorKeyword)
                                 {
                                     Foreground = "blue",
                                     ReferenceName = "preprocessorKeyword"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.HtmlEntity)
                                 {
                                     Foreground = "red",
                                     ReferenceName = "htmlEntity"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.XmlAttribute)
                                 {
                                     Foreground = "red",
                                     ReferenceName = "xmlAttribute"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.XmlAttributeQuotes)
                                 {
                                     Foreground = "black",
                                     ReferenceName = "xmlAttributeQuotes"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.XmlAttributeValue)
                                 {
                                     Foreground = "blue",
                                     ReferenceName = "xmlAttributeValue"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.XmlCDataSection)
                                 {
                                     Foreground = "gray",
                                     ReferenceName = "xmlCDataSection"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.XmlComment)
                                 {
                                     Foreground = "green",
                                     ReferenceName = "xmlComment"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.XmlDelimiter)
                                 {
                                     Foreground = "blue",
                                     ReferenceName = "xmlDelimiter"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.XmlName)
                                 {
                                     Foreground = "dullred",
                                     ReferenceName = "xmlName"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.ClassName)
                                 {
                                     Foreground = "mediumturquoise",
                                     ReferenceName = "className"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.CssSelector)
                                 {
                                     Foreground = "dullred",
                                     ReferenceName = "cssSelector"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.CssPropertyName)
                                 {
                                     Foreground = "red",
                                     ReferenceName = "cssPropertyName"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.CssPropertyValue)
                                 {
                                     Foreground = "blue",
                                     ReferenceName = "cssPropertyValue"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.SqlSystemFunction)
                                 {
                                     Foreground = "magenta",
                                     ReferenceName = "sqlSystemFunction"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.PowerShellAttribute)
                                 {
                                     Foreground = "powderblue",
                                     ReferenceName = "powershellAttribute"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.PowerShellOperator)
                                 {
                                     Foreground = "gray",
                                     ReferenceName = "powershellOperator"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.PowerShellType)
                                 {
                                     Foreground = "teal",
                                     ReferenceName = "powershellType"
                                 },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.PowerShellVariable)
                                 {
                                     Foreground = "orangered",
                                     ReferenceName = "powershellVariable"
                                 },

                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.Type)
                                {
                                    Foreground = "teal",
                                    ReferenceName = "type"
                                },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.TypeVariable)
                                {
                                    Foreground = "teal",
                                    Italic = true,
                                    ReferenceName = "typeVariable"
                                },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.NameSpace)
                                {
                                    Foreground = "navy",
                                    ReferenceName = "namespace"
                                },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.Constructor)
                                {
                                    Foreground = "purple",
                                    ReferenceName = "constructor"
                                },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.Predefined)
                                {
                                    Foreground = "navy",
                                    ReferenceName = "predefined"
                                },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.PseudoKeyword)
                                {
                                    Foreground = "navy",
                                    ReferenceName = "pseudoKeyword"
                                },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.StringEscape)
                                {
                                    Foreground = "gray",
                                    ReferenceName = "stringEscape"
                                },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.ControlKeyword)
                                {
                                    Foreground = "blue",
                                    ReferenceName = "controlKeyword"
                                },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.Number)
                                {
                                    ReferenceName = "number"
                                },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.Operator)
                                {
                                    ReferenceName = "operator"
                                },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.Delimiter)
                                {
                                    ReferenceName = "delimiter"
                                },

                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.MarkdownHeader)
                                {
                                    // Foreground = "Blue,
                                    Bold = true,
                                    ReferenceName = "markdownHeader"
                                },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.MarkdownCode)
                                {
                                    Foreground = "teal",
                                    ReferenceName = "markdownCode"
                                },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.MarkdownListItem)
                                {
                                    Bold = true,
                                    ReferenceName = "markdownListItem"
                                },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.MarkdownEmph)
                                {
                                    Italic = true,
                                    ReferenceName = "italic"
                                },
                             new ColorCode.Styling.Style(ColorCode.Common.ScopeName.MarkdownBold)
                                {
                                    Bold = true,
                                    ReferenceName = "bold"
                                },
                         };
        }

        public string Name
        {
            get { return "DefaultStyleSheet"; }
        }

        public ColorCode.Styling.StyleDictionary Styles
        {
            get { return styles; }
        }


    }


}

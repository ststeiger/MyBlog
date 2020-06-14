
// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace ColorCode.Styling.StyleSheets
{

    public class DefaultStyleSheet
        : IStyleSheet
    {
        //public static readonly string DullRed = "rgb(163, 21, 21)"; // FromArgb(163, 21, 21);
        public static readonly string DullRed = "#A31515";
        private static readonly ColorCode.StyleDictionary styles;

        static DefaultStyleSheet()
        {
            styles = new ColorCode.StyleDictionary
                         {
                             new ColorCode.Style(ColorCode.Common.ScopeName.PlainText)
                                 {
                                     Foreground = "black",
                                     Background = "white",
                                     CssClassName = "plainText"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.HtmlServerSideScript)
                                 {
                                     Background = "yellow",
                                     CssClassName = "htmlServerSideScript"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.HtmlComment)
                                 {
                                     Foreground = "green",
                                     CssClassName = "htmlComment"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.HtmlTagDelimiter)
                                 {
                                     Foreground = "blue",
                                     CssClassName = "htmlTagDelimiter"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.HtmlElementName)
                                 {
                                     Foreground = "dullred",
                                     CssClassName = "htmlElementName"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.HtmlAttributeName)
                                 {
                                     Foreground = "red",
                                     CssClassName = "htmlAttributeName"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.HtmlAttributeValue)
                                 {
                                     Foreground = "blue",
                                     CssClassName = "htmlAttributeValue"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.HtmlOperator)
                                 {
                                     Foreground = "blue",
                                     CssClassName = "htmlOperator"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.Comment)
                                 {
                                     Foreground = "green",
                                     CssClassName = "comment"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.XmlDocTag)
                                 {
                                     Foreground = "gray",
                                     CssClassName = "xmlDocTag"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.XmlDocComment)
                                 {
                                     Foreground = "green",
                                     CssClassName = "xmlDocComment"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.String)
                                 {
                                     Foreground = "dullred",
                                     CssClassName = "string"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.StringCSharpVerbatim)
                                 {
                                     Foreground = "dullred",
                                     CssClassName = "stringCSharpVerbatim"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.Keyword)
                                 {
                                     Foreground = "blue",
                                     CssClassName = "keyword"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.PreprocessorKeyword)
                                 {
                                     Foreground = "blue",
                                     CssClassName = "preprocessorKeyword"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.HtmlEntity)
                                 {
                                     Foreground = "red",
                                     CssClassName = "htmlEntity"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.XmlAttribute)
                                 {
                                     Foreground = "red",
                                     CssClassName = "xmlAttribute"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.XmlAttributeQuotes)
                                 {
                                     Foreground = "black",
                                     CssClassName = "xmlAttributeQuotes"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.XmlAttributeValue)
                                 {
                                     Foreground = "blue",
                                     CssClassName = "xmlAttributeValue"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.XmlCDataSection)
                                 {
                                     Foreground = "gray",
                                     CssClassName = "xmlCDataSection"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.XmlComment)
                                 {
                                     Foreground = "green",
                                     CssClassName = "xmlComment"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.XmlDelimiter)
                                 {
                                     Foreground = "blue",
                                     CssClassName = "xmlDelimiter"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.XmlName)
                                 {
                                     Foreground = "dullred",
                                     CssClassName = "xmlName"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.ClassName)
                                 {
                                     Foreground = "mediumturquoise",
                                     CssClassName = "className"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.CssSelector)
                                 {
                                     Foreground = "dullred",
                                     CssClassName = "cssSelector"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.CssPropertyName)
                                 {
                                     Foreground = "red",
                                     CssClassName = "cssPropertyName"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.CssPropertyValue)
                                 {
                                     Foreground = "blue",
                                     CssClassName = "cssPropertyValue"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.SqlSystemFunction)
                                 {
                                     Foreground = "magenta",
                                     CssClassName = "sqlSystemFunction"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.PowerShellAttribute)
                                 {
                                     Foreground = "powderblue",
                                     CssClassName = "powershellAttribute"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.PowerShellOperator)
                                 {
                                     Foreground = "gray",
                                     CssClassName = "powershellOperator"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.PowerShellType)
                                 {
                                     Foreground = "teal",
                                     CssClassName = "powershellType"
                                 },
                             new ColorCode.Style(ColorCode.Common.ScopeName.PowerShellVariable)
                                 {
                                     Foreground = "orangered",
                                     CssClassName = "powershellVariable"
                                 },

                             new ColorCode.Style(ColorCode.Common.ScopeName.Type)
                                {
                                    Foreground = "teal",
                                    CssClassName = "type"
                                },
                             new ColorCode.Style(ColorCode.Common.ScopeName.TypeVariable)
                                {
                                    Foreground = "teal",
                                    Italic = true,
                                    CssClassName = "typeVariable"
                                },
                             new ColorCode.Style(ColorCode.Common.ScopeName.NameSpace)
                                {
                                    Foreground = "navy",
                                    CssClassName = "namespace"
                                },
                             new ColorCode.Style(ColorCode.Common.ScopeName.Constructor)
                                {
                                    Foreground = "purple",
                                    CssClassName = "constructor"
                                },
                             new ColorCode.Style(ColorCode.Common.ScopeName.Predefined)
                                {
                                    Foreground = "navy",
                                    CssClassName = "predefined"
                                },
                             new ColorCode.Style(ColorCode.Common.ScopeName.PseudoKeyword)
                                {
                                    Foreground = "navy",
                                    CssClassName = "pseudoKeyword"
                                },
                             new ColorCode.Style(ColorCode.Common.ScopeName.StringEscape)
                                {
                                    Foreground = "gray",
                                    CssClassName = "stringEscape"
                                },
                             new ColorCode.Style(ColorCode.Common.ScopeName.ControlKeyword)
                                {
                                    Foreground = "blue",
                                    CssClassName = "controlKeyword"
                                },
                             new ColorCode.Style(ColorCode.Common.ScopeName.Number)
                                {
                                    CssClassName = "number"
                                },
                             new ColorCode.Style(ColorCode.Common.ScopeName.Operator)
                                {
                                    CssClassName = "operator"
                                },
                             new ColorCode.Style(ColorCode.Common.ScopeName.Delimiter)
                                {
                                    CssClassName = "delimiter"
                                },

                             new ColorCode.Style(ColorCode.Common.ScopeName.MarkdownHeader)
                                {
                                    // Foreground = "Blue,
                                    Bold = true,
                                    CssClassName = "markdownHeader"
                                },
                             new ColorCode.Style(ColorCode.Common.ScopeName.MarkdownCode)
                                {
                                    Foreground = "teal",
                                    CssClassName = "markdownCode"
                                },
                             new ColorCode.Style(ColorCode.Common.ScopeName.MarkdownListItem)
                                {
                                    Bold = true,
                                    CssClassName = "markdownListItem"
                                },
                             new ColorCode.Style(ColorCode.Common.ScopeName.MarkdownEmph)
                                {
                                    Italic = true,
                                    CssClassName = "italic"
                                },
                             new ColorCode.Style(ColorCode.Common.ScopeName.MarkdownBold)
                                {
                                    Bold = true,
                                    CssClassName = "bold"
                                },
                         };
        }

        public string Name
        {
            get { return "DefaultStyleSheet"; }
        }

        public ColorCode.StyleDictionary Styles
        {
            get { return styles; }
        }


    }


}

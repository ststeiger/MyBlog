
// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace WikiPlex.Legacy.Colorizer
{


    public class HtmlFormatter 
        : IFormatter
    {
        public void Write(string parsedSourceCode,
                          System.Collections.Generic.IList<ColorCode.Parsing.Scope> scopes,
                          IStyleSheet styleSheet,
                          System.IO.TextWriter textWriter)
        {
            System.Collections.Generic.List<ColorCode.Common.TextInsertion> styleInsertions = new System.Collections.Generic.List<ColorCode.Common.TextInsertion>();

            foreach (ColorCode.Parsing.Scope scope in scopes)
                GetStyleInsertionsForCapturedStyle(scope, styleInsertions);

            ColorCode.Common.ExtensionMethods.SortStable(styleInsertions, (x, y) => x.Index.CompareTo(y.Index));

            int offset = 0;

            foreach (ColorCode.Common.TextInsertion styleInsertion in styleInsertions)
            {
                textWriter.Write(System.Web.HttpUtility.HtmlEncode(parsedSourceCode.Substring(offset, styleInsertion.Index - offset)));
                if (string.IsNullOrEmpty(styleInsertion.Text))
                    BuildSpanForCapturedStyle(styleInsertion.Scope, styleSheet, textWriter);
                else
                    textWriter.Write(styleInsertion.Text);
                offset = styleInsertion.Index;
            }

            textWriter.Write(System.Web.HttpUtility.HtmlEncode(parsedSourceCode.Substring(offset)));
        }

        public void WriteFooter(IStyleSheet styleSheet,
                                ColorCode.ILanguage language,
                                System.IO.TextWriter textWriter)
        {
            ColorCode.Common.Guard.ArgNotNull(styleSheet, "styleSheet");
            ColorCode.Common.Guard.ArgNotNull(textWriter, "textWriter");
            
            textWriter.WriteLine();
            WriteHeaderPreEnd(textWriter);
            WriteHeaderDivEnd(textWriter);
        }

        public void WriteHeader(IStyleSheet styleSheet,
                                ColorCode.ILanguage language,
                                System.IO.TextWriter textWriter)
        {
            ColorCode.Common.Guard.ArgNotNull(styleSheet, "styleSheet");
            ColorCode.Common.Guard.ArgNotNull(textWriter, "textWriter");
            
            WriteHeaderDivStart(styleSheet, textWriter);
            WriteHeaderPreStart(textWriter);
            textWriter.WriteLine();
        }

        private static void GetStyleInsertionsForCapturedStyle(ColorCode.Parsing.Scope scope
            , System.Collections.Generic.ICollection<ColorCode.Common.TextInsertion> styleInsertions)
        {
            styleInsertions.Add(new ColorCode.Common.TextInsertion {
                                                      Index = scope.Index,
                                                      Scope = scope
                                                  });


            foreach (ColorCode.Parsing.Scope childScope in scope.Children)
                GetStyleInsertionsForCapturedStyle(childScope, styleInsertions);

            styleInsertions.Add(new ColorCode.Common.TextInsertion {
                                                      Index = scope.Index + scope.Length,
                                                      Text = "</span>"
                                                  });
        }

        private static void BuildSpanForCapturedStyle(ColorCode.Parsing.Scope scope,
                                                        IStyleSheet styleSheet,
                                                        System.IO.TextWriter writer)
        {
            string foreground = string.Empty;
            string background = string.Empty;
            bool italic = false;
            bool bold = false;

            if (styleSheet.Styles.Contains(scope.Name))
            {
                ColorCode.Styling.Style style = styleSheet.Styles[scope.Name];

                foreground = style.Foreground;
                background = style.Background;
                italic = style.Italic;
                bold = style.Bold;
            }

            WriteElementStart(writer, "span", foreground, background, italic, bold);
        }

        private static void WriteHeaderDivEnd(System.IO.TextWriter writer)
        {
            WriteElementEnd("div", writer);
        }

        private static void WriteElementEnd(string elementName,
                                            System.IO.TextWriter writer)
        {
            writer.Write("</{0}>", elementName);
        }

        private static void WriteHeaderPreEnd(System.IO.TextWriter writer)
        {
            WriteElementEnd("pre", writer);
        }

        private static void WriteHeaderPreStart(System.IO.TextWriter writer)
        {
            WriteElementStart(writer,"pre");
        }

        private static void WriteHeaderDivStart(IStyleSheet styleSheet,
                                                System.IO.TextWriter writer)
        {
            string foreground = string.Empty;
            string background = string.Empty;

            if (styleSheet.Styles.Contains(ColorCode.Common.ScopeName.PlainText))
            {
                ColorCode.Styling.Style plainTextStyle = styleSheet.Styles[ColorCode.Common.ScopeName.PlainText];

                foreground = plainTextStyle.Foreground;
                background = plainTextStyle.Background;
            }

            WriteElementStart(writer, "div", foreground, background );
        }

        private static void WriteElementStart(System.IO.TextWriter writer,
                                              string elementName)
        {
            WriteElementStart(writer, elementName, string.Empty, string.Empty);
        }

        private static void WriteElementStart(System.IO.TextWriter writer,
                                              string elementName,
                                              string foreground,
                                              string background,
                                              bool italic = false,
                                              bool bold = false
                                              )
        {
            writer.Write("<{0}", elementName);

            if (foreground != string.Empty || background != string.Empty || italic || bold)
            {
                writer.Write(" style=\"");

                if (foreground != string.Empty)
                    writer.Write("color:{0};", foreground);

                if (background != string.Empty)
                    writer.Write("background-color:{0};", background);

                if (italic)
                    writer.Write("font-style: italic;");

                if (bold)
                    writer.Write("font-weight: bold;");

                writer.Write("\"");
            }

            writer.Write(">");
        }
    }
}
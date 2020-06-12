﻿﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using WikiPlex.Common;
using WikiPlex.Syndication;

 
namespace WikiPlex.Formatting.Renderers
{
    /// <summary>
    /// Will render the syndicated feed scopes.
    /// </summary>
    public class SyndicatedFeedRenderer : Renderer
    {
        private readonly ISyndicationReader syndicationReader;
        private readonly WikiPlex.Syndication.IXmlDocumentReader xmlDocumentReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyndicatedFeedRenderer"/>.
        /// </summary>
        public SyndicatedFeedRenderer()
            : this(new WikiPlex.Syndication.XmlDocumentReaderWrapper(), new SyndicationReader())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SyndicatedFeedRenderer"/>.
        /// </summary>
        /// <param name="xmlDocumentReader">The xml document reader.</param>
        /// <param name="syndicationReader">The syndication reader.</param>
        public SyndicatedFeedRenderer(WikiPlex.Syndication.IXmlDocumentReader xmlDocumentReader, ISyndicationReader syndicationReader)
        {
            this.xmlDocumentReader = xmlDocumentReader;
            this.syndicationReader = syndicationReader;
        }

        /// <summary>
        /// Gets the collection of scope names for this <see cref="IRenderer"/>.
        /// </summary>
        protected override ICollection<string> ScopeNames
        {
            get { return new[] { ScopeName.SyndicatedFeed }; }
        }

        /// <summary>
        /// Gets the invalid argument error text.
        /// </summary>
        protected override string InvalidArgumentError
        {
            get { return "Cannot resolve syndicated feed macro, invalid parameter '{0}'."; }
        }

        /// <summary>
        /// Will expand the input into the appropriate content based on scope.
        /// </summary>
        /// <param name="scopeName">The scope name.</param>
        /// <param name="input">The input to be expanded.</param>
        /// <param name="htmlEncode">Function that will html encode the output.</param>
        /// <param name="attributeEncode">Function that will html attribute encode the output.</param>
        /// <returns>The expanded content.</returns>
        protected override string PerformExpand(string scopeName, string input, Func<string, string> htmlEncode, Func<string, string> attributeEncode)
        {
            string[] parameters = input.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

            string url = Parameters.ExtractUrl(parameters, false);
            string maxParameter, titlesOnlyParameter;
            int max = 20;
            bool titlesOnly = false;

            if (Parameters.TryGetValue(parameters, "max", out maxParameter)
                && (!int.TryParse(maxParameter, out max) || max <= 0 || max > 20))
                throw new ArgumentException("Invalid parameter.", "max");

            if (Parameters.TryGetValue(parameters, "titlesOnly", out titlesOnlyParameter)
                && !bool.TryParse(titlesOnlyParameter, out titlesOnly))
                throw new ArgumentException("Invalid parameter.", "titlesOnly");

            var content = new StringBuilder();
            using (var tw = new StringWriter(content))
            using (var writer = new WikiPlex.Legacy.HtmlTextWriter(tw, string.Empty))
            {
                writer.NewLine = string.Empty;
                RenderFeed(url, titlesOnly, max, writer);
            }

            return content.ToString();
        }

        /// <summary>
        /// Handles rendering a feed.
        /// </summary>
        /// <param name="url">The url to read the feed from.</param>
        /// <param name="titlesOnly">Indicates if only titles should be displayed.</param>
        /// <param name="max">The maximum number of entries to display.</param>
        /// <param name="writer">The text writer to write to.</param>
        protected virtual void RenderFeed(string url, bool titlesOnly, int max, WikiPlex.Legacy.HtmlTextWriter writer)
        {
            XmlDocument xdoc = xmlDocumentReader.Read(url);
            Guard.NotNull(xdoc, "url");

            SyndicationFeed feed = syndicationReader.Read(xdoc);

            writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Class, "rss");
            writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Div);

            RenderAccentBar(writer, feed.Title);

            for (int i = 0; i < feed.Items.Count(); i++)
            {
                if (i >= max)
                    break;

                SyndicationItem item = feed.Items.ElementAt(i);

                writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Class, "entry");
                writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Div);
                writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Class, "title");
                writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Div);
                writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Href, item.Link, false);
                writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.A);
                writer.Write(item.Title);
                writer.RenderEndTag(); //a
                writer.RenderEndTag(); // div

                writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Class, "moreinfo");
                writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Div);
                writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Class, "date");
                writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Span);
                writer.Write(item.Date);
                writer.RenderEndTag(); // span
                writer.Write(" &nbsp;|&nbsp; ");
                writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Class, "source");
                writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Span);
                writer.Write("From ");
                writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Target, "_blank");
                writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Href, url, false);
                writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.A);
                writer.Write(feed.Title);
                writer.RenderEndTag(); // a
                writer.RenderEndTag(); // span
                writer.RenderEndTag(); // div

                if (!titlesOnly)
                {
                    writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.P);
                    writer.Write(item.Description);
                    writer.RenderEndTag(); // p
                }

                writer.RenderEndTag(); // div
            }

            RenderAccentBar(writer, feed.Title);
            writer.RenderEndTag(); // div
        }

        /// <summary>
        /// Handles rendering the accent bar.
        /// </summary>
        /// <param name="writer">The text writer to write to.</param>
        /// <param name="title">The title to write.</param>
        protected static void RenderAccentBar(WikiPlex.Legacy.HtmlTextWriter writer, string title)
        {
            writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Class, "accentbar");
            writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Div);
            writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Class, "left");
            writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Span);
            writer.Write("&nbsp;");
            writer.RenderEndTag(); // span
            writer.Write(title + " News Feed");
            writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Class, "right");
            writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Span);
            writer.Write("&nbsp;");
            writer.RenderEndTag(); // span
            writer.RenderEndTag(); // div
        }
    }
}
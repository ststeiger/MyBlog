﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WikiPlex.Common;

namespace WikiPlex.Formatting.Renderers
{
    /// <summary>
    /// Will render all the video scopes.
    /// </summary>
    public class VideoRenderer : Renderer
    {
        /// <summary>
        /// Gets the collection of scope names for this <see cref="IRenderer"/>.
        /// </summary>
        protected override ICollection<string> ScopeNames
        {
            get
            {
                return new[] {
                                ScopeName.Channel9Video, ScopeName.FlashVideo, ScopeName.QuickTimeVideo,
                                ScopeName.RealPlayerVideo, ScopeName.VimeoVideo, ScopeName.WindowsMediaVideo,
                                ScopeName.YouTubeVideo, ScopeName.InvalidVideo
                           };
            }
        }

        /// <summary>
        /// Gets the invalid argument error text.
        /// </summary>
        protected override string InvalidArgumentError
        {
            get { return "Cannot resolve video macro, invalid parameter '{0}'."; }
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
            if (scopeName == ScopeName.InvalidVideo)
                throw new ArgumentException("Invalid video type.", "type");

            string[] parameters = input.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            string url = Parameters.ExtractUrl(parameters);
            WikiPlex.Legacy.HorizontalAlign align = Parameters.ExtractAlign(parameters, WikiPlex.Legacy.HorizontalAlign.Center);

            IVideoRenderer videoRenderer = GetVideoRenderer(scopeName);
            videoRenderer.Dimensions = Parameters.ExtractDimensions(parameters, 285, 320);

            var content = new StringBuilder();
            using (var tw = new StringWriter(content))
            using (var writer = new WikiPlex.Legacy.HtmlTextWriter(tw, string.Empty))
            {
                writer.NewLine = string.Empty;
                writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Class, "video");
                writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Style, string.Format("text-align:{0}", align));
                writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Div);

                writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Class, "player");
                writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Span);

                videoRenderer.Render(url, writer);
                    
                writer.RenderEndTag(); // </span>

                writer.Write("<br />");

                writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Class, "external");
                writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Span);
                writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Href, url);
                writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Target, "_blank");
                writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.A);
                writer.Write("Launch in another window");
                writer.RenderEndTag();
                writer.RenderEndTag();

                writer.RenderEndTag(); // </div>
            }

            return content.ToString();
        }

        private static IVideoRenderer GetVideoRenderer(string scopeName)
        {
            switch (scopeName)
            {
                case ScopeName.Channel9Video:
                    return new Channel9VideoRenderer();
                case ScopeName.FlashVideo:
                    return new FlashVideoRenderer();
                case ScopeName.QuickTimeVideo:
                    return new QuickTimeVideoRenderer();
                case ScopeName.RealPlayerVideo:
                    return new RealPlayerVideoRenderer();
                case ScopeName.VimeoVideo:
                    return new VimeoVideoRenderer();
                case ScopeName.WindowsMediaVideo:
                    return new WindowsMediaPlayerVideoRenderer();
                case ScopeName.YouTubeVideo:
                    return new YouTubeVideoRenderer();
                default:
                    return null;
            }
        }
    }
}
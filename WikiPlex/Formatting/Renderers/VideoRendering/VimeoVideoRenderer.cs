
namespace WikiPlex.Formatting.Renderers
{
    internal class VimeoVideoRenderer : EmbeddedVideoRender
    {
        private static readonly System.Text.RegularExpressions.Regex VideoIdRegex = 
            new System.Text.RegularExpressions.Regex(@"^http://(?:www\.)?vimeo\.com/(.+)$");
        const string WModeAttributeString = "transparent";
        const string SrcSttributeFormatString = "http://vimeo.com/moogaloop.swf?clip_id={0}&server=vimeo.com&show_title=1&show_byline=1&show_portrait=1&color=&fullscreen=1&autoplay=0&loop=0";

        protected override void AddObjectTagAttributes(string url)
        {
            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Height, Dimensions.Height.ToString());
            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Width, Dimensions.Width.ToString());
        }

        protected override void AddParameterTags(string url)
        {
            AddParameterTag("movie", string.Format(SrcSttributeFormatString, WikiPlex.Common.Utility.ExtractFragment(VideoIdRegex, url)));
            AddParameterTag("wmode", WModeAttributeString);
            AddParameterTag("allowfullscreen", "true");
            AddParameterTag("allowscriptaccess", "always");
        }

        protected override void AddEmbedTagAttributes(string url)
        {
            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Height, Dimensions.Height.ToString());
            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Width, Dimensions.Width.ToString());
            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Type, "application/x-shockwave-flash");
            AddAttribute("wmode", WModeAttributeString);
            AddAttribute("allowfullscreen", "true");
            AddAttribute("allowscriptaccess", "always");

            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Src, string.Format(SrcSttributeFormatString, WikiPlex.Common.Utility.ExtractFragment(VideoIdRegex, url)));
        }
    }
}

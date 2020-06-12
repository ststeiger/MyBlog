
namespace WikiPlex.Formatting.Renderers
{
    internal class YouTubeVideoRenderer : EmbeddedVideoRender
    {
        private static readonly System.Text.RegularExpressions.Regex VideoIdRegex = 
            new System.Text.RegularExpressions.Regex(@"^http://www\.youtube\.com/watch\?v=(.+)$");
        const string WModeAttributeString = "transparent";
        const string SrcSttributeFormatString = "http://www.youtube.com/v/{0}";

        protected override void AddObjectTagAttributes(string url)
        {
            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Height, Dimensions.Height.ToString());
            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Width, Dimensions.Width.ToString());
        }

        protected override void AddParameterTags(string url)
        {
            AddParameterTag("movie", string.Format(SrcSttributeFormatString, WikiPlex.Common.Utility.ExtractFragment(VideoIdRegex, url)));
            AddParameterTag("wmode", WModeAttributeString);
        }

        protected override void AddEmbedTagAttributes(string url)
        {
            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Height, Dimensions.Height.ToString());
            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Width, Dimensions.Width.ToString());
            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Type, "application/x-shockwave-flash");
            AddAttribute("wmode", WModeAttributeString);

            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Src, string.Format(SrcSttributeFormatString, WikiPlex.Common.Utility.ExtractFragment(VideoIdRegex, url)));
        }
    }
}
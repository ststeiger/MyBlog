
namespace WikiPlex.Formatting.Renderers
{
    internal class Channel9VideoRenderer : IVideoRenderer
    {
        private const string DimensionErrorText =
            "Cannot resolve video macro, invalid parameter '{0}'. Value can only be pixel based.";

        public WikiPlex.Common.Dimensions Dimensions { get; set; }

        public void Render(string url, WikiPlex.Legacy.HtmlTextWriter writer)
        {
            if (Dimensions.Height.Value.Type != WikiPlex.Legacy.UnitType.Pixel)
                throw new WikiPlex.Common.RenderException(string.Format(DimensionErrorText, "height"));
            if (Dimensions.Width.Value.Type != WikiPlex.Legacy.UnitType.Pixel)
                throw new WikiPlex.Common.RenderException(string.Format(DimensionErrorText, "width"));

            var actualUri = new System.Uri(url);
            url = actualUri.GetLeftPart(System.UriPartial.Path);

            if (url[url.Length - 1] != '/')
                url += "/";
            if (!url.EndsWith("/player/", System.StringComparison.OrdinalIgnoreCase))
                url += "player";

            writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Src,
                url + "?h=" + Dimensions.Height.Value.Value + "&w=" + Dimensions.Width.Value.Value, false);
            writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Width, Dimensions.Width.ToString());
            writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Height, Dimensions.Height.ToString());
            writer.AddAttribute("scrolling", "no");
            writer.AddAttribute("frameborder", "0");
            writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Iframe);
            writer.RenderEndTag();
        }
    }
}
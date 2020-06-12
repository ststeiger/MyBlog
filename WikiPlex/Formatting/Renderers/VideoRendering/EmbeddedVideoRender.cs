
namespace WikiPlex.Formatting.Renderers
{
    internal abstract class EmbeddedVideoRender : IVideoRenderer
    {
        private WikiPlex.Legacy.HtmlTextWriter writer;

        public WikiPlex.Common.Dimensions Dimensions { get; set; }

        public void Render(string url, WikiPlex.Legacy.HtmlTextWriter writer)
        {
            this.writer = writer;

            AddObjectTagAttributes(url);
            writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Object);

            AddParameterTags(url);

            AddEmbedTagAttributes(url);
            writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Embed);
            writer.RenderEndTag();

            writer.RenderEndTag(); // </object>
            this.writer = null;
        }

        protected abstract void AddObjectTagAttributes(string url);
        protected abstract void AddParameterTags(string url);
        protected abstract void AddEmbedTagAttributes(string url);

        protected void AddAttribute(string key, string value)
        {
            writer.AddAttribute(key, value);
        }

        protected void AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute key, string value)
        {
            writer.AddAttribute(key, value);
        }

        protected void AddParameterTag(string name, string value)
        {
            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Name, name);
            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Value, value);
            writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Param);
            writer.RenderEndTag();
        }
    }
}
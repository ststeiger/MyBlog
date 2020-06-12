
namespace WikiPlex.Formatting.Renderers
{
    internal abstract class PluginVideoRenderer : EmbeddedVideoRender
    {
        protected abstract string ClassIdAttribute { get; }
        protected abstract string CodebaseAttribute { get; }
        protected abstract string PluginsPageAttribute { get; }
        protected abstract string TypeAttribute { get; }

        protected override void AddObjectTagAttributes(string url)
        {
            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Type, TypeAttribute);
            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Height, Dimensions.Height.ToString());
            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Width, Dimensions.Width.ToString());
            AddAttribute("classid", ClassIdAttribute);
            AddAttribute("codebase", CodebaseAttribute);
        }

        protected override void AddEmbedTagAttributes(string url)
        {
            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Type, TypeAttribute);
            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Height, Dimensions.Height.ToString());
            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Width, Dimensions.Width.ToString());
            AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Src, url);
            AddAttribute("pluginspage", PluginsPageAttribute);
            AddAttribute("autoplay", "false");
            AddAttribute("autostart", "false");
        }
    }
}
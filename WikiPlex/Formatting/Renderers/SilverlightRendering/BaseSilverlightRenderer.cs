
namespace WikiPlex.Formatting.Renderers
{
    internal abstract class BaseSilverlightRenderer : ISilverlightRenderer
    {
        public abstract string DataMimeType { get; }
        public abstract string ObjectType { get; }
        public abstract string DownloadUrl { get; }

        public void AddObjectTagAttributes(WikiPlex.Legacy.HtmlTextWriter writer)
        {
            writer.AddAttribute("data", DataMimeType);
            writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Type, ObjectType);
        }

        public virtual void AddParameterTags(string url, bool gpuAcceleration, string[] initParams,
            WikiPlex.Legacy.HtmlTextWriter writer)
        {
            AddParameter("source", url, writer);
            AddParameter(gpuAcceleration ? "enableGPUAcceleration" : "windowless", "true", writer);

            if (initParams.Length > 0)
                AddParameter("initParams", string.Join(",", initParams), writer);
        }

        public void AddDownloadLink(WikiPlex.Legacy.HtmlTextWriter writer)
        {
            writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.P);

            writer.Write("You need to install Microsoft Silverlight to view this content. ");

            writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Href, DownloadUrl, false);
            writer.AddStyleAttribute(WikiPlex.Legacy.HtmlTextWriterStyle.TextDecoration, "none");
            writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.A);

            writer.Write("Get Silverlight!<br />");

            writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Src,
                "http://go.microsoft.com/fwlink/?LinkID=108181", false);
            writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Alt, "Get Microsoft Silverlight");
            writer.AddStyleAttribute(WikiPlex.Legacy.HtmlTextWriterStyle.BorderStyle, "none");
            writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Img);
            writer.RenderEndTag(); // img

            writer.RenderEndTag(); // a
            writer.RenderEndTag(); // p
        }

        protected void AddParameter(string name, string value, WikiPlex.Legacy.HtmlTextWriter writer)
        {
            writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Name, name);
            writer.AddAttribute(WikiPlex.Legacy.HtmlTextWriterAttribute.Value, value);
            writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Param);
            writer.RenderEndTag();
        }
    }
}
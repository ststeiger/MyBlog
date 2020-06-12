
namespace WikiPlex.Formatting.Renderers
{
    internal interface ISilverlightRenderer
    {
        void AddObjectTagAttributes(WikiPlex.Legacy.HtmlTextWriter writer);
        void AddParameterTags(string url, bool gpuAcceleration, string[] initParams, WikiPlex.Legacy.HtmlTextWriter writer);
        void AddDownloadLink(WikiPlex.Legacy.HtmlTextWriter writer);
    }
}
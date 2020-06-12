
namespace WikiPlex.Formatting.Renderers
{
    internal interface IVideoRenderer
    {
        WikiPlex.Common.Dimensions Dimensions { get; set; }

        void Render(string url, WikiPlex.Legacy.HtmlTextWriter writer);
    }
}
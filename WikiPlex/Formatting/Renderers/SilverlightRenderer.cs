
namespace WikiPlex.Formatting.Renderers
{
    /// <summary>
    /// Will render the silverlight scopes.
    /// </summary>
    public class SilverlightRenderer 
        : Renderer
    {
        /// <summary>
        /// Gets the collection of scope names for this <see cref="IRenderer"/>.
        /// </summary>
        protected override System.Collections.Generic.ICollection<string> ScopeNames
        {
            get { return new[] { ScopeName.Silverlight }; }
        }

        /// <summary>
        /// Gets the invalid argument error text.
        /// </summary>
        protected override string InvalidArgumentError
        {
            get { return "Cannot resolve silverlight macro, invalid parameter '{0}'."; }
        }

        /// <summary>
        /// Will expand the input into the appropriate content based on scope.
        /// </summary>
        /// <param name="scopeName">The scope name.</param>
        /// <param name="input">The input to be expanded.</param>
        /// <param name="htmlEncode">Function that will html encode the output.</param>
        /// <param name="attributeEncode">Function that will html attribute encode the output.</param>
        /// <returns>The expanded content.</returns>
        protected override string PerformExpand(string scopeName, string input
            , System.Func<string, string> htmlEncode
            , System.Func<string, string> attributeEncode)
        {
            string[] parameters = input.Split(new[] {','}, System.StringSplitOptions.RemoveEmptyEntries);
            string url = WikiPlex.Common.Parameters.ExtractUrl(parameters);
            WikiPlex.Common.Dimensions dimensions = WikiPlex.Common.Parameters.ExtractDimensions(parameters, 200, 200);
            bool gpuAcceleration = WikiPlex.Common.Parameters.ExtractBool(parameters, "gpuAcceleration", false);

            string versionValue;
            int version = 5;
            if (WikiPlex.Common.Parameters.TryGetValue(parameters, "version", out versionValue) && int.TryParse(versionValue, out version))
            {
                if (version < 2 || version > 5)
                    version = 5;
            }

            if (version == 2 && gpuAcceleration)
                throw new WikiPlex.Common.RenderException("Cannot resolve silverlight macro, 'gpuAcceleration' cannot be enabled with version 2 of Silverlight.");

            string[] initParams = GetInitParams(parameters);
            ISilverlightRenderer renderer = GetRenderer(version);

            System.Text.StringBuilder content = new System.Text.StringBuilder();
            using (var tw = new System.IO.StringWriter(content))
            using (var writer = new WikiPlex.Legacy.HtmlTextWriter(tw, string.Empty))
            {
                writer.NewLine = string.Empty;

                renderer.AddObjectTagAttributes(writer);
                writer.AddStyleAttribute(WikiPlex.Legacy.HtmlTextWriterStyle.Height, dimensions.Height.ToString());
                writer.AddStyleAttribute(WikiPlex.Legacy.HtmlTextWriterStyle.Width, dimensions.Width.ToString());
                writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Object);

                renderer.AddParameterTags(url, gpuAcceleration, initParams, writer);
                renderer.AddDownloadLink(writer);

                writer.RenderEndTag(); // object

                writer.AddStyleAttribute(WikiPlex.Legacy.HtmlTextWriterStyle.Visibility, "hidden");
                writer.AddStyleAttribute(WikiPlex.Legacy.HtmlTextWriterStyle.Height, "0");
                writer.AddStyleAttribute(WikiPlex.Legacy.HtmlTextWriterStyle.Width, "0");
                writer.AddStyleAttribute(WikiPlex.Legacy.HtmlTextWriterStyle.BorderWidth, "0");
                writer.RenderBeginTag(WikiPlex.Legacy.HtmlTextWriterTag.Iframe);
                writer.RenderEndTag();
            }

            return content.ToString();
        }
        
        
        private static string[] GetInitParams(System.Collections.Generic.IEnumerable<string> parameters)
        {
            System.Collections.Generic.List<string> ls = new System.Collections.Generic.List<string>();
            
            foreach (string p in parameters)
            {
                if( !p.StartsWith("url=", System.StringComparison.OrdinalIgnoreCase)
                    && !p.StartsWith("height=", System.StringComparison.OrdinalIgnoreCase)
                    && !p.StartsWith("width=", System.StringComparison.OrdinalIgnoreCase)
                    && !p.StartsWith("version=", System.StringComparison.OrdinalIgnoreCase)
                    && !p.StartsWith("gpuAcceleration=", System.StringComparison.OrdinalIgnoreCase))
                    ls.Add(p);
            }
            
            return ls.ToArray();
        }

        private static ISilverlightRenderer GetRenderer(int version)
        {
            switch (version)
            {
                case 5:
                    return new Silverlight5Renderer();
                case 4:
                    return new Silverlight4Renderer();
                case 3:
                    return new Silverlight3Renderer();
                default:
                    return new Silverlight2Renderer();
            }
        }
    }
}
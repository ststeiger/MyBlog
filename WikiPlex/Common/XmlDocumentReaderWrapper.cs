
namespace WikiPlex.Common
{
    public class XmlDocumentReaderWrapper : IXmlDocumentReader
    {
        public System.Xml.XmlDocument Read(string path)
        {
            try
            {
                var xdoc = new System.Xml.XmlDocument();
                xdoc.Load(path);
                return xdoc;
            }
            catch
            {
                return null;
            }
        }
    }
}
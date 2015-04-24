
namespace MyBlog
{


	public class Server
	{


		public Server()
		{ }


		public static string HtmlEncode(string s)
		{
			return System.Web.HttpUtility.HtmlEncode(s);
		}


		public static string HtmlDecode(string s)
		{
			return System.Web.HttpUtility.HtmlDecode(s);
		}


		public static string HtmlAttributeEncode(string s)
		{
			return System.Web.HttpUtility.HtmlAttributeEncode(s);
		}


		public static string MapPath(string virtualPath)
		{
			return System.Web.Hosting.HostingEnvironment.MapPath(virtualPath);
		}


	} // End Class Server


} // End Namespace MyBlog

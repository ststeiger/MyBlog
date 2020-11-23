
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


// namespace OnlineYournal.Code
namespace Open.Infrastructure.Web.DomainMatcher
{
	/// <summary>
	/// Route supporting domain names
	/// </summary>
	internal sealed class DomainRoute : RouteBase
	{
		private readonly IRouter m_target;

		/// <summary>
		/// Initialize DomainRoute
		/// </summary>
		/// <param name="target"></param>
		/// <param name="template"></param>
		/// <param name="name"></param>
		/// <param name="constraintResolver"></param>
		public DomainRoute(
			IRouter target,
			string template,
			string name,
			IInlineConstraintResolver constraintResolver)
			: this(target, template, name, constraintResolver
				  , new RouteValueDictionary()
				  , new RouteValueDictionary()
				  , new RouteValueDictionary())
		{
		}
		/// <summary>
		/// Initialization DomainRoute
		/// </summary>
		/// <param name="target"></param>
		/// <param name="template"></param>
		/// <param name="name"></param>
		/// <param name="constraintResolver"></param>
		/// <param name="defaults"></param>
		/// <param name="constraints"></param>
		/// <param name="dataTokens"></param>
		public DomainRoute(
			IRouter target,
			string template,
			string name,
			IInlineConstraintResolver constraintResolver,
			RouteValueDictionary defaults,
			IDictionary<string, object> constraints,
			RouteValueDictionary dataTokens)
			: base(template, name, constraintResolver, defaults, constraints, dataTokens)
		{
			m_target = target;
			//{projectcode}.ixiaoben.com.cn/Home/Index
			if (template.IndexOf('.') == -1)
			{
				throw new ArgumentException($"{nameof(template)}Must be in hostname format");
			}
			// The host name must be before the first slash (/)
			int index = template.IndexOf('/');
			this.HostTemplate = TemplateParser.Parse(template.Substring(0, index));
			base.ParsedTemplate = TemplateParser.Parse(template.Substring(index + 1));
		}
		/// <summary>
		/// Hostname template
		/// </summary>
		public RouteTemplate HostTemplate { get; private set; }

		protected override Task OnRouteMatched(RouteContext context)
		{
			context.RouteData.Routers.Add(m_target);
			return m_target.RouteAsync(context);
		}

		protected override VirtualPathData OnVirtualPathGenerated(VirtualPathContext context)
		{
			return m_target.GetVirtualPath(context);
		}

		public override Task RouteAsync(RouteContext context)
		{
			// Match host
			var matcher = new HostMatcher(this.HostTemplate);
			var host = context.HttpContext.Request.Host;
			if (!matcher.TryMatch(host, context.RouteData.Values))
			{
				return Task.CompletedTask;
			}
			// Match path
			return base.RouteAsync(context);
		}
	}
}

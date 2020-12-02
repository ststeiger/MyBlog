
using Microsoft.Extensions.DependencyInjection; // for GetRequiredService


    // https://github.com/xeonye/Open.Infrastructure/blob/master/Open.Infrastructure.Solution/src/Open.Infrastructure.Web/DomainMatcher/MapDomainRouteRouteBuilderExtensions.cs
namespace Open.Infrastructure.Web.DomainMatcher
{
    /// <summary>
    /// DomainRoute Extension
    /// </summary>
    public static class MapDomainRouteRouteBuilderExtensions
    {
        /// <summary>
        /// Add DomainRoute
        /// </summary>
        /// <param name="routeBuilder"></param>
        /// <param name="name"></param>
        /// <param name="template"></param>
        public static void MapDomainRoute(this Microsoft.AspNetCore.Routing.IRouteBuilder routeBuilder, string name, string template)
        {
            if (routeBuilder.DefaultHandler == null)
            {
                throw new System.Exception($"Must be set {nameof(Microsoft.AspNetCore.Routing.IRouteBuilder)} of DefaultHandler");
            }
            
            var inlineConstraintResolver = routeBuilder.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Routing.IInlineConstraintResolver>();
            routeBuilder.Routes.Add(new DomainRoute(routeBuilder.DefaultHandler, template, name, inlineConstraintResolver));
        }
    }
}
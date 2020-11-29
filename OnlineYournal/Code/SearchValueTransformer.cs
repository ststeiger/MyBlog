
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;


namespace OnlineYournal
{


    // https://stackoverflow.com/questions/32582232/imlementing-a-custom-irouter-in-asp-net-5-vnext-mvc-6
    // https://stackoverflow.com/questions/32565768/change-route-collection-of-mvc6-after-startup
    public class SearchValueTransformer
        : DynamicRouteValueTransformer
    {


        // private readonly IProductLocator _productLocator;
        public SearchValueTransformer( /*IProductLocator productLocator*/)
        {
            // this._productLocator = productLocator;
        } // End Constructor 


        // https://weblogs.asp.net/ricardoperes/dynamic-routing-in-asp-net-core-3#:~:text=ASP.NET%20Core%203%20introduced,request%20will%20be%20dispatched%20to.
        public override async System.Threading.Tasks.ValueTask<RouteValueDictionary> TransformAsync(
            HttpContext httpContext, RouteValueDictionary values)
        {
            string productString = values["product"] as string;
            object controller = "Blog";
            object action = "Dataa";
            object id = 123; // await this._productLocator.FindProduct("product", out var controller);

            values["controller"] = controller;
            values["action"] = action;
            values["id"] = id;

            return await System.Threading.Tasks.Task.FromResult(values);
        } // End Task TransformAsync 


    } // End Class SearchValueTransformer 


}

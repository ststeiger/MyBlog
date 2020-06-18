using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.Logging;
using MyBlogCore.Models;

namespace MyBlogCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        
        public async Task<IActionResult> Add([FromServices] INodeServices nodeServices)
        {
            int num1 = 10;
            int num2 = 20;
            int result = await nodeServices.InvokeAsync<int>("AddModule.js", num1, num2);
            // ViewData["ResultFromNode"] = $"Result of {num1} + {num2} is {result}";
            // return View();
            return Content($"Result of {num1} + {num2} is {result}", "text/plain");
        }
        
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

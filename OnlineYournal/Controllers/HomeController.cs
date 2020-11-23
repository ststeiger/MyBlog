using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineYournal.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


// https://stackoverflow.com/questions/53803266/subdomain-routing-in-asp-net-core-3-0-razorpages
// https://github.com/xeonye/Open.Infrastructure/blob/master/Open.Infrastructure.Solution/src/Open.Infrastructure.Web/DomainMatcher/DomainRoute.cs


namespace OnlineYournal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

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

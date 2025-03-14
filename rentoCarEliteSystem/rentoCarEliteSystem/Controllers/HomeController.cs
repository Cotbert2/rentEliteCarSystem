using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using rentoCarEliteSystem.Models;

namespace rentoCarEliteSystem.Controllers
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
        public IActionResult Error(int code)
        {
            if (code == 404)
            {
                return View("Error404");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult newSell()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }


        
    }
}

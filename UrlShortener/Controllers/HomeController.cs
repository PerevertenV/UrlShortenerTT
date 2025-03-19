using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _fileName = "data.txt";

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ActionResult> Index()
        {
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, _fileName);

            if (!System.IO.File.Exists(filePath))
            {
                await System.IO.File.WriteAllTextAsync(filePath, "About UrlShortener");
            }

            string content = await System.IO.File.ReadAllTextAsync(filePath);

            return View((object)content);
        }
        [HttpPost]
        public async Task<ActionResult> Index(string text) 
        {
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, _fileName);

            await System.IO.File.WriteAllTextAsync(filePath, text);

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

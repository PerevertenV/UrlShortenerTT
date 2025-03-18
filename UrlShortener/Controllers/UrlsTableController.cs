using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Controllers
{
    public class UrlsTableController : Controller
    {
        public ActionResult AngularPage()
        {
            return File("~/wwwroot/angular/index.html", "text/html");
        }
    }
}

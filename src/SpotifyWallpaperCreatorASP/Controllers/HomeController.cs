using Microsoft.AspNet.Mvc;
using SpotifyWallpaperCreatorASP.Models;

namespace SpotifyWallpaperCreatorASP.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}

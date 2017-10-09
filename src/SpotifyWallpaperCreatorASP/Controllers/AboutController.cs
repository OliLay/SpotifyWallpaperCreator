using Microsoft.AspNet.Mvc;
using SpotifyWallpaperCreatorASP.Models;

namespace SpotifyWallpaperCreatorASP.Controllers
{
    public class AboutController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}

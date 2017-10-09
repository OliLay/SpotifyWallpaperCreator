using Microsoft.AspNet.Mvc;
using SpotifyWallpaperCreatorASP.Models;
using SpotifyWallpaperCreatorASP.ViewModels;
using System.Drawing;

namespace SpotifyWallpaperCreatorASP.Controllers
{
    public class CreatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Init(string username)
        {
            User spotifyUser = new User(username);
            spotifyUser.GetPlaylists();

            CreatorViewModel creatorViewModel = new CreatorViewModel(spotifyUser, false);

            return View(creatorViewModel);
        }

        [HttpPost]
        public IActionResult Create(string username, string[] playlistsSelected, string imageSize, string tileSize, bool useTileOnce)
        {
            User spotifyUser = new User(username);
            spotifyUser.GetPlaylists();

            ImageCreator imageCreator = new ImageCreator(spotifyUser.GetCombinedTracks(spotifyUser.GetPlaylists(playlistsSelected)), new ImageResolution(imageSize), new ImageResolution(tileSize), username, useTileOnce);
            string imageURL = imageCreator.GetImage();
            imageCreator.Dispose();

            if(imageURL == null)
            {
                return View("Init", new CreatorViewModel(spotifyUser, true));
            }
            else
            {
                return Redirect(Url.Action("Home", "Index") + "/" + imageURL);
            }
        }
    }
}

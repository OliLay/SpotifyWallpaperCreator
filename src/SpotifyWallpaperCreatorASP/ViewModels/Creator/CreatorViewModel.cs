using SpotifyWallpaperCreatorASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyWallpaperCreatorASP.ViewModels
{
    public class CreatorViewModel
    {
        public User User { get; set; }
        public ImageResolutionViewModel ImageResolutionViewModel { get; set; }
        public TileResolutionViewModel TileResolutionViewModel { get; set; }
        public bool CreatingFailed { get; set; }

        public CreatorViewModel(User user, bool creatingFailed)
        {
            User = user;
            ImageResolutionViewModel = new ImageResolutionViewModel();
            TileResolutionViewModel = new TileResolutionViewModel();
            CreatingFailed = creatingFailed;
        }

    }
}

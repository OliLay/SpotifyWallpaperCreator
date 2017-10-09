using SpotifyWallpaperCreatorASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyWallpaperCreatorASP.ViewModels
{
    public class TileResolutionViewModel : ResolutionViewModel
    {
        public TileResolutionViewModel()
        {
            resolutions = new List<ImageResolution>();
            resolutions.Add(new ImageResolution(25, 25));
            resolutions.Add(new ImageResolution(50, 50));
            resolutions.Add(new ImageResolution(100, 100));
            resolutions.Add(new ImageResolution(150, 150));
            resolutions.Add(new ImageResolution(200, 200));

            SelectedResolution = resolutions.First().ToString();
        }
    }
}

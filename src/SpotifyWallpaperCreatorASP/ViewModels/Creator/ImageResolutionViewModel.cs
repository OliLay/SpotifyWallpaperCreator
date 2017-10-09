using SpotifyWallpaperCreatorASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyWallpaperCreatorASP.ViewModels
{
    public class ImageResolutionViewModel : ResolutionViewModel
    {

        public ImageResolutionViewModel()
        {
            resolutions = new List<ImageResolution>();
            resolutions.Add(new ImageResolution(1280, 960));
            resolutions.Add(new ImageResolution(1920, 1080));

            SelectedResolution = resolutions.First().ToString();
        }
    }
}

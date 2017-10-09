using System;
using System.Collections.Generic;
using System.Linq;
using SpotifyWallpaperCreatorASP.Models;
using System.Drawing;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Mvc.Rendering;

namespace SpotifyWallpaperCreatorASP.ViewModels
{
    public abstract class ResolutionViewModel
    {
        public string SelectedResolution { get; set; }
        protected List<ImageResolution> resolutions;

        public IEnumerable<SelectListItem> Resolutions
        {
            get
            {
                var allSizes = resolutions.Select(f => new SelectListItem
                {
                    Value = f.Size.Width.ToString() + "x" + f.Size.Height.ToString(),
                    Text = f.Size.Width.ToString() + "x" + f.Size.Height.ToString()
                });

                return allSizes;
            }
        }
    }
}

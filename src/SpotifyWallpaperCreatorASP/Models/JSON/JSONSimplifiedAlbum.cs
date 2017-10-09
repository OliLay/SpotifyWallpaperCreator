using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyWallpaperCreatorASP.Models.JSON
{
    class JSONSimplifiedAlbum
    {
        public string href;
        public JSONImage[] images;
        public string name;


        public string GetImageURL()
        {
            if(images.Length > 0)
            {
                return images[0].url;
            }
            else
            {
                return String.Empty;
            }
        }
    }
}

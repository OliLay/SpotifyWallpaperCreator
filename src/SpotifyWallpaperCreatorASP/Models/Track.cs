using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyWallpaperCreatorASP.Models
{
    public class Track
    {
        public string Name { get; set; }
        public Album Album { get; set; }

        
        public Track(string name, Album album)
        {
            Name = name;
            Album = album;
        }
    }
}

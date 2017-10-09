using System.Drawing;

namespace SpotifyWallpaperCreatorASP.Models
{
    public class Album
    {

        public string Name { get; set; }
        public string ImageURL { get; set; }


        public Album(string Name, string ImageURL)
        {
            this.Name = Name;
            this.ImageURL = ImageURL;
        }


        public bool Equals(Album a2)
        {
            return Name == a2.Name && ImageURL == ImageURL;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyWallpaperCreatorASP.Models
{
    public class ImageResolution
    {
        public Size Size { get; set; }

        
        public ImageResolution(Size size)
        {
            Size = size;
        }

        public ImageResolution(int width, int height)
        {
            Size = new Size(width, height);
        }

        public ImageResolution(string s)
        {
            String[] splitted = s.Split('x');

            Size = new Size(int.Parse(splitted[0]), int.Parse(splitted[1]));
        }


        public override string ToString()
        {
            return Size.Width + "x" + Size.Height;
        }
    }
}

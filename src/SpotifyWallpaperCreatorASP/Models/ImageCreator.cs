using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SpotifyWallpaperCreatorASP.Models
{
    public class ImageCreator : IDisposable
    {
        private List<Track> tracks;
        private ImageResolution imageSize;
        private ImageResolution tileSize;

        private List<string> imageURLs;
        private List<Image> images;
        private string username;
        private bool useTileOnce;

        private int neededTiles;


        public ImageCreator(List<Track> tracks, ImageResolution imageSize, ImageResolution tileSize, string username, bool useTileOnce)
        {
            this.tracks = tracks;
            this.imageSize = imageSize;
            this.tileSize = tileSize;
            this.username = username;
            this.useTileOnce = useTileOnce;

            imageURLs = new List<string>();
            images = new List<Image>();
        }


        private void GetTrackURLs()
        {
            foreach (Track t in tracks)
            {
                if(!imageURLs.Contains(t.Album.ImageURL) && t.Album.ImageURL != String.Empty)
                {
                    imageURLs.Add(t.Album.ImageURL);
                }
            }
        }

        private int GetIntNeededTiles()
        {
            return (imageSize.Size.Width / tileSize.Size.Width) * (imageSize.Size.Height / tileSize.Size.Height) + imageSize.Size.Width / tileSize.Size.Width + imageSize.Size.Height / tileSize.Size.Height;
        }

        private bool CheckIfPossible()
        {
            return neededTiles <= imageURLs.Count;
        }

        private List<T> RandomizeList<T>(List<T> l)
        {
            return l.OrderBy(o => Guid.NewGuid().ToString()).ToList();
        }

        private void GetImages()
        {
            foreach(string url in imageURLs)
            {
                images.Add(DownloadImage(url));
            }

            if(!useTileOnce && images.Count < neededTiles)
            {
                DuplicateImages(neededTiles - images.Count);
            }
           
            images = RandomizeList(images);
        }

        private void DuplicateImages(int amount)
        {
            Random random = new Random();

            for(int i = 0; i < amount; i++)
            {
                images.Add(images[random.Next(0, images.Count)]);
            }
        }

        private Image DownloadImage(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(url);

                using (MemoryStream mem = new MemoryStream(data))
                {
                    return Image.FromStream(mem);
                }
            }
        }

        private Image CombineImages()
        {
           Image image = new Bitmap(imageSize.Size.Width, imageSize.Size.Height);

           Graphics g = Graphics.FromImage(image);

           int i = 0;
           for (int x = 0; x < image.Size.Width / tileSize.Size.Width + 1; x++)
           {
               for(int y = 0; y < image.Size.Height / tileSize.Size.Height + 1; y++)
               {
                    g.DrawImage(images[i], new Rectangle(x * tileSize.Size.Width, y * tileSize.Size.Height, tileSize.Size.Width, tileSize.Size.Height));
                    i += 1;
                }
            }

                g.Save();

                return image;     
        }

        public string GetImage()
        {
            GetTrackURLs();
            neededTiles = GetIntNeededTiles();

            if (!useTileOnce || CheckIfPossible())
            {
                GetImages();

                Image image = CombineImages();

                if (image != null)
                {
                    string filename = DateTime.Now.ToString("yyyyMMdd_hh_mm_ss") + username + ".png";
                    string path = "/var/www/html/spotify/wwwroot/" + filename;
                    image.Save(filename);

                    return filename;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        public void Dispose()
        {
            foreach(Image i in images)
            {
                i.Dispose();
            }

            tracks.Clear();
            imageURLs.Clear();
            images.Clear();
        }
    }
}

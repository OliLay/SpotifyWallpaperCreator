using System;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyWallpaperCreatorASP.Models
{
    public class Playlist
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public List<Track> Tracks { get; set; }
        public List<Album> Albums { get; set; }

        private SpotifyGetter spotifyHelper;

        public Playlist(string name, string id, SpotifyGetter spotifyHelper)
        {
            Name = name;
            ID = id;
            this.spotifyHelper = spotifyHelper;

            Albums = new List<Album>();
        }


        private void GetAlbums()
        {
            Albums.Clear();
            foreach (Track t in Tracks)
            {
                if(Albums.Count() == 0)
                {
                    Albums.Add(t.Album);
                }
                else
                {
                    bool notInList = true;
                    foreach(Album a in Albums)
                    {
                        if(a.Equals(t.Album))
                        {
                            notInList = false; 
                            break;
                        }
                    }

                    if(notInList) //if the album is not in the List yet, add it
                    {
                        Albums.Add(t.Album);
                    }
                }
                
            }
        }


        public void GetTracks()
        {
            Tracks = spotifyHelper.GetTracks(ID);

            GetAlbums();
        }

    }
}

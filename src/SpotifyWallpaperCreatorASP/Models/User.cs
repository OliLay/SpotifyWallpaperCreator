using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyWallpaperCreatorASP.Models
{
    public class User
    {
        public string Name { get; set; }
        public List<Playlist> Playlists { get; set; }

        private SpotifyGetter spotifyHelper;

        public User(string name)
        {
            Name = name;
            Playlists = new List<Playlist>();
            spotifyHelper = new SpotifyGetter(this);

            GetPlaylists();
        }

        
        public void GetPlaylists()
        {
            Playlists = spotifyHelper.GetPlaylists();
        }


        public Playlist GetPlaylist(string playlistID)
        {
            foreach (Playlist p in Playlists)
            {
                if (p.ID == playlistID)
                {
                    return p;
                }
            }

            return null;
        }

        public List<Playlist> GetPlaylists(string[] playlistID)
        {
            List<Playlist> playlists = new List<Playlist>();

            for (int i = 0; i < playlistID.Length; i++)
            {
                playlists.Add(GetPlaylist(playlistID[i]));
            }

            return playlists;
        }


        public List<Track> GetCombinedTracks(List<Playlist> playlists)
        {
            List<Track> tracks = new List<Track>();

            foreach(Playlist p in playlists)
            {
                p.GetTracks();
                tracks.AddRange(p.Tracks);
            }

            return tracks;
        }

    }
}

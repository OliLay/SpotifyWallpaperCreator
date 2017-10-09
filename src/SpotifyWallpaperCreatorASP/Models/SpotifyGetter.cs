using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpotifyWallpaperCreatorASP.Models.JSON;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace SpotifyWallpaperCreatorASP.Models
{
    public class SpotifyGetter
    {
        private SpotifyToken spotifyToken;
        private User user;

        public SpotifyGetter(User user)
        {
            this.user = user;
            this.spotifyToken = GetSpotifyToken();
        }


        public List<Playlist> GetPlaylists()
        {
            JSONPlaylists jPlaylists = GetPlaylists(spotifyToken.AccessToken, user.Name);
            List<Playlist> playlists = new List<Playlist>();

            foreach(JSONPlaylist jp in jPlaylists.items)
            {
                playlists.Add(new Playlist(jp.name, jp.id, this));
            }

            return playlists;
        }

        public List<Track> GetTracks(string playlistID)
        {
            List<Track> tracks = new List<Track>();
            List<JSONPlaylistInfo> playlistInfos = new List<JSONPlaylistInfo>();
            playlistInfos.Add(GetPlaylistInfo(spotifyToken.AccessToken, user.Name, playlistID, 0)); //gets the first 100 tracks

            int totalTracks = int.Parse(playlistInfos.First().total);

            for(int i = 1; i < (int)Math.Ceiling((double)totalTracks / 100); i++)
            {
                playlistInfos.Add(GetPlaylistInfo(spotifyToken.AccessToken, user.Name, playlistID, i * 100));
            }

            foreach(JSONPlaylistInfo jPlaylistInfo in playlistInfos)
            {
                foreach(JSONPlaylistTrack jPlaylistTrack in jPlaylistInfo.items)
                {
                    tracks.Add(new Track(jPlaylistTrack.track.name, new Album(jPlaylistTrack.track.album.name, jPlaylistTrack.track.album.GetImageURL())));
                } 
            }

            return tracks;
        }

        private SpotifyToken GetSpotifyToken()
        {
            byte[] byteArray = Encoding.UTF8.GetBytes("grant_type=client_credentials");

            WebRequest webRequest = WebRequest.Create("https://accounts.spotify.com/api/token");
            webRequest.Method = "POST";
            webRequest.Headers.Add("Authorization", "Basic ZTQ5MTZhNmY2YzlmNDE5OWEzOTk0YWI1ZGE2OWM0ZGQ6Mjg5OTE5YWI1ODBjNGY2YzkxNmI0MTU2ZjYyYWU1MjU=");
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = byteArray.Length;

            using (Stream dataStream = webRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse response = webRequest.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            JObject jObject = JObject.Parse(reader.ReadToEnd());

                            return new SpotifyToken(jObject.GetValue("access_token").ToString(), jObject.GetValue("token_type").ToString(), int.Parse(jObject.GetValue("expires_in").ToString()));
                        }
                    }
                }
            }
        }

        private T GetSpotifyType<T>(string token, string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "GET";
                request.Headers.Add("Authorization", "Bearer " + token);
                request.ContentType = "application/json; charset=utf-8";

                T type = default(T);

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            string responseFromServer = reader.ReadToEnd();
                            type = JsonConvert.DeserializeObject<T>(responseFromServer);
                        }
                    }
                }
                return type;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());

                return default(T);
            }
        }


        private JSONPlaylists GetPlaylists(string token, string user)
        {
            string url = string.Format("https://api.spotify.com/v1/users/{0}/playlists", user);
            JSONPlaylists playlists = GetSpotifyType<JSONPlaylists>(token, url);

            return playlists;
        }

        private JSONPlaylistInfo GetPlaylistInfo(string token, string user, string playlistID, int startAt)
        {
            string url = string.Format("https://api.spotify.com/v1/users/{0}/playlists/{1}/tracks?offset={2}", user, playlistID, startAt);
            JSONPlaylistInfo playlistsPaging = GetSpotifyType<JSONPlaylistInfo>(token, url);

            return playlistsPaging;
        }
    }
}

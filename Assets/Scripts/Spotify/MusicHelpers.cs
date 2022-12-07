using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace MusicHelpers
{
    // ---- TRACK ----
    public class Track
    {
        public string spotifyID;
        public string title;
        public string artist;
        public float duration;

        public Track(JObject jsonData)
        {
            spotifyID = (string)jsonData["id"];
            title = (string)jsonData["name"];
            artist = (string)jsonData["artists"][0]["name"];
            duration = (float)jsonData["duration_ms"] / 1000f;
        }

        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this, true);
        }
    }

    // ---- ALBUM ----
    public class Album
    {
        public string spotifyID;
        public string title;
        public string artist;
        public List<Track> tracks;
        public string coverBigResURL;
        public string coverSmallResURL;

        public Album(JObject jsonData)
        {
            spotifyID = (string)jsonData["id"];
            spotifyID = (string)jsonData["id"];
            title = (string)jsonData["name"];
            artist = (string)jsonData["artists"][0]["name"];
            coverBigResURL = (string)jsonData["images"][0]["url"];
            coverSmallResURL = (string)jsonData["images"][2]["url"];

            tracks = new List<Track>();
            foreach (JObject trackData in jsonData["tracks"]["items"])
                tracks.Add(new Track(trackData));
        }

        public int TrackCount { get { return tracks.Count; } }
        public float Duration
        {
            get
            {
                float totalDuration = 0;
                foreach (var track in tracks)
                    totalDuration += track.duration;

                return totalDuration;
            }
        }

        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this, true);
        }
    }
}

using System.Collections.Generic;

namespace MusicHelpers
{
    // ---- TRACK ----
    public class Track
    {
        public string spotifyID;
        public string title;
        public string artist;
        public int duration;

        public Track(dynamic jsonData)
        {
            spotifyID = jsonData.id;
            title = jsonData.name;
            artist = jsonData.artists[0].name;
            duration = jsonData.duration_ms;
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

        public Album(dynamic jsonData)
        {
            spotifyID = jsonData.id;
            title = jsonData.name;
            artist = jsonData.artists[0].name;
            coverBigResURL = jsonData.images[0].url;
            coverSmallResURL = jsonData.images[2].url;

            tracks = new List<Track>();
            foreach (var trackData in jsonData.tracks.items)
                tracks.Add(new Track(trackData));
        }

        public int TrackCount { get { return tracks.Count; } }
        public int Duration
        {
            get
            {
                int totalDuration = 0;
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

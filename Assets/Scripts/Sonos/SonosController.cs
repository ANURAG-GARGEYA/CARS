using UnityEngine;
using System;
using TMPro;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Collections;
using Microsoft.MixedReality.Toolkit.UI;
using MusicHelpers;
using UnityEngine.UI;


public class SonosController : MonoBehaviour
{
    public HARequests HUB;
    public SpotifyAPI spotify;
    public string id = "media_player.den";
    public TextMeshProUGUI trackTitle;
    public TextMeshProUGUI artist;
    public Slider trackProgressSlider;
    public TextMeshProUGUI trackProgressFormatted;
    public TextMeshProUGUI trackDurationFormatted;

    private bool _isPlaying;
    private float _currentVolume;
    private int _currentTrackIndex;
    private float _currentTrackProgress = 0.0f;
    private Track? _currentTrack;
    private Album? _currentAlbum;


    void Start()
    {
        // StartCoroutine(UpdateValues(delay: 0.0f));
    }

    void Update()
    {

        UpdateUI();
    }

    public IEnumerator UpdateValues(float delay = 2.5f, bool updateVolume = true)
    {
        yield return new WaitForSeconds(delay);

        HUB.Get(id).Then(res =>
        {
            string jsonStr = Encoding.UTF8.GetString(res.Data);
            JObject data = JObject.Parse(jsonStr);
            // _isPlaying = (string)data["state"] != "paused"; // it's delayed anyway, stupid instant response from HA

            var attributes = data["attributes"];
            if (_currentTrack != null)
            {
                _currentTrack.title = (string)attributes["media_title"];
                _currentTrack.artist = (string)attributes["media_artist"];
                _currentTrack.duration = (float)attributes["media_duration"];
            }

            if (updateVolume)
                _currentVolume = (float)attributes["volume_level"];

        }).Catch(err =>
            {
                Debug.Log("UPDATE VALUES");
                Debug.LogError(err.Message);
            });
    }

    void UpdateUI()
    {
        if (_currentTrack != null)
        {
            trackTitle.text = _currentTrack.title;
            artist.text = _currentTrack?.artist;

            if (_isPlaying)
            {
                _currentTrackProgress += Time.deltaTime;
                trackProgressSlider.value = _currentTrackProgress / (float)_currentTrack.duration;
            }

            TimeSpan progressTime = TimeSpan.FromSeconds(_currentTrackProgress);
            trackProgressFormatted.text = progressTime.ToString(@"mm\:ss");
            TimeSpan durationTime = TimeSpan.FromSeconds(_currentTrack.duration);
            trackDurationFormatted.text = durationTime.ToString(@"mm\:ss");
        }
    }

    public void PlayPause()
    {
        JObject data = new JObject();
        data["entity_id"] = id;

        string stringifiedData = data.ToString();
        HUB.Post("media_player", "media_play_pause", stringifiedData).Then(res =>
        {
            _isPlaying = !_isPlaying;
        });
    }

    public void NextTrack()
    {
        JObject data = new JObject();
        data["entity_id"] = id;

        string stringifiedData = data.ToString();
        HUB.Post("media_player", "media_next_track", stringifiedData).Then(res =>
        {
            _currentTrackIndex = Math.Min(_currentAlbum.TrackCount - 1, _currentTrackIndex + 1);
            _currentTrack = _currentAlbum.tracks[_currentTrackIndex];
            _currentTrackProgress = 0;
        }).Catch(err =>
        {
            Debug.LogError(err.Message);
        });
    }

    public void PrevTrack()
    {
        JObject data = new JObject();
        data["entity_id"] = id;

        string stringifiedData = data.ToString();
        HUB.Post("media_player", "media_previous_track", stringifiedData).Then(res =>
        {
            _currentTrackIndex = Math.Max(0, _currentTrackIndex - 1);
            _currentTrack = _currentAlbum.tracks[_currentTrackIndex];
            _currentTrackProgress = 0;
        }).Catch(err =>
        {
            Debug.LogError(err.Message);
        });
    }

    public void JumpToTrack(int trackIndex)
    {
        JObject data = new JObject();
        data["entity_id"] = id;
        data["queue_position"] = trackIndex;

        string stringifiedData = data.ToString();
        HUB.Post("sonos", "play_queue", stringifiedData).Then(res =>
        {
            _currentTrack = _currentAlbum.tracks[trackIndex];
            _currentTrackIndex = trackIndex;
        }).Catch(err =>
        {
            Debug.LogError(err.Message);
        });
    }

    public void OnVolumeSliderUpdated(SliderEventData eventData)
    {

        float newVolume = eventData.NewValue / 3.0f; // Division to limit loudness for development
        if (newVolume == _currentVolume)
            return;

        _currentVolume = newVolume;

        JObject data = new JObject();
        data["entity_id"] = id;
        data["volume_level"] = newVolume;

        string stringifiedData = data.ToString();
        HUB.Post("media_player", "volume_set", stringifiedData).Then(res =>
        {
            StartCoroutine(UpdateValues(updateVolume: false));
        }).Catch(err =>
        {
            Debug.LogError(err.Message);
        });
    }

    public void SetTracklist(Album album)
    {


    }

    public void AddAlbum(string albumID)
    {
        _currentTrackProgress = 0.0f;
        spotify.GetAlbum(albumID).Then(res =>
        {
            string jsonStr = Encoding.UTF8.GetString(res.Data);
            JObject albumData = JObject.Parse(jsonStr);

            _currentAlbum = new Album(albumData);
            _currentTrackIndex = 0;
            _currentTrack = _currentAlbum.tracks[0];

            JObject postData = new JObject();
            postData["entity_id"] = id;
            postData["media_content_type"] = "music";
            postData["media_content_id"] = $"https://open.spotify.com/album/{_currentAlbum.spotifyID}";

            string stringifiedData = postData.ToString();
            HUB.Post("media_player", "play_media", stringifiedData).Then(res =>
            {
                _isPlaying = true; // set it manually as it doesn't update properly...
            });

        }).Catch(err =>
        {
            Debug.LogError(err.Message);
        });
    }

}


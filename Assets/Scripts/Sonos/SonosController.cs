using UnityEngine;
using System;
using TMPro;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using MusicHelpers;


// TODO CHANGE DICT TO JOBJECT

public class SonosController : MonoBehaviour
{
    public HARequests HUB;
    public SpotifyAPI spotify;
    public string id = "media_player.den";
    public TextMeshPro trackTitle;
    public TextMeshPro artist;

    private bool _isPlaying;
    private float _currentVolume;
    private int _currentTrackIndex;
    private Track? _currentTrack;
    private Album? _currentAlbum;

    public Track SetTrack { set; get; }
    public Album SetAlbum { set; get; }

    void Start()
    {
        StartCoroutine(UpdateValues(delay: 0.0f));
    }

    void Update()
    {
        UpdateUI();
    }

    public IEnumerator UpdateValues(float delay = 2.5f, bool updateVolume = true)
    {
        yield return new WaitForSeconds(delay);

        var data = new Dictionary<string, dynamic>();
        HUB.Get(id, data).Then(res =>
        {
            // Debug.Log("GET");
            string jsonStr = Encoding.UTF8.GetString(res.Data);

            data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(jsonStr);
            _isPlaying = data["state"] != "paused";

            var attributes = data["attributes"].ToObject<Dictionary<string, dynamic>>();
            _currentTrack.title = attributes["media_title"];
            _currentTrack.artist = attributes["media_artist"];
            _currentTrack.duration = (int)attributes["media_duration"];

            if (updateVolume)
                _currentVolume = (float)attributes["volume_level"];

        }).Catch(err =>
            {
                Debug.Log(err.Message);
            });
    }

    void UpdateUI()
    {
        trackTitle.text = _currentTrack?.title ?? "";
        artist.text = _currentTrack?.artist ?? "";
    }

    public void PlayPause()
    {
        var data = new Dictionary<string, dynamic>();
        data["entity_id"] = id;

        HUB.Post("media_player", "media_play_pause", data);
    }

    public void NextTrack()
    {
        var data = new Dictionary<string, dynamic>();
        data["entity_id"] = id;

        HUB.Post("media_player", "media_next_track", data).Then(res =>
        {
            StartCoroutine(UpdateValues());
        }).Catch(err =>
        {
            Debug.Log(err.Message);
        });
    }

    public void PrevTrack()
    {
        var data = new Dictionary<string, dynamic>();
        data["entity_id"] = id;

        HUB.Post("media_player", "media_previous_track", data).Then(res =>
        {
            StartCoroutine(UpdateValues());
        }).Catch(err =>
        {
            Debug.Log(err.Message);
        });
    }

    public void OnVolumeSliderUpdated(SliderEventData eventData)
    {

        float newVolume = eventData.NewValue / 3.0f; // Division to limit loudness for development
        if (newVolume == _currentVolume)
            return;

        _currentVolume = newVolume;

        var data = new Dictionary<string, dynamic>();
        data["entity_id"] = id;
        data["volume_level"] = newVolume;

        HUB.Post("media_player", "volume_set", data).Then(res =>
        {
            StartCoroutine(UpdateValues(updateVolume: false));
        }).Catch(err =>
        {
            Debug.Log(err.Message);
        });
    }

    public void AddAlbum()
    {

        spotify.GetAlbum("4aawyAB9vmqN3uQ7FjRGTy").Then(res =>
        {
            string jsonStr = Encoding.UTF8.GetString(res.Data);
            dynamic albumData = JValue.Parse(jsonStr);

            _currentAlbum = new Album(albumData);
            _currentTrackIndex = 0;
            _currentTrack = _currentAlbum.tracks[0];

            dynamic postData = new JObject();
            postData.entity_id = id;
            postData.media_content_type = "music";
            postData.media_content_id = $"https://open.spotify.com/album/{_currentAlbum.spotifyID}";
            // HUB.Post("media_player", "play_media", postData);

        }).Catch(err =>
        {
            Debug.LogError(err.Message);
        });
    }

}


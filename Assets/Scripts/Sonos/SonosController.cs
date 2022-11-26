using UnityEngine;
using System;
using TMPro;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;

struct Track
{
    public string title;
    public string artist;
    public int duration;
};

public class SonosController : MonoBehaviour
{
    public HARequests HUB;
    public string id = "media_player.den";
    public TextMeshPro trackTitle;
    public TextMeshPro artist;

    private bool _isPlaying;
    private float _currentVolume;
    private Track _currentTrack;

    void Start()
    {
        StartCoroutine(UpdateValues(delay: 0.0f));
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

            UpdateUI();
        }).Catch(err =>
            {
                Debug.Log(err.Message);
            });
    }

    void UpdateUI()
    {
        trackTitle.text = _currentTrack.title;
        artist.text = _currentTrack.artist;
    }

    public void PlayPause()
    {
        var data = new Dictionary<string, dynamic>();
        data["entity_id"] = id;

        HUB.Post("media_player", "media_play_pause", data);
    }

    public void AddTrack()
    {
        var data = new Dictionary<string, dynamic>();
        data["entity_id"] = id;
        data["media_content_type"] = "music";
        data["media_content_id"] = "https://open.spotify.com/album/3IaQ0DQMIXMShbMDNepeTK?si=FT-FwhitTT-MshMvf099wA";

        HUB.Post("media_player", "play_media", data);
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
}


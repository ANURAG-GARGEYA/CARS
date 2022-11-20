using UnityEngine;
using System;
using TMPro;
using Newtonsoft.Json;
using Microsoft.MixedReality.Toolkit.UI;
using System.Text;
using System.Collections.Generic;

// Just playing around with sonos api
public class SonosAPI : MonoBehaviour
{
    public HARequests HUB;
    public string id = "media_player.den";
    public TextMeshPro trackTitle;
    public TextMeshPro artist;

    private bool _isPlaying = false;
    private float _currentVolume = 0.1f;

    void Start()
    {
        var data = new Dictionary<string, dynamic>();
        HUB.Get(id, data).Then(res =>
        {
            Debug.Log("Status" + res.ToString() + "Ok");
            string jsonStr = Encoding.UTF8.GetString(res.Data);
            data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(jsonStr);
            // Debug.Log(JsonConvert.SerializeObject(data));
        }).Catch(err =>
        {
            Debug.Log(err.Message);
        });
    }

    // TODO how to monitor states without big overhead 
    // calling this every few seconds seems not perfect
    public void UpdateValues()
    {
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

        HUB.Post("media_player", "media_next_track", data);
    }

    public void PrevTrack()
    {
        AddTrack();
        var data = new Dictionary<string, dynamic>();
        data["entity_id"] = id;

        HUB.Post("media_player", "media_previous_track", data);
    }

    public void OnVolumeSliderUpdate(SliderEventData eventData)
    {
        float newVolume = (float)Math.Min(eventData.NewValue, 0.15); // just for testing to avoid blasting loud music in the lab
        if (newVolume != _currentVolume)
        {
            // _serviceClient.CallService("media_player", "volume_set", new { entity_id = "media_player.den", volume_level = newVolume });
            _currentVolume = newVolume;
        }
    }

    void OnDestroy()
    {
    }
}


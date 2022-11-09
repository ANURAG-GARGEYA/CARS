using UnityEngine;
using System;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using HADotNet.Core;
using HADotNet.Core.Clients;

// Just playing around with sonos api
public class SonosAPI : MonoBehaviour
{
    public string ip;
    public TextMeshPro trackTitle;

    private ServiceClient _serviceClient;
    private bool _isPlaying = false;

    void Start()
    {
        _serviceClient = ClientFactory.GetClient<ServiceClient>();
    }

    // TODO how to monitor states without big overhead 
    // calling this every few seconds seems not perfect
    public void UpdateValues()
    {
    }

    public async void PlayPause()
    {
        await _serviceClient.CallService("media_player", "media_play_pause", new { entity_id = "media_player.den" });
    }

    public async void AddTrack()
    {
        await _serviceClient.CallService("media_player", "play_media", new { entity_id = "media_player.den", media_content_type = "music", media_content_id = "https://open.spotify.com/track/3nVr75rx4FFrXGkuwU5CCg?si=1abf34356f514ff9" });
    }

    public async void NextTrack()
    {
        await _serviceClient.CallService("media_player", "media_next_track", new { entity_id = "media_player.den" });
    }

    public async void PrevTrack()
    {
        await _serviceClient.CallService("media_player", "media_previous_track", new { entity_id = "media_player.den" });
    }

    public async void OnVolumeSliderUpdate(SliderEventData eventData)
    {
        double newVolume = Math.Min(eventData.NewValue, 0.15); // just for testing to avoid blasting loud music in the lab
        await _serviceClient.CallService("media_player", "volume_set", new { entity_id = "media_player.den", volume_level = newVolume });
    }

    void OnDestroy()
    {
    }
}


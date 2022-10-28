using UnityEngine;
using ByteDev.Sonos;
using ByteDev.Sonos.Models;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;

// Just playing around with sonos api
public class SonosAPI : MonoBehaviour
{
    public string ip;
    public TextMeshPro songTitleObj;

    private SonosController _controller;
    private SonosVolume _volume;
    private bool _isPlaying;

    void Start()
    {
        _controller = new SonosControllerFactory().Create(ip);
        songTitleObj.text = "Some cool song"; // TODO figure out how to get track data from sonos
        InvokeRepeating("UpdateValues", 0.0f, 5.0f);
    }

    // TODO how to monitor states without big overhead 
    // calling this every few seconds seems not perfect
    public async void UpdateValues()
    {
        _volume = await _controller.GetVolumeAsync();
        _isPlaying = await _controller.GetIsPlayingAsync();
    }

    public async void PlayPause()
    {
        _isPlaying = await _controller.GetIsPlayingAsync();
        if (_isPlaying)
            await _controller.PauseAsync();
        else
            await _controller.PlayAsync();
    }

    public async void NextTrack()
    {
        await _controller.NextTrackAsync();
    }

    public async void PrevTrack()
    {
        await _controller.PreviousTrackAsync();
    }

    public async void OnVolumeSliderUpdate(SliderEventData eventData)
    {
        int newVolume = (int)(eventData.NewValue * 50);
        if (newVolume == _volume.Value) return;

        _volume = new SonosVolume(newVolume);
        await _controller.SetVolumeAsync(_volume);
    }

    async void OnDestroy()
    {
        _volume.Equals(5);
        await _controller.SetVolumeAsync(_volume);
    }
}

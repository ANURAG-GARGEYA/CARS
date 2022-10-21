using UnityEngine;
using ByteDev.Sonos;
using ByteDev.Sonos.Models;

// Just playing around with sonos api
public class Volume : MonoBehaviour
{
    public string ip;

    private SonosController controller;
    private SonosVolume volume;

    async void Start()
    {
        Debug.Log("STARTING");
        controller = new SonosControllerFactory().Create(ip);
        volume = await controller.GetVolumeAsync();
        volume.Equals(5);
        await controller.SetVolumeAsync(volume);
        await controller.PlayAsync();
    }

    public async void IncreaseVolume()
    {
        Debug.Log("Changing volume");
        volume = await controller.GetVolumeAsync();
        volume.Increase(10);
        await controller.SetVolumeAsync(volume);
    }

    async void OnDestroy()
    {
        await controller.StopAsync();
        volume.Equals(5);
        await controller.SetVolumeAsync(volume);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;
public class MPUIInfoHandler : MonoBehaviour
{
    public TextMeshProUGUI trackName;
    [SerializeField]
    private Texture PlayTexture, PauseTexture;
    [SerializeField]
    private PinchSlider VolumeSlider;
    public void OnPlay()
    {
        Debug.Log("Play");
        //TODO write track play code here
    }
    public void OnPause()
    {
        Debug.Log("Pause");
        //TODO write track Pause code here
    }
    public void OnNext()
    {
        Debug.Log("Next");
        //TODO write next track code here
    }
    public void OnPrevious()
    {
        Debug.Log("Previous");
        //TODO write previous track  code here
    }

    public void OnSetVolume(float Value)
    {
        if (Value == 0)
        {
            //mute 
            Debug.Log("Mute");
            return;
        }
        Debug.Log("Volume" + VolumeSlider.SliderValue);
        //TODO write control volume code here - use volume slider value- already declared in this script
    }

}

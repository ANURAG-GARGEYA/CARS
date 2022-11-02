using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Microsoft.MixedReality.Toolkit.UI;
public class ButtonAction : MonoBehaviour
{
    
    [SerializeField]
    private HueLamp _hueLamp;

    [SerializeField]
    private GameObject cube;
    [SerializeField]
    private PinchSlider _lampBrightness;

    public void CubeEvent()
    {
        _hueLamp.on = _hueLamp.on ? false : true;
    }
   
    public void updateBrightness()
    {
        _hueLamp.color.a = _lampBrightness.SliderValue;
    }

}

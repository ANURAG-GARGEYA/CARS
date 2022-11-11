using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class CustomColorPicker : MonoBehaviour
{
    [SerializeField]
    private PinchSlider _rSlider, _gSlider, _bSlider;
    private int _rValue, _gValue, _bValue;

    public void UpdateRGBValue()
    {
        _rValue = (int)(_rSlider.SliderValue * 255);
        _gValue = (int)(_gSlider.SliderValue * 255);
        _bValue = (int)(_bSlider.SliderValue * 255);
        PhilipsHueController.instance.UpdateColor(_rValue, _gValue, _bValue);

    }



}

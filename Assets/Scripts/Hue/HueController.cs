using UnityEngine;
using TMPro;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine.UI;


public class HueController : MonoBehaviour
{
    public HARequests HUB;
    public string id = "light.lampa_tunna";

    private bool _isOn = false;
    private Color _currentColor = Color.white;
    private int _currentBrightness = 255;

    void Start()
    {

    }

    void Update()
    {

    }

    public void ToggleLight()
    {
        JObject data = new JObject();
        data["entity_id"] = id;

        string stringifiedData = data.ToString();
        HUB.Post("light", "toggle", stringifiedData).Then(res =>
        {
            _isOn = !_isOn;
        }).Catch(err =>
        {
            Debug.LogError(err.Message);

        });
    }

    public void SetColorRGB()
    {
        int[] newColor = {
            (int)(Color.red.r * 255.0f),
            (int)(Color.red.g * 255.0f),
            (int)(Color.red.b * 255.0f),
        };
        JObject data = new JObject();
        data["entity_id"] = id;
        data["rgb_color"] = new JArray(newColor);

        string stringifiedData = data.ToString();
        Debug.Log(stringifiedData);
        HUB.Post("light", "turn_on", stringifiedData).Then(res =>
        {

        }).Catch(err =>
        {
            Debug.LogError(err.Message);

        });
    }

    public void OnBrightnessSliderUpdated(SliderEventData eventData)
    {
        int newBrightness = (int)(eventData.NewValue * 255.0f);
        if (newBrightness == _currentBrightness)
            return;

        _currentBrightness = newBrightness;

        Debug.Log(newBrightness);
        JObject data = new JObject();
        data["entity_id"] = id;
        data["brightness"] = newBrightness;

        string stringifiedData = data.ToString();
        HUB.Post("light", "turn_on", stringifiedData).Then(res =>
        {
            _currentBrightness = newBrightness;
        }).Catch(err =>
        {
            Debug.LogError(err.Message);

        });

    }
}

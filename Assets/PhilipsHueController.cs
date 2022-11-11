using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HADotNet.Core.Clients;
using HADotNet.Core;
public class PhilipsHueController : MonoBehaviour
{
    public static PhilipsHueController instance;

    private ServiceClient _serviceClient;
    private StatesClient _statesClient;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _serviceClient = ClientFactory.GetClient<ServiceClient>();
        _statesClient = ClientFactory.GetClient<StatesClient>();

    }
    public async void ToggleLight()
    {
        await _serviceClient.CallService("light", "toggle", new { entity_id = "light.lampa_tunna" });
    }
    public async void UpdateColor(int r, int g, int b)
    {
        int[] color = { r, g, b };
        await _serviceClient.CallService("light", "turn_on", new { entity_id = "light.lampa_tunna", rgb_color = color });
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleLight();
        }
    }
}

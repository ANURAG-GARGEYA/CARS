using UnityEngine;
using HADotNet.Core;

public class HAClientInitializer : MonoBehaviour
{
    public string haUrl;
    public string apiKey;

    void Awake()
    {
        ClientFactory.Initialize(haUrl, apiKey);
    }
}

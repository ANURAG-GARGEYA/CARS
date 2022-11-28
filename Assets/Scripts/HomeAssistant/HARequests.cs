using UnityEngine;
using Proyecto26;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class HARequests : MonoBehaviour
{
    public string haUrl;
    public string apiKey;

    public RSG.IPromise<ResponseHelper> Post(string media, string action, string objectData)
    {
        RestClient.DefaultRequestHeaders["Authorization"] = $"Bearer {apiKey}";

        var resPromise = RestClient.Post($"{haUrl}/api/services/{media}/{action}", objectData);
        RestClient.ClearDefaultHeaders();

        return resPromise;
    }

    public RSG.IPromise<ResponseHelper> Get(string mediaId)
    {
        RestClient.DefaultRequestHeaders["Authorization"] = $"Bearer {apiKey}";

        var resPromise = RestClient.Get($"{haUrl}/api/states/{mediaId}");
        RestClient.ClearDefaultHeaders();

        return resPromise;
    }
}


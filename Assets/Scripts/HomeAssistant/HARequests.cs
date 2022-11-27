using UnityEngine;
using Proyecto26;
using Newtonsoft.Json;
using System.Collections.Generic;

public class HARequests : MonoBehaviour
{
    public string haUrl;
    public string apiKey;

    // TODO CHANGE DICT TO JOBJECT
    public RSG.IPromise<ResponseHelper> Post(string media, string action, Dictionary<string, dynamic> objectData)
    {
        RestClient.DefaultRequestHeaders["Authorization"] = $"Bearer {apiKey}";

        var resPromise = RestClient.Post($"{haUrl}/api/services/{media}/{action}", JsonConvert.SerializeObject(objectData));
        RestClient.ClearDefaultHeaders();

        return resPromise;
    }

    public RSG.IPromise<ResponseHelper> Get(string mediaId, Dictionary<string, dynamic> dataToSave)
    {
        RestClient.DefaultRequestHeaders["Authorization"] = $"Bearer {apiKey}";

        var resPromise = RestClient.Get($"{haUrl}/api/states/{mediaId}");
        RestClient.ClearDefaultHeaders();

        return resPromise;
    }
}


using UnityEngine;
using Proyecto26;
using Newtonsoft.Json;
using System.Collections.Generic;

public class HARequests : MonoBehaviour
{
    public string haUrl;
    public string apiKey;

    void Awake()
    {
        RestClient.DefaultRequestHeaders["Authorization"] = $"Bearer {apiKey}";
    }

    public void Post(string media, string action, Dictionary<string, dynamic> objectData)
    {
        RestClient.Post($"{haUrl}/api/services/{media}/{action}", JsonConvert.SerializeObject(objectData)).Then(res =>
        {
            Debug.Log("Status" + res.ToString() + "Ok");
        }).Catch(err =>
        {
            Debug.Log(err.Message);
        });
    }

    public RSG.IPromise<ResponseHelper> Get(string mediaId, Dictionary<string, dynamic> dataToSave)
    {
        return RestClient.Get($"{haUrl}/api/states/{mediaId}");
    }
}


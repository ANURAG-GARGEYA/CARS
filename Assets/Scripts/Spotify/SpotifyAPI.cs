using UnityEngine;
using Proyecto26;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;



public class SpotifyAPI : MonoBehaviour
{
    public string clientID;
    public string clientSecret;

    private string _authToken;
    private string _baseUrl = "https://api.spotify.com/v1";

    void Start()
    {
        ObtainToken();
    }

    void ObtainToken()
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes($"{clientID}:{clientSecret}");
        string encodedCredentials = System.Convert.ToBase64String(plainTextBytes);

        RestClient.DefaultRequestHeaders["Authorization"] = $"Basic {encodedCredentials}";

        RequestHelper rh = new RequestHelper
        {
            Uri = "https://accounts.spotify.com/api/token",
            ContentType = "application/x-www-form-urlencoded",
            SimpleForm = new Dictionary<string, string>{
                {"grant_type", "client_credentials"}
            },

        };

        RestClient.Post(rh).Then(res =>
            {
                string jsonStr = Encoding.UTF8.GetString(res.Data);
                JObject data = JObject.Parse(jsonStr);
                _authToken = (string)data["access_token"];
            }).Catch(err =>
            {
                _authToken = "";
                Debug.LogError("FAILED TO OBTAIN TOKEN");
                Debug.LogError(err.Message);
            });

        RestClient.ClearDefaultHeaders();
    }

    public RSG.IPromise<ResponseHelper> GetAlbum(string albumID)
    {
        if (_authToken == "")
            ObtainToken();

        RestClient.DefaultRequestHeaders["Authorization"] = $"Bearer {_authToken}";
        var resPromise = RestClient.Get($"{_baseUrl}/albums/{albumID}");
        RestClient.ClearDefaultHeaders();

        return resPromise;
    }
}

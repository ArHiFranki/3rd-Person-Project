using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class NetwokrService
{
    private const string _webImage = 
    "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c5/Moraine_Lake_17092005.jpg/1280px-Moraine_Lake_17092005.jpg";

    private IEnumerator CallAPI(string url, Action<string> callback )
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Network problem: " + request.error);
            }
            else if (request.responseCode != (long)System.Net.HttpStatusCode.OK)
            {
                Debug.LogError("Response error: " + request.responseCode);
            }
            else
            {
                callback(request.downloadHandler.text);
            }
        }
    }

    public IEnumerator GetWeatherXML(Action<string> callback)
    {
        return CallAPI(APIKey.XmlAPIKey, callback);
    }

    public IEnumerator GetWeatherJSON(Action<string> callback)
    {
        return CallAPI(APIKey.JsonAPIKey, callback);
    }

    public IEnumerator DownloadImage(Action<Texture2D> callback)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(_webImage);
        yield return request.SendWebRequest();
        callback(DownloadHandlerTexture.GetContent(request));
    }
}

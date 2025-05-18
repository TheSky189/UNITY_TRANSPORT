using TMPro;  
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NodeJS_WebClient : MonoBehaviour
{
    public TextMeshProUGUI webContentText; 

    void Start()
    {
        StartCoroutine(GetWebContent());
    }

    IEnumerator GetWebContent()
    {
        UnityWebRequest request = UnityWebRequest.Get("http://localhost:3000");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            webContentText.text = request.downloadHandler.text;
        }
        else
        {
            webContentText.text = "Error: " + request.error;
        }
    }
}

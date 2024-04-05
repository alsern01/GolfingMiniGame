using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class FileWebRequester : MonoBehaviour
{
    void Start()
    {
    }


    public void GetFileFromServer(string filename)
    {
        StartCoroutine(GetText(filename));
    }

    public void UploadFileToServer(string filename)
    {
        StartCoroutine(UploadFile(filename));
    }

    private IEnumerator GetText(string filename)
    {
        //UnityWebRequest request = new UnityWebRequest($"url del dominio/{filename}");
        UnityWebRequest request = new UnityWebRequest("https://gist.githubusercontent.com/alsern01/f735695759c64f52766f986e975718c5/raw/d97a1c9d8ec534735ffe568741d5d9c7081af037/golf_config.json");
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            // Show results as text
            Debug.Log(request.downloadHandler.text);

            string savePath = $"{Application.persistentDataPath}/{filename}";
            File.WriteAllText(savePath, request.downloadHandler.text);
        }
    }

    private IEnumerator UploadFile(string filename)
    {
        string filePath = $"{Application.persistentDataPath}/{filename}";

        if (File.Exists(filePath))
        {
            byte[] fileData = File.ReadAllBytes(filePath);

            UnityWebRequest request = UnityWebRequest.Put($"http://www.my-server.com/{filename}", fileData);
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/octet-stream");

            // Send the request
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to upload file: " + request.error);
            }
            else
            {
                Debug.Log("File upload successful!");
            }
        }

        yield return null;
    }
}

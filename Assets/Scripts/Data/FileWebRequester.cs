using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FileWebRequester : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image panel;

    void Start()
    {
        if (text != null)
            text.gameObject.SetActive(false);

        if (panel != null)
            panel.gameObject.SetActive(false);

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

            if (text != null)
            {
                text.gameObject.SetActive(true);
                panel.gameObject.SetActive(true);
                text.text = "No existe el nombre de usuario";
            }
        }
        else
        {
            // Show results as text
            Debug.Log(request.downloadHandler.text);
            Debug.Log($"Nombre del archivo: {filename}");

            if (filename != "")
            {
                string savePath = $"{Application.persistentDataPath}/{filename}";
                File.WriteAllText(savePath, request.downloadHandler.text);
            }
            if (text != null)
            {
                text.color = Color.green;
                text.text = "Configuracion aplicada";
            }
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

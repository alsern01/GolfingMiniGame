using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField userField;
    [SerializeField] private FileWebRequester webRequester;
    [SerializeField] private TextMeshProUGUI text;

    public void EscenaJuego()
    {
        webRequester.GetFileFromServer(userField.text);

        if (text.text != "No existe el nombre de usuario")
            SceneManager.LoadScene("GameScene");
        else
            Debug.Log("No se inicia el juego");
    }

    public void OptionsBoton()
    {
        //SceneManager.LoadScene("OptionsScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

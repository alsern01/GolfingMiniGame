using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Image panel;



    public void LoadUser()
    {
        if (RealmController.Instance.UserLogin())
        {
            GameManager.Instance.PlayerId = RealmController.Instance.PlayerId;
            GameManager.Instance.LoadConfig();

            infoText.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            infoText.color = Color.green;
            infoText.text = "Configuracion aplicada";

            Invoke("GameScene", 2f);
        }
        else
        {
            infoText.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            infoText.color = Color.red;
            infoText.text = "No existe el nombre de usuario";
        }
    }

    public void OptionsBoton()
    {
        //SceneManager.LoadScene("OptionsScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void GameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Image panel;


    private void Start()
    {
        SceneManager.sceneLoaded += GameManager.Instance.OnSceneChange;
    }

    public void LoadUser()
    {
        if (RealmController.Instance.UserLogin())
        {
            GameManager.Instance.PlayerId = RealmController.Instance.PlayerId;
            GameManager.Instance.LoadConfig();

            infoText.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            infoText.color = Color.green;
            infoText.text = "Nombre de usuario correcto. Empezando...";

            StartCoroutine(StartGame(2));
        }
        else
        {
            infoText.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            infoText.color = Color.red;
            infoText.text = "Nombre de usuario incorrecto.";
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator StartGame(int delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameScene");
    }
}

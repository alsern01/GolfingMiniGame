using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = GameManager.Instance.GetScore().ToString();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

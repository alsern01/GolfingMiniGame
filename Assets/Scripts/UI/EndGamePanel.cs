using UnityEngine;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = GameManager.Instance.GetScore().ToString();
    }

}

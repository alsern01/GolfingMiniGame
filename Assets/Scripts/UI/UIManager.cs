using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TMPro.EditorUtilities;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider movementSlider;
    [SerializeField] private GameObject connectionPanel;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI ipText;
    [SerializeField] private PreparationCountdownTimer timer;
    [SerializeField] private RectTransform uiBallBarTransform;
    [SerializeField] private Image uiBallPrefab;
    [SerializeField] private TextMeshProUGUI scoreText;

    private static UIManager instance;
    public static UIManager Instance
    {

        get { return instance; }
        private set
        {
            if (instance == null)
            {
                instance = value;
            }
            else if (instance != value)
            {
                Destroy(value);
            }
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //ShowBallHitFeedback();
    }

    void Update()
    {

    }

    public void UpdateSlider(float value)
    {
        movementSlider.value = value;

        if (value >= 1f)
        {
            movementSlider.fillRect.GetComponent<Image>().color = Color.blue;
        }
        else
        {
            movementSlider.fillRect.GetComponent<Image>().color = Color.red;
        }
    }

    public void SetIPText()
    {
        ipText.SetText($"{ipText.text} {NetworkManager.Instance.ipAddress}");
    }


    public void StartCountdown()
    {
        infoText.gameObject.SetActive(false);
        ipText.gameObject.SetActive(false);

        timer.Init();
        timer.gameObject.SetActive(true);
    }

    public void StopCountdown()
    {
        infoText.gameObject.SetActive(true);
        ipText.gameObject.SetActive(true);

        timer.gameObject.SetActive(false);
    }
    public void EnableConnectionPanel()
    {
        connectionPanel.SetActive(true);
    }

    public void DisableConnectionPanel()
    {
        connectionPanel.SetActive(false);
    }

    public void UpdateScore()
    {
        scoreText.SetText($"Score: {GameManager.Instance.GetScore()}");
    }

    public void ShowBallHitFeedback()
    {
        // Calcular la distancia entre las bolas
        float distanceBetweenBall = uiBallBarTransform.sizeDelta.x / (GameManager.Instance.maxBalls - 1);

        float x = uiBallBarTransform.anchoredPosition.x - uiBallBarTransform.sizeDelta.x / 2 + (GameManager.Instance.numBallHit - 1) * distanceBetweenBall;
        float y = uiBallBarTransform.anchoredPosition.y + uiBallBarTransform.sizeDelta.y / 2;

        // Clamp the x position to ensure it stays within the bounds of the parent
        x = Mathf.Clamp(x, uiBallBarTransform.anchoredPosition.x - uiBallBarTransform.sizeDelta.x / 2, uiBallBarTransform.anchoredPosition.x + uiBallBarTransform.sizeDelta.x / 2);

        // instancia imagen y cambia el transform a la posici�n calculada
        Image spawnedUiBall = Instantiate(uiBallPrefab, uiBallBarTransform);
        spawnedUiBall.rectTransform.anchoredPosition = new Vector2(x, y);
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider movementSlider;
    [SerializeField] private GameObject connectionPanel;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private TextMeshProUGUI ipText;
    [SerializeField] private PreparationCountdownTimer timer;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private RectTransform uiBallBarTransform;
    [SerializeField] private Image uiBallPrefab;
    [SerializeField] private Sprite uiHittedBall;
    [SerializeField] private Sprite uiHittedBomb;
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private GameObject ipPanel;

    private List<Image> uiBallImages = new List<Image>();

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


    public void StartCountdown(float countdown)
    {
        ipPanel.SetActive(false);

        timer.Init(countdown);
        timer.gameObject.SetActive(true);
    }

    public void StopCountdown()
    {
        ipPanel.SetActive(true);

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
        scoreText.SetText($"{GameManager.Instance.GetScore()}");
    }

    public void UpdateRoundsText()
    {
        roundText.SetText($"{GameManager.Instance.GetRoundsLeft()}");
    }

    public void ShowBallsToHit()
    {
        int totalBalls = GameManager.Instance.maxBalls; // Número total de imágenes a mostrar
        float distanceBetweenBall = uiBallBarTransform.sizeDelta.x / (totalBalls - 1);

        float startX = uiBallBarTransform.anchoredPosition.x - uiBallBarTransform.sizeDelta.x / 2;
        float y = 0 - uiBallPrefab.preferredHeight / 2;

        for (int i = 0; i < totalBalls; i++)
        {
            float x = startX + i * distanceBetweenBall;
            // Instancia la imagen y cambia el transform a la posición calculada
            Image spawnedUiBall = Instantiate(uiBallPrefab, uiBallBarTransform);
            spawnedUiBall.rectTransform.anchoredPosition = new Vector2(x, y);
            uiBallImages.Add(spawnedUiBall);
        }
    }

    public void ClearBallImages()
    {
        foreach (var image in uiBallImages)
        {
            Destroy(image.gameObject);
        }

        uiBallImages.Clear();
    }

    public void ChangeHitImageSprite(int index, bool bomb)
    {
        if (index < GameManager.Instance.maxBalls)
        {
            if (bomb)
                uiBallImages[index].sprite = uiHittedBomb;
            else
                uiBallImages[index].sprite = uiHittedBall;

        }
    }
}

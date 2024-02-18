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


    // Update is called once per frame
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
}

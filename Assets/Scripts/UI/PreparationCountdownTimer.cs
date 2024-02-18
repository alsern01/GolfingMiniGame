using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PreparationCountdownTimer : MonoBehaviour
{
    private float timeLeft;
    private TextMeshProUGUI timerText;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();

    }

    void Update()
    {
        if (timeLeft > 1)
        {
            timeLeft -= Time.deltaTime;
            timerText.SetText(string.Format("{0:0}", timeLeft));
        }
        else
        {
            Invoke("OnCountdownEnded", 1.0f);
        }

    }

    private void OnCountdownEnded()
    {
        GameManager.Instance.StartGame();
        UIManager.Instance.DisableConnectionPanel();
        this.gameObject.SetActive(false);
    }

    public void Init()
    {
        timeLeft = 10;
    }
}

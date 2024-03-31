using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PreparationCountdownTimer : MonoBehaviour
{
    private float timeLeft;
    private TextMeshProUGUI timerText;
    private bool countdownFinished = false;

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
        else if (!countdownFinished)
        {
            countdownFinished = true;
            timerText.SetText("YA!!");
            Invoke("OnCountdownEnd", 1.0f);
        }

    }

    private void OnCountdownEnd()
    {
        GameManager.Instance.StartRound();
        UIManager.Instance.DisableConnectionPanel();
        this.gameObject.SetActive(false);
    }

    public void Init(float prepTime)
    {
        UIManager.Instance.EnableConnectionPanel();
        timeLeft = prepTime;
        countdownFinished = false;
    }
}

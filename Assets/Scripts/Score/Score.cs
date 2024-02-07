using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI textScore;

    private float score;

    private void Start()
    {
        textScore = GetComponent<TextMeshProUGUI>();
    }

    public void Update(){
        textScore.text = score.ToString();
    }

    public void SumarPuntos(float addScore){
        score += addScore;
    }

    public void RestarPuntos(float subScore){
        score -= subScore;
    }
}

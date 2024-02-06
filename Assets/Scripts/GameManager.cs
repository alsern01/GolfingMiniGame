using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    public bool ballCreated { get; set; }
    public bool ballHit { get; set; }

    private int _score;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public bool clientConnected { get; private set; }


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //Convertirlo a false
            clientConnected = true;
            DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (NetworkManager.Instance.Server.ClientCount >= 1)
        {
            clientConnected = true;
        }
    }

    public void AddPoints(int numPoints)
    {
        _score += numPoints;
        _scoreText.SetText($"Score: {_score}");
    }

    public int GetScore()
    {
        return _score;
    }
}
